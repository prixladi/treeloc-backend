using System;

namespace TreeLoc.Database
{
  public sealed class DbCollectionAttribute: Attribute
  {
    public string Name { get; }

    public DbCollectionAttribute(string name)
    {
      Name = name;
    }
  }
}
