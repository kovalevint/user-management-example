using System.Diagnostics.CodeAnalysis;

namespace backend.Infrastructure;

[ExcludeFromCodeCoverage]
public sealed class ErrorCodeInfo(string code, string message)
{
  public ErrorCodeInfo(string code)
    : this(code, string.Empty)
  {
  }

  public string Code { get; } = code;

  public string Message { get; } = message;

  public override string ToString()
  {
    return $"{Code}: '{Message}'.";
  }
}
