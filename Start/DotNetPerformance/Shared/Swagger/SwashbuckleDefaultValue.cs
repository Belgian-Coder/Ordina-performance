using System;

namespace DotNetPerformance.Shared.Swagger
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public class SwashbuckleDefaultValue : Attribute
    {
        public string Parameter { get; set; }
        public string Value { get; set; }

        public SwashbuckleDefaultValue(string parameter, string value)
        {
            Parameter = parameter;
            Value = value;
        }
    }
}
