using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace TreeLoc.Api.Configs
{
  [ExcludeFromCodeCoverage]
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
