using System;
using System.Diagnostics;
using MongoDB.Driver;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class FilterSortExtensions
  {
    public static FilterDefinition<WoodyPlantDocument> ToFilterDefinition(this WoodyPlantFilterModel model)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      var filter = FilterDefinition<WoodyPlantDocument>.Empty;

      if (model.Text != null)
      {
        var text = model.Text.ToLower();
        filter &= Builders<WoodyPlantDocument>.Filter.Text(text);
      }

      if (model.Point != null)
      {
        Debug.Assert(model.Distance != null);
        filter &= Builders<WoodyPlantDocument>.Filter.NearSphere(x => x.Location!.Geometry, model.Point.Latitude, model.Point.Longitude, model.Distance.Value);
      }

      return filter;
    }

    public static FindOptions<WoodyPlantDocument> ToFindOptions(this WoodyPlantSortModel model, int skip, int take, bool textSearch)
    {
      if (model is null)
        throw new ArgumentNullException(nameof(model));

      SortDefinition<WoodyPlantDocument>? sort = null;

      switch (model.SortBy)
      {
        case nameof(WoodyPlantDocument.LocalizedNames):
          sort = model.Ascending ? Builders<WoodyPlantDocument>.Sort.Ascending(x => x.LocalizedNames) : Builders<WoodyPlantDocument>.Sort.Descending(x => x.LocalizedNames);
          break;
        case nameof(WoodyPlantDocument.LocalizedNotes):
          sort = model.Ascending ? Builders<WoodyPlantDocument>.Sort.Ascending(x => x.LocalizedNotes) : Builders<WoodyPlantDocument>.Sort.Descending(x => x.LocalizedNotes);
          break;
        case nameof(WoodyPlantDocument.LocalizedSpecies):
          sort = model.Ascending ? Builders<WoodyPlantDocument>.Sort.Ascending(x => x.LocalizedSpecies) : Builders<WoodyPlantDocument>.Sort.Descending(x => x.LocalizedSpecies);
          break;
        case nameof(WoodyPlantDocument.TextMatchScore):
          sort = Builders<WoodyPlantDocument>.Sort.MetaTextScore(nameof(WoodyPlantDocument.TextMatchScore));
          break;
      }

      var options = new FindOptions<WoodyPlantDocument> { Skip = skip, Limit = take };

      if (textSearch && model.SortBy == nameof(WoodyPlantDocument.TextMatchScore))
        options.Projection = Builders<WoodyPlantDocument>.Projection.MetaTextScore(nameof(WoodyPlantDocument.TextMatchScore));

      if (sort != null)
        options.Sort = sort;

      return options;
    }
  }
}
