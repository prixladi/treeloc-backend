using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using TreeLoc.Api.Swagger;

namespace TreeLoc.Api.Configs
{
  [ExcludeFromCodeCoverage]
  internal static class SwaggerConfig
  {
    public const string _SwaggerSecurityDefinitionName = "BasicAuthentication";

    private const string _RouteTemplate = "/docs/{documentName}/swagger.json";

    public static void SetupSwagger(SwaggerOptions options)
    {
      options.RouteTemplate = _RouteTemplate;
    }

    public static void SetupSwaggerGen(SwaggerGenOptions options)
    {
      options.SwaggerDoc("service", new OpenApiInfo
      {
        Title = $"TreeLoc Api",
        Description = "TreeLoc REST API"
      });

      options.MapType<ObjectId>(() => new OpenApiSchema { Type = "string" });
      options.MapType<ObjectId?>(() => new OpenApiSchema { Type = "string" });
      options.MapType<byte>(() => new OpenApiSchema { Type = "integer" });

      options.OperationFilter<FlattenObjectIdOperationFilter>();

      foreach (string xmlFile in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, "*.xml"))
        options.IncludeXmlComments(xmlFile);
    }

    public static void SetupSwaggerUI(SwaggerUIOptions options)
    {
      options.SwaggerEndpoint(_RouteTemplate.Replace("{documentName}", "service"), "Service");
      options.DisplayRequestDuration();
    }
  }
}
