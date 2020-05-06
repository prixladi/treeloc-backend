using System;
using System.Diagnostics.CodeAnalysis;

namespace TreeLoc.Loader
{
  [ExcludeFromCodeCoverage]
  public static class EnvironmentVariables
  {
    public const string _MongoUrl = "MONGO_URL";
    public const string _MongoDatabaseName = "MONGO_DATABASE_NAME";
    public const string _LoaderInterval = "LOADER_INTERVAL";
    public const string _DiscoveryInterval = "DISCOVERY_INTERVAL";
    public const string _DiscoveryUrl = "DISCOVERY_URL";
    public const string _RemoveOld = "REMOVE_OLD";

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
