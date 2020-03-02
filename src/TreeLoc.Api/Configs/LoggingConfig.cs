namespace TreeLoc.Api.Configs
{
  public static class LoggingConfig
  {
    public static string SentryUrl => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoUrl);
  }
}
