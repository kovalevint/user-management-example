using backend.Contracts;
using backend.Infrastructure;
using backend.Infrastructure.Keycloak;

namespace backend.Actions;

public static class UserGetAllRoute
{
  public static void MapGetAllRoute(this IEndpointRouteBuilder route)
    => route.MapGet("/", ExecuteAsync)
      .Produces<ServiceResponse<ICollection<UserViewModel>>>()
      .Produces(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status401Unauthorized);

  private static async ValueTask<IResult> ExecuteAsync(
    KeycloakHttpClient httpClient,
    CancellationToken cancellationToken)
  {
    var getAllUsersResult = await httpClient.GetAllAsync(cancellationToken);
    return getAllUsersResult switch
    {
      { IsSuccessful: false } => Results.BadRequest(getAllUsersResult.Error),
      _ => Results.Ok(getAllUsersResult.Data)
    };
  }
}
