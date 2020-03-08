using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using NSubstitute;
using TreeLoc.Api.Binders;
using Xunit;

namespace Treeloc.Api.UnitTests.Binders
{
  public class ObjectIdBinderTest
  {
    private readonly ModelBindingContext fContext;
    private readonly IValueProvider fValueProvider;

    public ObjectIdBinderTest()
    {
      fContext = Substitute.For<ModelBindingContext>();
      fValueProvider = Substitute.For<IValueProvider>();

      fContext.ValueProvider.Returns(fValueProvider);
    }

    [Fact]
    public void BindModel_Success_NoneResult_TestAsync()
    {
      string modelName = "model";

      fContext.ModelName.Returns(modelName);
      fContext.ModelType.Returns(typeof(ObjectId));
      fValueProvider.GetValue(Arg.Is(modelName)).Returns(ValueProviderResult.None);

      var binder = new ObjectIdBinder();
      var result = binder.BindModelAsync(fContext);

      Assert.True(result.IsCompleted);
    }

    [Fact]
    public void BindModel_InvalType_TestAsync()
    {
      var binder = new ObjectIdBinder();
      var result = binder.BindModelAsync(fContext);

      Assert.True(result.IsCompleted);
    }

    [Fact]
    public async Task BindModel_Success_IdResult_TestAsync()
    {
      var id = ObjectId.GenerateNewId();
      string modelName = "model";

      fContext.ModelName.Returns(modelName);
      fContext.ModelType.Returns(typeof(ObjectId));
      fValueProvider.GetValue(Arg.Is(modelName)).Returns(new ValueProviderResult(id.ToString()));
      fContext.ModelState = new ModelStateDictionary();

      var binder = new ObjectIdBinder();
      await binder.BindModelAsync(fContext);

      Assert.Equal(id, fContext.Result.Model);
    }

    [Fact]
    public async Task BindModel_Error_TestAsync()
    {
      var id = "aaaa";
      string modelName = "model";

      fContext.ModelName.Returns(modelName);
      fContext.ModelType.Returns(typeof(ObjectId));
      fValueProvider.GetValue(Arg.Is(modelName)).Returns(new ValueProviderResult(id.ToString()));
      fContext.ModelState = new ModelStateDictionary();

      var binder = new ObjectIdBinder();
      await binder.BindModelAsync(fContext);

      Assert.Single(fContext.ModelState);
    }
  }
}
