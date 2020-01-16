using System;
using System.Collections.Generic;
using System.Linq;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class WoodyPlantsExtensions
  {
    public static WoodyPlantDetailModel ToDetail(this WoodyPlantDocument woodyPlantDocument)
    {
      if (woodyPlantDocument is null)
        throw new ArgumentNullException(nameof(woodyPlantDocument));

      return new WoodyPlantDetailModel
      {
        Id = woodyPlantDocument.Id,
        Name = woodyPlantDocument.Name,
        ImageUrl = woodyPlantDocument.ImageUrl,
        Note = woodyPlantDocument.Note
      };
    }

    public static WoodyPlantPreviewModel ToPreview(this WoodyPlantDocument woodyPlantDocument)
    {
      if (woodyPlantDocument is null)
        throw new ArgumentNullException(nameof(woodyPlantDocument));

      return new WoodyPlantPreviewModel
      {
        Id = woodyPlantDocument.Id,
        Name = woodyPlantDocument.Name,
        ImageUrl = woodyPlantDocument.ImageUrl,
        Note = woodyPlantDocument.Note
      };
    }

    public static List<WoodyPlantPreviewModel> ToPreview(this List<WoodyPlantDocument> woodyPlantDocuments)
    {
      if (woodyPlantDocuments is null)
        throw new ArgumentNullException(nameof(woodyPlantDocuments));

      return woodyPlantDocuments
        .Select(ToPreview)
        .ToList();
    }
  }
}
