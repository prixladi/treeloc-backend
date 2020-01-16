using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Binders;
using TreeLoc.Api.Filters;

namespace TreeLoc.Api.Configs
{
  internal static class ControllersConfig
  {
    public static void Configure(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
      options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
    }
  }
}
