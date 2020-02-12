using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class MultiLineStringGeometry: GeometryBase
  {
    public override string Type { get; set; } = "MultiLineString";

    [BsonElement("coordinates")]
    public double[][][] Coordinates { get; set; } = default!;
  }
}
