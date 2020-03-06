using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TreeLoc.Api.Controllers;
using TreeLoc.Api.Models;
using Xunit;

namespace Treeloc.Api.UnitTests.Controllers
{
  public class InfoControllerTest
  {
    [Fact]
    public void Get_Test()
    {
      var controller = new InfoController();

      var result = controller.Get();
      var objResult = Assert.IsType<OkObjectResult>(result);
      Assert.IsType<InfoModel>(objResult.Value);
    }
  }
}
