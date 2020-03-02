using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantFilterModel: IValidatableObject
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; set; }

    [Range(1, 300)]
    [Required]
    public int Take { get; set; }

    public string? Text { get; set; }

    public Point? Point { get; set; }

    public double? Distance { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Text != null && Point != null)
        yield return new ValidationResult($"Cannot use GeoQuery and TextSearch in one Query", new string[] { nameof(Point), nameof(Text) });
    }
  }
}
