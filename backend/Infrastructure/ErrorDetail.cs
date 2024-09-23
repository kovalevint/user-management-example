using System.Text.Json.Serialization;

namespace backend.Infrastructure;

public sealed class ErrorDetail
{
  public const string TargetPathSeparator = ".";
  public const string TargetFakePrefix = "$";

  [JsonConstructor]
  public ErrorDetail(string code, string message, string target = "")
  {
    Code = code;
    Message = message;
    Target = MakeTargetCamelCase(RemoveFakePrefix(target));
  }

  public ErrorDetail(ErrorCodeInfo info, string target)
    : this(info.Code, info.Message, target)
  {
  }

  public ErrorDetail(ErrorCodeInfo info, string customMessage, string target)
    : this(info.Code, customMessage, target)
  {
  }

  public string Code { get; }

  public string Message { get; }

  public string Target { get; }

  private string RemoveFakePrefix(string target)
    => target.TrimStart(TargetFakePrefix.ToCharArray());

  private string MakeTargetCamelCase(string target)
  {
    if (string.IsNullOrWhiteSpace(target))
    {
      return target;
    }

    var parts = target.Split(TargetPathSeparator, StringSplitOptions.RemoveEmptyEntries);
    for (var i = 0; i < parts.Length; i++)
    {
      if (!char.IsUpper(parts[i][0]))
      {
        continue;
      }

      parts[i] = char.ToLowerInvariant(parts[i][0]) + parts[i].Substring(1);
    }

    return string.Join(TargetPathSeparator, parts);
  }
}
