using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents.Locations
{
  [BsonKnownTypes(typeof(PointLocation))]
  public abstract class LocationBase
  {
    public virtual string type { get; set; } = default!;
  }
}
