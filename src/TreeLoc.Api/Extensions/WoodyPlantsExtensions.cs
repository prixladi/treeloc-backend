using System;
using System.Collections.Generic;
using System.Linq;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class WoodyPlantsExtensions
  {
    public static WoodyPlantDetailModel ToDetail(this WoodyPlantDocument doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new WoodyPlantDetailModel
      {
        Id = doc.Id,
        Name = doc.Name,
        ImageUrl = doc.ImageUrl,
        Note = doc.Note,
        Location = doc.Location
      };
    }

    public static WoodyPlantPreviewModel ToPreview(this WoodyPlantDocument doc)
    {
      if (doc is null)
        throw new ArgumentNullException(nameof(doc));

      return new WoodyPlantPreviewModel
      {
        Id = doc.Id,
        Name = doc.Name,
        ImageUrl = doc.ImageUrl,
        Note = doc.Note,
        Location = doc.Location
      };
    }

    public static List<WoodyPlantPreviewModel> ToPreview(this List<WoodyPlantDocument> docs)
    {
      if (docs is null)
        throw new ArgumentNullException(nameof(docs));

      return docs
        .Select(ToPreview)
        .ToList();
    }
  }
}
