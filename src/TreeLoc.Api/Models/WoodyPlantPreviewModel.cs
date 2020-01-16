using MongoDB.Bson;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantPreviewModel
  {
    public ObjectId Id { get; set; }
    public string Name { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public string Note { get; set; } = default!;
  }
}
