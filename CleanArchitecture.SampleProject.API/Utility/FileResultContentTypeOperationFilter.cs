using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.SampleProject.Api.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class FileResultContentTypeOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var requestAttribute = context.MethodInfo.GetCustomAttributes(typeof(FileResultContentTypeAttribute), false)
                .Cast<FileResultContentTypeAttribute>()
                .FirstOrDefault();

            if (requestAttribute == null) return;

            operation.Responses.Clear();
            operation.Responses.Add("200", new OpenApiResponse
            {
                Content = new Dictionary<string, OpenApiMediaType>
            {
                {
                    requestAttribute.ContentType, new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary"
                        }
                    }
                }
            }
            });
        }
    }
}
