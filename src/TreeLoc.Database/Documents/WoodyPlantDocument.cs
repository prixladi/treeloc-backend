using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents
{
  [DbCollection("WoodyPlants")]
  public class WoodyPlantDocument: DocumentBase
  {
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string Note { get; set; } = default!;
    public Location Location { get; set; } = default!;

    [BsonIgnoreIfDefault]
    public ObjectId[] InnerWoodyPlantIds { get; set; } = default!;
  }
}
