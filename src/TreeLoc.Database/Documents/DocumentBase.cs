using MongoDB.Bson;

namespace TreeLoc.Database.Documents
{
  public abstract class DocumentBase
  {
    public ObjectId Id { get; set; }
  }
}
