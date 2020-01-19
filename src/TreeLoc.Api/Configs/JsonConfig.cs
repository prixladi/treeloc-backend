using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Converters;

namespace TreeLoc.Api.Configs
{
  internal static class JsonConfig
  {
    public static void Setup(MvcNewtonsoftJsonOptions jsonOptions)
    {
      jsonOptions.SerializerSettings.Converters.Add(new ObjectIdNewtonsoftJsonConverter());
    }
  }
}
