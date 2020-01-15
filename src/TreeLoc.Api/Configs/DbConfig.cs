using System;
using TreeLoc.Database;

namespace TreeLoc.Api.Configs
{
  public class DbConfig: IDbConfig
  {
    public string DatabaseName => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoDatabaseName);

    public string Url => EnvironmentVariables.GetOrThrow(EnvironmentVariables._MongoUrl);
  }
}
