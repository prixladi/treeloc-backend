using System.Diagnostics;
using MongoDB.Driver;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class FilterExtensions
  {
    public static FilterDefinition<WoodyPlantDocument> ToFilterDefinition(this WoodyPlantFilterModel model)
    {
      var filter = FilterDefinition<WoodyPlantDocument>.Empty;

      if(model.Text != null)
      {
        var text = model.Text.ToLower();
        filter &= Builders<WoodyPlantDocument>.Filter.Where(doc => (doc.Name != null && doc.Name.Contains(text))
          || (doc.Note != null && doc.Note.Contains(text))
          || (doc.Species != null && doc.Species.Contains(text)));
      }

      if(model.Point != null)
      {
        Debug.Assert(model.Distance != null);
        filter &= Builders<WoodyPlantDocument>.Filter.NearSphere(x => x.Location!.Geometry, model.Point.Latitude, model.Point.Longitude, model.Distance.Value);
      }

      return filter;
    }
  }
}
