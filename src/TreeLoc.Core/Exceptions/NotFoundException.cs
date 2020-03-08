using System;

namespace TreeLoc.Exceptions
{
  public class NotFoundException: Exception
  {
    public NotFoundException(string message)
      : base(message) { }
  }
}
