using MongoDB.Bson;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantPreviewModel
  {
    public ObjectId Id { get; set; }
    public string? Name { get; set; } 
    public string? ImageUrl { get; set; } 
    public string? Note { get; set; } 
    public Location? Location { get; set; }
  }
}
