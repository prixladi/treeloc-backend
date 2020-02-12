using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  public class MultiPointGeometry: GeometryBase
  {
    public override string Type { get; set; } = "MultiPoint";

    [BsonElement("coordinates")]
    public double[][] Coordinates { get; set; } = default!;
  }
}
