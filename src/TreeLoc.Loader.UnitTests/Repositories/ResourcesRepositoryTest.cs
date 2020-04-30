using System;
using TreeLoc.Loader.Repositories;
using Xunit;

namespace TreeLoc.Loader.UnitTests.Repositories
{
  public class ResourcesRepositoryTest
  {
    private readonly ResourcesRepository fRepository;

    public ResourcesRepositoryTest()
    {
      fRepository = new ResourcesRepository();
    }

    [Fact]
    public void Test()
    {
      fRepository.Add(new Uri("http://dot.com/"));
      fRepository.Add(new Uri("https://dot.com/"));

      Assert.Equal(2, fRepository.GetFalse().Length);
      fRepository.SetTrue(new Uri("https://dot.com/"));

      var uri = Assert.Single(fRepository.GetFalse());

      Assert.Equal("http://dot.com/", uri.AbsoluteUri);
    }
  }
}
