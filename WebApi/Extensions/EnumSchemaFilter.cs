using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;
using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (!context.Type.IsEnum) return;

            var values = Enum.GetValues(context.Type)
                .Cast<Enum>()
                .Select(e =>
                {
                    var member = context.Type.GetMember(e.ToString()).First();
                    var description = member.GetCustomAttribute<DescriptionAttribute>()?.Description ?? e.ToString();
                    return $"{Convert.ToInt32(e)} = {description}";
                });

            schema.Description += "Enum: [ " + string.Join(", ", values) + " ]";
        }
    }
}
