using Microsoft.Extensions.DependencyInjection;
using TreeLoc.Api.IoC;
using Xunit;

namespace Treeloc.Api.UnitTests.IoC
{
  public class IoCTest
  {
    [Fact]
    public void TestServiceCollection()
    {
      var collection = new ServiceCollection();
      collection.AddHostedServices();
      collection.AddRepositories();

      Assert.Equal(2, collection.Count);
    }
  }
}
