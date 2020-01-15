namespace TreeLoc.Database
{
  public interface IDbConfig
  {
    string DatabaseName { get; }
    string Url { get; }
  }
}
