using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TreeLoc.Api.Validation
{
  public class AllowedValuesAttribute: ValidationAttribute
  {
    private readonly string[] fValues;

    public bool AllowNull { get; set; }

    public AllowedValuesAttribute(params string[] values)
    {
      fValues = values;
    }

    public override bool IsValid(object value)
    {
      if (value == null)
        return AllowNull;

      if (value is string str)
        return fValues.Contains(str);

      throw new ArgumentException("Argument type mismatch.", nameof(value));
    }

    public override string FormatErrorMessage(string name)
    {
      return $"The value of field '{name}' is not one of allowed values [{string.Join(", ", fValues)}].";
    }
  }
}
