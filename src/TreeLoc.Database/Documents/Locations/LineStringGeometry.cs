using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class LineStringGeometry: GeometryBase
  {
    public override string Type { get; set; } = "LineString";

    [BsonElement("coordinates")]
    public double[][] Coordinates { get; set; } = default!;
  }
}
