namespace backend.Infrastructure.Keycloak;

public static class DependencyInjection
{
  public static IServiceCollection AddKeycloakHttpClient(this IServiceCollection services)
  {
    services.AddHttpClient<KeycloakHttpClient>();

    return services;
  }
}
