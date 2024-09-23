using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace backend.Infrastructure;

[ExcludeFromCodeCoverage]
[method: JsonConstructor]
public class Error(string code, string message)
{
  public Error(ErrorCodeInfo info)
    : this(info.Code, info.Message)
  {
  }

  public Error(ErrorCodeInfo info, string customMessage)
    : this(info.Code, customMessage)
  {
  }

  public string Code { get; } = code;

  public string Message { get; } = message;

  public string Target { get; set; } = string.Empty;

  public List<ErrorDetail> Details { get; set; } = new();
}
