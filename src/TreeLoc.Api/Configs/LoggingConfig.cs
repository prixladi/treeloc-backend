using System.Diagnostics.CodeAnalysis;

namespace TreeLoc.Api.Configs
{
  [ExcludeFromCodeCoverage]
  public static class LoggingConfig
  {
    public static string SentryUrl => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoUrl);
  }
}
