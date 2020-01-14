using System;
using System.Collections.Generic;
using System.Text;
using TreeLoc.Handlers.Exceptions;

namespace TreeLoc.Factories
{
  public class ExceptionHandlerFactory: FactoryBase<IExceptionHandler>, IExceptionHandlerFactory
  {
    public ExceptionHandlerFactory(IEnumerable<IExceptionHandler> products)
      : base(products) { }

    public IExceptionHandler CreateOne(Exception ex)
    {
      return GetOne(x => x.CanHandle(ex));
    }
  }
}
