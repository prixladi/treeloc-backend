using MongoDB.Bson.Serialization.Attributes;
using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Database.Documents
{
  [BsonIgnoreExtraElements]
  public class LocationDocument
  {
    public string? Name { get; set; }
    
    [BsonIgnoreIfNull]
    public GeometryBase? Geometry { get; set; }
  }
}
