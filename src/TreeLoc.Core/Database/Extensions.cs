using System;
using System.Reflection;
using MongoDB.Driver;
using TreeLoc.Database.Documents;

namespace TreeLoc.Database
{
  public static class Extensions
  {
    public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase database)
      where TDocument : DocumentBase
    {
      if (database is null)
        throw new ArgumentNullException(nameof(database));

      var attribute = typeof(TDocument).GetCustomAttribute<DbCollectionAttribute>();
      if (attribute == null)
        throw new InvalidOperationException($"Attribute {typeof(DbCollectionAttribute).Name} does not exist on type {typeof(TDocument)}.");

      return database.GetCollection<TDocument>(attribute.Name);
    }
  }
}
