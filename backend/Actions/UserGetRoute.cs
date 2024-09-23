using backend.Contracts;
using backend.Infrastructure;
using backend.Infrastructure.Keycloak;

namespace backend.Actions;

public static class UserGetRoute
{
  public static void MapGetRoute(this IEndpointRouteBuilder route)
    => route.MapGet("/{email}", ExecuteAsync)
      .Produces<ServiceResponse<UserViewModel>>()
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status401Unauthorized)
      .Produces(StatusCodes.Status404NotFound);

  private static async ValueTask<IResult> ExecuteAsync(
    string email,
    KeycloakHttpClient httpClient,
    CancellationToken cancellationToken)
  {
    var getUserByEmailResult = await httpClient.GetByEmailAsync(email, cancellationToken);
    return getUserByEmailResult switch
    {
      { IsSuccessful: false, Error: NotFoundError } => Results.NotFound(getUserByEmailResult.Error),
      { IsSuccessful: false } => Results.BadRequest(getUserByEmailResult.Error),
      _ => Results.Ok(getUserByEmailResult.Data)
    };
  }
}
