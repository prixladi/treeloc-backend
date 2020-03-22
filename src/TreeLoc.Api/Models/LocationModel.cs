using MongoDB.Bson.Serialization.Attributes;
using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Api.Models
{
  public class LocationModel
  {
    public string? Name { get; set; }

    [BsonIgnoreIfNull]
    public GeometryBase? Geometry { get; set; }
  }
}
