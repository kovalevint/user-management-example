using System.ComponentModel.DataAnnotations;
using backend.Actions.Request;
using backend.Contracts;
using backend.Infrastructure;
using backend.Infrastructure.Keycloak;
using Microsoft.AspNetCore.Mvc;

namespace backend.Actions;

public static class UserLoginRoute
{
  public static void MapLoginRoute(this IEndpointRouteBuilder route)
    => route.MapPost("/login", ExecuteAsync)
      .Produces<ServiceResponse<TokenViewModel>>()
      .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status401Unauthorized);

  private static async ValueTask<IResult> ExecuteAsync(
    [Required, FromBody] LoginRequest request,
    KeycloakHttpClient httpClient,
    CancellationToken cancellationToken)
  {
    var loginResult = await httpClient.LoginAsync(request.Email, request.Password, cancellationToken);
    return !loginResult.IsSuccessful ? Results.Unauthorized() : Results.Ok(loginResult.Data);
  }
}
