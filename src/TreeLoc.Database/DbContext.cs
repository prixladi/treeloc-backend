using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TreeLoc.Database
{
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
