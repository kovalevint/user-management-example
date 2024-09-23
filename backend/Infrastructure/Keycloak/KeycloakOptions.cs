namespace backend.Infrastructure.Keycloak;

public sealed class KeycloakOptions
{
  public const string SectionName = "Keycloak";

  public required string Host { get; init; }
  public required string RealmName { get; init; }
  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }
}
