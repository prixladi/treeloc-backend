using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using MongoDB.Bson;
using NSubstitute;
using TreeLoc.Api.Binders;
using Xunit;

namespace Treeloc.Api.UnitTests.Binders
{
  public class ObjectIdBinderProviderTest
  {
    private readonly ModelBinderProviderContext fContext;

    public ObjectIdBinderProviderTest()
    {
      fContext = Substitute.For<ModelBinderProviderContext>();
    }

    [Theory]
    [InlineData(typeof(ObjectId))]
    [InlineData(typeof(ObjectId?))]
    public void GetBinder_Success_Test(Type type)
    {
      var provider = new ObjectIdBinderProvider();
      fContext.Metadata.Returns(CreateMetada(type));

      var binder = provider.GetBinder(fContext);

      Assert.NotNull(binder);
      Assert.IsType<ObjectIdBinder>(binder);
    }

    [Theory]
    [InlineData(typeof(int))]
    [InlineData(typeof(string))]
    public void GetBinder_Fail_Test(Type type)
    {
      var provider = new ObjectIdBinderProvider();
      fContext.Metadata.Returns(CreateMetada(type));

      var binder = provider.GetBinder(fContext);

      Assert.Null(binder);
    }

    [Fact]
    public void GetBinder_Exception_Test()
    {
      var provider = new ObjectIdBinderProvider();
      Assert.Throws<ArgumentNullException>(() => provider.GetBinder(null!));
    }

    private ModelMetadata CreateMetada(Type type)
    {
      return Substitute.For<ModelMetadata>(ModelMetadataIdentity.ForType(type));
    }
  }
}
