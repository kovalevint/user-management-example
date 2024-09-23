using backend.Infrastructure;
using backend.Infrastructure.Keycloak;

namespace backend.Actions.Request;

public static class UserRemoveRoute
{
  public static void MapRemoveRoute(this IEndpointRouteBuilder route)
    => route.MapDelete("/{email}", ExecuteAsync)
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status404NotFound);

  private static async ValueTask<IResult> ExecuteAsync(
    string email,
    KeycloakHttpClient httpClient,
    CancellationToken cancellationToken)
  {
    var getUserByEmailResult = await httpClient.GetByEmailAsync(email, cancellationToken);
    if (!getUserByEmailResult.IsSuccessful)
    {
      return getUserByEmailResult.Error is NotFoundError
        ? Results.NotFound(getUserByEmailResult.Error)
        : Results.BadRequest(getUserByEmailResult.Error);
    }

    var removeUserResult = await httpClient.DeleteAsync(getUserByEmailResult.Data.Id, cancellationToken);
    return !removeUserResult.IsSuccessful ? Results.BadRequest(removeUserResult.Error) : Results.Empty;
  }
}
