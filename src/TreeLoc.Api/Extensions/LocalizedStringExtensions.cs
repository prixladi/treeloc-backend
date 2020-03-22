using System;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class LocalizedStringExtensions
  {
    public static LocalizedStringModel ToModel(this LocalizedStringDocument? localizedString)
    {
      if (localizedString is null)
        return new LocalizedStringModel();

      return new LocalizedStringModel
      {
        Czech = localizedString.Czech
      };
    }
  }
}
