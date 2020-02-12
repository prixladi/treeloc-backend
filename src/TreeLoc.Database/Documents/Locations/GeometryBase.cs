using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  [BsonKnownTypes(
    typeof(PointGeometry), 
    typeof(MultiPointGeometry), 
    typeof(LineStringGeometry), 
    typeof(MultiLineStringGeometry), 
    typeof(PolygonGeometry), 
    typeof(MultiPolygonGeometry))]
  public abstract class GeometryBase
  {
    [BsonElement("type")]
    public virtual string Type { get; set; } = default!;
  }
}
