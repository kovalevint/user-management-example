using System.Diagnostics.CodeAnalysis;

namespace backend.Infrastructure;

public readonly struct Result<TData>
{
  public Result(TData data)
  {
    Data = data;
    Error = null;
  }

  public Result(Error error)
  {
    Data = default;
    Error = error;
  }

  public TData? Data { get; }

  public Error? Error { get; }

  [MemberNotNullWhen(true, nameof(Data))]
  [MemberNotNullWhen(false, nameof(Error))]
  public bool IsSuccessful => Error == null;
}

public readonly struct Result
{
  public Result(Error? error)
  {
    Error = error;
  }

  public Error? Error { get; }

  [MemberNotNullWhen(false, nameof(Error))]
  public bool IsSuccessful => Error == null;
}

public static class ResultFactory
{
  public static Result<TData> Successful<TData>(TData data)
    => new(data);

  public static Result<TData> Failed<TData>(Error error)
    => new(error);
}
