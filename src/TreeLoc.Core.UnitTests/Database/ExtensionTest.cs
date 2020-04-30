using System;
using MongoDB.Driver;
using NSubstitute;
using TreeLoc.Database;
using TreeLoc.Database.Documents;
using Xunit;

namespace TreeLoc.UnitTests.Database
{
  public class ExtensionTest
  {
    private const string _Collection = "collection";

    public class NotDocument: DocumentBase { }

    [DbCollection(_Collection)]
    public class Document: DocumentBase { }

    private readonly IMongoDatabase fDatabase;

    public ExtensionTest()
    {
      fDatabase = Substitute.For<IMongoDatabase>();
    }

    [Fact]
    public void GetCollection_Exceptions_Test()
    {
      Assert.Throws<ArgumentNullException>(() => ((IMongoDatabase)null).GetCollection<NotDocument>());
      Assert.Throws<InvalidOperationException>(() => fDatabase.GetCollection<NotDocument>());
    }

    [Fact]
    public void GetCollection_Sucess_Test()
    {
      var collection = Substitute.For<IMongoCollection<Document>>();

      fDatabase
        .GetCollection<Document>(Arg.Is(_Collection))
        .Returns(collection);

      Assert.Equal(collection, fDatabase.GetCollection<Document>());
    }
  }
}
