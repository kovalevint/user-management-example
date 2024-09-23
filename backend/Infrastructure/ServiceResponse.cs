namespace backend.Infrastructure;

public class ServiceResponse
{
  public Error? Error { get; init; }
}

public class ServiceResponse<T>
  where T : class
{
  public ServiceResponse()
  {
  }

  public ServiceResponse(T data, Error? error = null)
  {
    Data = data;
    Error = error;
  }

  public T? Data { get; init; }

  public Error? Error { get; init; }
}

public sealed class ErrorServiceResponse(Error error)
{
  public Error Error { get; } = error;
}
