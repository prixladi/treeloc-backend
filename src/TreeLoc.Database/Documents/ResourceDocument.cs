namespace TreeLoc.Database.Documents
{
  [DbCollection("Resource")]
  public class ResourceDocument: DocumentBase
  {
    public string Url { get; set; } = default!;
    public bool Fetched { get; set; } = default!;
  }
}
