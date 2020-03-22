using System;
using MongoDB.Bson;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantPreviewModel
  {
    public ObjectId Id { get; set; }
    public LocalizedStringModel LocalizedNames { get; set; } = default!;
    public LocalizedStringModel LocalizedNotes { get; set; } = default!;
    public LocalizedStringModel LocalizedSpecies { get; set; } = default!;
    public string[] ImageUrls { get; set; } = Array.Empty<string>();
    public LocationModel? Location { get; set; }
  }
}
