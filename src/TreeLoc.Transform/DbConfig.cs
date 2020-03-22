using System;
using TreeLoc.Database;

namespace TreeLoc.Transform
{
  public class DbConfig: IDbConfig
  {
    public string DatabaseName => "TreeLoc";
    public string Url => "mongodb://192.168.0.102:27017";
  }
}
