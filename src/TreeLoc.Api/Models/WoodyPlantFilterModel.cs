using System.ComponentModel.DataAnnotations;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantFilterModel
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; set; }

    [Range(1, 100)]
    [Required]
    public int Take { get; set; }

    public string? Name { get; set; }
  }
}
