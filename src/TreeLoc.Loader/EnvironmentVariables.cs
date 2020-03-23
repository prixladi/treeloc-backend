using System;
using System.Diagnostics.CodeAnalysis;

namespace TreeLoc.Loader
{
  [ExcludeFromCodeCoverage]
  public static class EnvironmentVariables
  {
    public const string _MongoUrl = "MONGO_URL";
    public const string _MongoDatabaseName = "MONGO_DATABASE_NAME";
    public const string _LoaderDelay = "LOADER_DELAY";
    public const string _DiscoveryDelay = "DISCOVERY_DELAY";
    public const string _DiscoveryUrl = "DISCOVERY_URL";

    public static string GetOrThrow(string name)
    {
      return Environment.GetEnvironmentVariable(name) ?? throw new InvalidOperationException($"Variable with name '{name}' does not exist.");
    }

    public static string? Get(string name)
    {
      return Environment.GetEnvironmentVariable(name);
    }
  }
}
