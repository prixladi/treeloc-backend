using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace TreeLoc.Loader
{
  [ExcludeFromCodeCoverage]
  public static class HttpClientContext
  {
    internal static HttpClient Client { get; } = new HttpClient();
  }
}
