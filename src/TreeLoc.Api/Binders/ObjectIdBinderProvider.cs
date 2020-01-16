using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;

namespace TreeLoc.Api.Binders
{
  public class ObjectIdBinderProvider: IModelBinderProvider
  {
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof(context));

      if (context.Metadata.ModelType == typeof(ObjectId)
        || context.Metadata.ModelType == typeof(ObjectId?))
        return new ObjectIdBinder();

      return null;
    }
  }
}
