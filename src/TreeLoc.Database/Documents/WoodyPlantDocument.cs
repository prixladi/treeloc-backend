using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents
{
  [DbCollection("WoodyPlants")]
  [BsonIgnoreExtraElements]
  public class WoodyPlantDocument: DocumentBase
  {
    public LocalizedString LocalizedNames { get; set; } = new LocalizedString();
    public LocalizedString LocalizedNotes { get; set; } = new LocalizedString();
    public LocalizedString LocalizedSpecies { get; set; } = new LocalizedString();
    public string? ImageUrl { get; set; } 
    public Location? Location { get; set; } 
    public ObjectId[]? InnerWoodyPlantIds { get; set; }

    [BsonIgnoreIfNull]
    public double? TextMatchScore { get; set; }
  }
}
