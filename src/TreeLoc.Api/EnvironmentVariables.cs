using System;

namespace TreeLoc.Api
{
  public static class EnvironmentVariables
  {
    public const string _MongoUrl = "MONGO_URL";
    public const string _MongoDatabaseName = "MONGO_DATABASE_NAME";
    public const string _SentryUrl = "SENTRY_URL";

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
