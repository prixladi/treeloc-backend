using System;
using TreeLoc.Api.Validation;
using Xunit;

namespace Treeloc.Api.UnitTests.Validation
{
  public class AllowedValuesAttributeTest
  {
    [Theory]
    [InlineData("test1")]
    [InlineData("test2")]
    [InlineData(null)]
    public void IsValid_Success_Test(object value)
    {
      var attribute = new AllowedValuesAttribute("test1", "test2") { AllowNull = true };

      Assert.True(attribute.IsValid(value));
    }

    [Theory]
    [InlineData("test3")]
    [InlineData(null)]
    public void IsValid_Fail_Test(object value)
    {
      var attribute = new AllowedValuesAttribute("5", "test2") { AllowNull = false };

      Assert.False(attribute.IsValid(value));
    }

    [Theory]
    [InlineData(5)]
    [InlineData(true)]
    public void IsValid_Exception_Test(object value)
    {
      var attribute = new AllowedValuesAttribute("5", "test2") { AllowNull = false };

      Assert.Throws<ArgumentException>(() => attribute.IsValid(value));
    }

    [Fact]
    public void FormatMessage_Exception_Test()
    {
      var attribute = new AllowedValuesAttribute("5", "test2") { AllowNull = false };

      Assert.Equal(
        $"The value of field 'test' is not one of allowed values [5, test2].",
        attribute.FormatErrorMessage("test"));
    }
  }
}
