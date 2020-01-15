using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using TreeLoc.Database.Documents;

namespace TreeLoc.Repositories
{
  public interface IRepositoryBase<TDocument> 
    where TDocument : DocumentBase
  {
    Task<TDocument?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken);
    Task InsertOneAsync(TDocument document, CancellationToken cancellationToken);
  }
}