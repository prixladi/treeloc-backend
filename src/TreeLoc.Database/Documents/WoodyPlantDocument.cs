using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TreeLoc.Database.Documents
{
  [DbCollection("WoodyPlants")]
  [BsonIgnoreExtraElements]
  public class WoodyPlantDocument: DocumentBase
  {
    public LocalizedStringDocument LocalizedNames { get; set; } = new LocalizedStringDocument();
    public LocalizedStringDocument LocalizedNotes { get; set; } = new LocalizedStringDocument();
    public LocalizedStringDocument LocalizedSpecies { get; set; } = new LocalizedStringDocument();
    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    public PlantType Type { get; set; }
    public LocationDocument Location { get; set; } = new LocationDocument();
    public ObjectId[] InnerWoodyPlantIds { get; set; } = Array.Empty<ObjectId>();
    public string? Version { get; set; }

    [BsonIgnoreIfNull]
    public double? TextMatchScore { get; set; }
  }
}
