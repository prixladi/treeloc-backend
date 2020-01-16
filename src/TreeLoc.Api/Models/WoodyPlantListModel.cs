using System.Collections.Generic;

namespace TreeLoc.Api.Models
{
  public class WoodyPlantListModel
  {
    public List<WoodyPlantPreviewModel> WoodyPlants { get; set; } = default!;
    public long TotalCount { get; set; }
  }
}
