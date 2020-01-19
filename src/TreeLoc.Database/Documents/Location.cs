using MongoDB.Bson.Serialization.Attributes;
using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Database.Documents
{
  [BsonIgnoreExtraElements]
  public class Location
  {
    public string? Name { get; set; }
    public GeometryBase? Geometry { get; set; }
  }
}
