using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Converters;

namespace TreeLoc.Api.Configs
{
  internal static class JsonConfig
  {
    public static void Setup(JsonOptions jsonOptions)
    {
      jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
      jsonOptions.JsonSerializerOptions.Converters.Add(new ObjectIdJsonConverter());
    }
  }
}
