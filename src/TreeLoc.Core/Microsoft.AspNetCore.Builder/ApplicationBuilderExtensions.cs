using TreeLoc.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
      return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
  }
}
