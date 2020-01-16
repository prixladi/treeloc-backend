using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace TreeLoc.Api.Binders
{
  public class ObjectIdBinder: IModelBinder
  {
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext.ModelType != typeof(ObjectId)
        && bindingContext.ModelType != typeof(ObjectId?))
        return Task.CompletedTask;

      var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
      if (valueProviderResult == ValueProviderResult.None)
        return Task.CompletedTask;

      string firstValue = valueProviderResult.FirstValue;
      if (!ObjectId.TryParse(firstValue, out var objectId))
      {
        bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"Parameter '{bindingContext.ModelName}' has invalid format.");
        return Task.CompletedTask;
      }

      bindingContext.Result = ModelBindingResult.Success(objectId);
      return Task.CompletedTask;
    }
  }
}
