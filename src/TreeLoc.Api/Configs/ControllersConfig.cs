using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Filters;

namespace TreeLoc.Api.Configs
{
  internal static class ControllersConfig
  {
    public static void Configure(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
    }
  }
}
