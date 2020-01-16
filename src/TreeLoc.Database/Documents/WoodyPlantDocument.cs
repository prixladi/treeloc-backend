using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TreeLoc.Database.Documents.Locations;

namespace TreeLoc.Database.Documents
{
  [DbCollection("WoodyPlants")]
  public class WoodyPlantDocument: DocumentBase
  {
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string Note { get; set; } = default!;
    public LocationBase Location { get; set; } = default!;

    [BsonIgnoreIfDefault]
    public ObjectId[] InnerWoodyPlantIds { get; set; } = default!;
  }
}
