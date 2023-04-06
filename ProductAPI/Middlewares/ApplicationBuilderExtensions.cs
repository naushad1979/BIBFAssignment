namespace ProductAPI.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
