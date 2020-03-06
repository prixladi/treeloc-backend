using System;
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;

namespace TreeLoc.Database
{
  [ExcludeFromCodeCoverage]
  public class DbContext
  {
    public IDbConfig Config { get; }
    public IMongoDatabase Database { get; }

    public DbContext(IDbConfig config)
    {
      Config = config ?? throw new ArgumentNullException(nameof(config));

      var client = new MongoClient(config.Url);

      Database = client.GetDatabase(config.DatabaseName);
    }
  }
}
