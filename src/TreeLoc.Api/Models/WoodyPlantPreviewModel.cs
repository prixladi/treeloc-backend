using System;
using MongoDB.Bson;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantPreviewModel
  {
    public ObjectId Id { get; set; }
    public LocalizedStringModel LocalizedNames { get; set; } = default!;
    public LocalizedStringModel LocalizedNotes { get; set; } = default!;
    public LocalizedStringModel LocalizedSpecies { get; set; } = default!;
    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    public Location? Location { get; set; }
  }
}
