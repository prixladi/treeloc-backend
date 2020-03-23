using System.Net.Http;

namespace TreeLoc.Loader
{
  public static class HttpClientContext
  {
    internal static HttpClient Client { get; } = new HttpClient();
  }
}
