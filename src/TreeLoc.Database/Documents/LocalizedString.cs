using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents
{
  [BsonIgnoreExtraElements]
  public class LocalizedString
  {
    [BsonElement("cs")]
    public string? Czech { get; set; }
  }
}
