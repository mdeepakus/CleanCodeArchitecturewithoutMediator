using CleanArchitecture.SampleProject.Application.Exceptions;
using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace CleanArchitecture.SampleProject.Api.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = JsonConvert.SerializeObject(validationException.ValdationErrors);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    break;
                case ApiException apiException:
                    httpStatusCode = (HttpStatusCode)apiException.StatusCode;
                    result = JsonConvert.SerializeObject(apiException.Errors);
                    break;
                case Exception ex:
                    Log.Logger.Write(Serilog.Events.LogEventLevel.Information, ex, "A Exception has occured");
                    httpStatusCode = HttpStatusCode.BadRequest;
                    break;
            }



            //if (httpStatusCode == HttpStatusCode.InternalServerError)
            //{
            //    context.Response.StatusCode = (int)httpStatusCode;
            //    return context.Response.WriteAsync(exception.Message + ':' + exception.StackTrace);
            //}
            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = exception.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
