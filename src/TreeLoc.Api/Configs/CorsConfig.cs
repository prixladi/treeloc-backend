using Microsoft.AspNetCore.Cors.Infrastructure;

namespace TreeLoc.Api.Configs
{
  internal static class CorsConfig
  {
    public static void Setup(CorsPolicyBuilder builder)
    {
      builder.AllowAnyMethod();
      builder.AllowAnyHeader();
      builder.AllowAnyOrigin();
    }
  }
}
