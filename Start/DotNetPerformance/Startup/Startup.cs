using DotNetPerformance.Data.Contexts;
using DotNetPerformance.Data.Seeders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using DotNetPerformance.Data._base;
using DotNetPerformance.Shared.Swagger;
using Swashbuckle.AspNetCore.Swagger;
using DotNetPerformance.Shared.Constants;
using DotNetPerformance.Business.Readers;
using AutoMapper;
using DotNetPerformance.Business;
using DotNetPerformance.ServiceAgents;

namespace DotNetPerformance.Startup
{
    public class Startup
    {
        public static DbModeEnum DbMode { get; } = DbModeEnum.LocalDb;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config => {
                config.Advanced.AllowAdditiveTypeMapCreation = true;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Register Swagger generator
            services.AddSwaggerGen(config => {
                config.SwaggerDoc("v1", 
                    new Info
                    {
                        Title = "DotNetPerformance API",
                        Version = "v1",
                        TermsOfService = "None",
                        Contact = new Contact()
                        {
                            Email = "jobs@ordina.be",
                            Name = "Ordina Benelux"
                        }
                    });
                config.DescribeAllEnumsAsStrings();
                config.OperationFilter<LowerCaseQueryAndBodyParameterFilter>();
                config.OperationFilter<AddDefaultValues>();
            });

            switch (DbMode)
            {
                case DbModeEnum.LocalDb:
                    // MSSQL Server express, full DB support
                    var localDBconnection = @"Server=(localdb)\mssqllocaldb;Database=DBWebshop;Trusted_Connection=True;ConnectRetryCount=0";
                    services.AddDbContext<WebshopContext>
                        (options => options.UseSqlServer(localDBconnection));
                    break;
                case DbModeEnum.SqLite:
                    // In-memory SQLite, relations supported but locks database on read/write
                    var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
                    var connectionString = connectionStringBuilder.ToString();
                    var sqlLiteConnection = new SqliteConnection(connectionString);
                    sqlLiteConnection.Open();
                    services
                      .AddEntityFrameworkSqlite()
                      .AddDbContext<WebshopContext>(
                        options => options.UseSqlite(sqlLiteConnection));
                    break;
                case DbModeEnum.Unconfigured:
                default:
                    // In memory fallback, no relations, auto incrementing keys, auto include, no sql translations
                    break;
            }

            RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetPerformance API v1");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "swagger/");
            });

            InitializeDatabases(app);
        }

        private void InitializeDatabases(IApplicationBuilder app)
        {
            BaseSeeder<WebshopContext, WebshopDbInitializer>.Seed(app);
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IServiceProvider, ServiceProvider>();
            services.AddTransient<IStatisticsReader, StatisticsReader>();
            services.AddTransient<IOrderReader, OrderReader>();
            services.AddTransient<IOrderProcessor, OrderProcessor>();
            services.AddTransient<IProductReader, ProductReader>();
            services.AddTransient<ICustomerReader, CustomerReader>();
            services.AddTransient<IMailGenerator, MailGenerator>();
            services.AddTransient<IMailProcessor, MailProcessor>();
            services.AddTransient<IMailClient, MailClient>();
        }
    }
}
