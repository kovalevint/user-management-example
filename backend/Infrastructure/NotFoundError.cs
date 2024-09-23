namespace backend.Infrastructure;

public class NotFoundError : Error
{
  public NotFoundError(ErrorCodeInfo info) : base(info)
  {
  }
  public NotFoundError(ErrorCodeInfo info, string customMessage) : base(info, customMessage)
  {
  }
  public NotFoundError(string code, string message) : base(code, message)
  {
  }
}
