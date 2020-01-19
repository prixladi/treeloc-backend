using TreeLoc.Api.Validation;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantSortModel
  {
    [AllowedValues(
      nameof(WoodyPlantDocument.LocalizedNames),
      nameof(WoodyPlantDocument.LocalizedNotes),
      nameof(WoodyPlantDocument.LocalizedSpecies),
      nameof(WoodyPlantDocument.TextMatchScore),
      AllowNull = true)]
    public string? SortBy { get; set; }

    public bool Ascending { get; set; }
  }
}
