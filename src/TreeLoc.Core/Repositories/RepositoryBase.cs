using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TreeLoc.Database;
using TreeLoc.Database.Documents;

namespace TreeLoc.Repositories
{
  public class RepositoryBase<TDocument, TDocumentBase>: IRepositoryBase<TDocument>
    where TDocumentBase : DocumentBase
    where TDocument: TDocumentBase
  {
    private readonly DbContext fDbContext;

    protected IMongoCollection<TDocument> Collection => fDbContext.Database.GetCollection<TDocumentBase>().OfType<TDocument>();
    protected IMongoQueryable<TDocument> Query => Collection.AsQueryable();

    public RepositoryBase(DbContext dbContext)
    {
      fDbContext = dbContext;
    }

    public async Task<TDocument?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
    {
      return Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
    }
  }

  public class RepositoryBase<TDocument>: IRepositoryBase<TDocument>
    where TDocument : DocumentBase
  {
    private readonly DbContext fDbContext;

    protected IMongoCollection<TDocument> Collection => fDbContext.Database.GetCollection<TDocument>();
    protected IMongoQueryable<TDocument> Query => Collection.AsQueryable();

    public RepositoryBase(DbContext dbContext)
    {
      fDbContext = dbContext;
    }

    public async Task<TDocument?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
      return await Query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public Task InsertOneAsync(TDocument document, CancellationToken cancellationToken)
    {
      return Collection.InsertOneAsync(document, cancellationToken: cancellationToken);
    }
  }
}
