using backend.Actions.Request;

namespace backend.Actions;

public static class UsersManagementExtensions
{
  public static IEndpointRouteBuilder MapUsersManagementRoutes(this IEndpointRouteBuilder builder)
    => builder
      .MapGroup($"/users-management")
      .MapRoutes()
      .WithOpenApi();

  private static RouteGroupBuilder MapRoutes(this RouteGroupBuilder group)
  {
    group.MapLoginRoute();
    group.MapGetRoute();
    group.MapRemoveRoute();
    group.MapGetAllRoute();

    return group;
  }
}
