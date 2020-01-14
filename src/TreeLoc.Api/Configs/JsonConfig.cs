using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TreeLoc.Api.Configs
{
  public static class JsonConfig
  {
    public static void Setup(JsonOptions jsonOptions)
    {
      jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    }
  }
}
