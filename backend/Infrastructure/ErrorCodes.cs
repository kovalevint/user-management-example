namespace backend.Infrastructure;

public static class ErrorCodes
{
  public const string EmptyDeserializationModelCode = nameof(EmptyDeserializationModelCode);
  public const string UnauthorizedCode = nameof(UnauthorizedCode);
  public const string UserNotFoundCode = nameof(UserNotFoundCode);

  public static Error EmptyDeserializationModelError<T>() => new(EmptyDeserializationModelCode, $"Empty deserialization model: {typeof(T).Name}");
  public static Error UnauthorizedError(string errorMessage) => new(UnauthorizedCode, $"Unauthorized response. Message: {errorMessage}");
  public static NotFoundError UserNotFoundError(string email) => new(UserNotFoundCode, $"User not found by email: {email}");
}
