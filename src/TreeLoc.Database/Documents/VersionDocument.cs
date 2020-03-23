using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents
{
  [BsonDiscriminator("Version")]
  public class VersionDocument: DbConfigDocument
  {
    public string Version { get; set; } = default!;
  }
}
