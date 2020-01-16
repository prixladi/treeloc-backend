using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class PointGeometry: GeometryBase
  {
    public override string Type { get; set; } = "Point";

    [BsonElement("coordinates")]
    public double[] Coordinates { get; set; } = default!;
  }
}
