using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class PolygonGeometry: GeometryBase
  {
    public override string Type { get; set; } = "Polygon";

    [BsonElement("coordinates")]
    public double[][][] Coordinates { get; set; } = default!;
  }
}
