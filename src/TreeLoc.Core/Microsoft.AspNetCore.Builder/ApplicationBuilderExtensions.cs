using System.Diagnostics.CodeAnalysis;
using TreeLoc.Middlewares;

namespace Microsoft.AspNetCore.Builder
{
  [ExcludeFromCodeCoverage]
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
    {
      return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
  }
}
