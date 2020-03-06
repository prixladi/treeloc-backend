using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Converters;

namespace TreeLoc.Api.Configs
{
  [ExcludeFromCodeCoverage]
  internal static class JsonConfig
  {
    public static void Setup(MvcNewtonsoftJsonOptions jsonOptions)
    {
      jsonOptions.SerializerSettings.Converters.Add(new ObjectIdJsonConverter());
    }
  }
}
