using MongoDB.Bson;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Dtos
{
  public class WoodyPlantPreviewDto
  {
    public ObjectId Id { get; set; }
    public LocalizedStringDto LocalizedNames { get; set; } = default!;
    public LocalizedStringDto LocalizedNotes { get; set; } = default!;
    public LocalizedStringDto LocalizedSpecies { get; set; } = default!;
    public string? ImageUrl { get; set; } = default!;
    public Location? Location { get; set; }

    public double? TextMatchScore { get; set; }
  }
}
