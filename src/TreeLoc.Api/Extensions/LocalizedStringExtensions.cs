using System;
using TreeLoc.Api.Models;
using TreeLoc.Database.Documents;

namespace TreeLoc.Api.Extensions
{
  public static class LocalizedStringExtensions
  {
    public static LocalizedStringModel ToModel(this LocalizedString localizedString)
    {
      if (localizedString is null)
        throw new ArgumentNullException(nameof(localizedString));

      return new LocalizedStringModel
      {
        Czech = localizedString.Czech
      };
    }
  }
}
