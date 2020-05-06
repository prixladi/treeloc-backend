using System;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using TreeLoc.Loader.Configs;
using TreeLoc.Loader.Repositories;
using TreeLoc.Loader.Services;
using Xunit;

namespace TreeLoc.Loader.UnitTests.Services
{
  public class DiscoveryServiceTest
  {
    private readonly IHttpService fHttpService;
    private readonly IResourcesRepository fResourcesRepository;
    private readonly IDiscoveryServiceConfig fConfig;

    private readonly DiscoveryService fService;

    public DiscoveryServiceTest()
    {
      fHttpService = Substitute.For<IHttpService>();
      fResourcesRepository = Substitute.For<IResourcesRepository>();
      fConfig = Substitute.For<IDiscoveryServiceConfig>();

      fService = new DiscoveryService(fHttpService, fResourcesRepository, fConfig);
    }

    [Fact]
    public async Task RunService_Success_TestAsync()
    {
      fConfig
        .Interval
        .Returns(TimeSpan.FromSeconds(7));

      fConfig
        .Url
        .Returns("http://dot.com/");

      fHttpService
        .DiscoveryAsync(Arg.Is<Uri>(x => x.AbsoluteUri == "http://dot.com/"), Arg.Any<CancellationToken>())
        .Returns(new string[] { "https://dot.eu/" });

      await fService.StartAsync(default);
      await Task.Delay(600);

      await fResourcesRepository
        .Received()
        .AddAsync(Arg.Is("https://dot.eu/"), Arg.Any<CancellationToken>());

      await fService.StopAsync(default);
      await fService.StopAsync(default);
    }

    [Fact]
    public async Task RunService_Exception_TestAsync()
    {
      fConfig
        .Interval
        .Returns(TimeSpan.FromSeconds(7));

      fConfig
        .Url
        .Returns("http://dot.com/");

      fHttpService
        .DiscoveryAsync(Arg.Is<Uri>(x => x.AbsoluteUri == "http://dot.com/"), Arg.Any<CancellationToken>())
        .Returns(new string[] { "https://dot.eu/" });

      fResourcesRepository
        .When(x => x.AddAsync(Arg.Is("https://dot.eu/"), Arg.Any<CancellationToken>()))
        .Throw<Exception>();

      await fService.StartAsync(default);
      await Task.Delay(600);

      await fService.StopAsync(default);
    }
  }
}
