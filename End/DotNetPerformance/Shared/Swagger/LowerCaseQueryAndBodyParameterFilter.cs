using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetPerformance.Shared.Swagger
{
    public class LowerCaseQueryAndBodyParameterFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) return;

            foreach (var parameter in operation.Parameters.Where(x => !String.IsNullOrWhiteSpace(x.In) && (x.In.Equals("query") || x.In.Equals("body"))))
            {
                parameter.Name = parameter.Name.ToLowerInvariant();
            }
        }
    }
}
