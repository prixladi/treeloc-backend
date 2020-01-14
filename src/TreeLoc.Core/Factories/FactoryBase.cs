using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeLoc.Factories
{
  public abstract class FactoryBase<THandler>
  {
    private readonly IEnumerable<THandler> fProducts;

    protected FactoryBase(IEnumerable<THandler> products)
    {
      fProducts = products;
    }

    protected IEnumerable<THandler> GetMany()
    {
      return fProducts;
    }

    public THandler GetOne(Func<THandler, bool> predicate)
    {
      return fProducts
        .Where(predicate)
        .SingleOrDefault();
    }
  }
}
