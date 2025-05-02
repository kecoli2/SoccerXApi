using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoccerX.API.StartUp.Swagger
{
    public class DefaultLanguageHeaderFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // Accept-Language header'ını ekleyin
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string", Default = new OpenApiString("tr-TR") },
                Description = "Dil tercihi (Örnek: tr-TR, en-US)"
            });
        }
    }
}
