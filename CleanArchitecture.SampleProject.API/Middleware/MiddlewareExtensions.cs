namespace CleanArchitecture.SampleProject.Api.Middleware
{
    /// <summary>
    /// 
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionHandlerMiddleware>();
            return builder;
        }
    }
}
