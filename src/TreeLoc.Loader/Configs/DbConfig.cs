using TreeLoc.Database;

namespace TreeLoc.Loader.Configs
{
  public class DbConfig: IDbConfig
  {
    public string DatabaseName => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoDatabaseName);
    public string Url => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoUrl);
  }
}
