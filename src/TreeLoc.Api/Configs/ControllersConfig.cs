using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Binders;
using TreeLoc.Api.Filters;

namespace TreeLoc.Api.Configs
{
  [ExcludeFromCodeCoverage]
  internal static class ControllersConfig
  {
    public static void Configure(MvcOptions options)
    {
      options.Filters.Add<ApiValidationAttribute>();
      options.ModelBinderProviders.Insert(0, new ObjectIdBinderProvider());
    }
  }
}
