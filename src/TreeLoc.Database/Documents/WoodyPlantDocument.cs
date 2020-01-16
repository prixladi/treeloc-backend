using System;
using MongoDB.Bson;

namespace TreeLoc.Database.Documents
{
  [DbCollection("WoodyPlants")]
  public class WoodyPlantDocument: DocumentBase
  {
    public string? Name { get; set; } 
    public string? Species { get; set; }
    public string? Note { get; set; } 
    public string? ImageUrl { get; set; } 
    public Location? Location { get; set; } 
    public ObjectId[]? InnerWoodyPlantIds { get; set; }

    public Tuple<string, string>[]? LocalizedNames { get; set; } 
    public Tuple<string, string>[]? LocalizedNotes { get; set; } 
    public Tuple<string, string>[]? LocalizedSpecies { get; set; } 
  }
}
