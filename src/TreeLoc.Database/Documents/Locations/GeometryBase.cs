using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  [BsonKnownTypes(typeof(PointGeometry))]
  public abstract class GeometryBase
  {
    [BsonElement("type")]
    public virtual string Type { get; set; } = default!;
  }
}
