using TreeLoc.Database.Documents;
using TreeLoc.OFN;

namespace TreeLoc.Extension
{
  public static class LocalizedStringExtensions
  {
    public static LocalizedStringDocument ToDocument(this LocalizedString? localizedString)
    {
      if (localizedString == null)
        return new LocalizedStringDocument();

      return new LocalizedStringDocument
      {
        Czech = localizedString.Czech
      };
    }
  }
}
