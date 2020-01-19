using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantFilterModel: IValidatableObject
  {
    [Range(0, int.MaxValue)]
    [Required]
    public int Skip { get; set; }

    [Range(1, 100)]
    [Required]
    public int Take { get; set; }

    public string? Text { get; set; }

    public Point? Point { get; set; }

    public double? Distance { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Text != null && Point != null)
        yield return new ValidationResult($"Cannot use GeoQuery and TextSearch in one Query", new string[] { nameof(Point), nameof(Text) });

      if (Point == null != (Distance == null))
        yield return new ValidationResult($"For geoquery you need both '{nameof(Point)}' and {nameof(Distance)} to be not null", new string[] { nameof(Point), nameof(Distance) });
    }
  }
}
