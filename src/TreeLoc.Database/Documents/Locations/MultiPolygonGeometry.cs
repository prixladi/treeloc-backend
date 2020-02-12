using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class MultiPolygonGeometry: GeometryBase
  {
    public override string Type { get; set; } = "MultiPolygon";

    [BsonElement("coordinates")]
    public double[][][][] Coordinates { get; set; } = default!;
  }
}
