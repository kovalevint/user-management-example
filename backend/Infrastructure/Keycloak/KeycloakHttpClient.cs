using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace backend.Infrastructure.Keycloak;

public sealed class KeycloakHttpClient
{
  public const string TokenEndpointPath = "protocol/openid-connect/token";
  
  private readonly HttpClient _httpClient;
  private readonly KeycloakOptions _options;

  public KeycloakHttpClient(
    HttpClient httpClient,
    IOptions<KeycloakOptions> options)
  {
    _httpClient = httpClient;
    _options = options.Value;

    _httpClient.BaseAddress = new Uri(options.Value.Host);
  }

  public async Task<Result<TokenResponse>> LoginAsync(
    string email,
    string password,
    CancellationToken cancellationToken)
  {
    var keycloakTokenUrl = $"/realms/{_options.RealmName}/protocol/openid-connect/token";
    
    var content = new FormUrlEncodedContent(new[]
    {
      new KeyValuePair<string, string>("client_id", _options.ClientId),
      new KeyValuePair<string, string>("grant_type", "password"),
      new KeyValuePair<string, string>("username", email),
      new KeyValuePair<string, string>("password", password),
      new KeyValuePair<string, string>("client_secret", _options.ClientSecret)
    });
    
    var response = await _httpClient.PostAsync(keycloakTokenUrl, content, cancellationToken);
    
    if (!response.IsSuccessStatusCode)
    {
      var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
      return ResultFactory.Failed<TokenResponse>(ErrorCodes.UnauthorizedError(errorContent));
    }

    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);

    return tokenResponse is null
      ? ResultFactory.Failed<TokenResponse>(ErrorCodes.EmptyDeserializationModelError<TokenResponse>())
      : ResultFactory.Successful(tokenResponse);
  }

  public async Task<Result<UserResponse>> GetByEmailAsync(
    string email,
    CancellationToken cancellationToken)
  {
    var adminTokenResult = await GetAdminToken(cancellationToken);
    
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTokenResult.Data!.AccessToken);
    
    var keycloakUrl = $"/admin/realms/{_options.RealmName}/users?email={email}";
    var response = await _httpClient.GetAsync(keycloakUrl, cancellationToken);
    
    if (!response.IsSuccessStatusCode)
    {
      return ResultFactory.Failed<UserResponse>(ErrorCodes.UserNotFoundError(email));
    }

    var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>(cancellationToken);

    return userResponse is null
      ? ResultFactory.Failed<UserResponse>(ErrorCodes.EmptyDeserializationModelError<UserResponse>())
      : ResultFactory.Successful(userResponse);
  }
  
  public async Task<Result<IReadOnlyCollection<UserResponse>>> GetAllAsync(
    CancellationToken cancellationToken)
  {
    var adminTokenResult = await GetAdminToken(cancellationToken);
    
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTokenResult.Data!.AccessToken);
    
    var keycloakUrl = $"/admin/realms/{_options.RealmName}/users";
    var response = await _httpClient.GetAsync(keycloakUrl, cancellationToken);
    
    if (!response.IsSuccessStatusCode)
    {
      var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
      return ResultFactory.Failed<IReadOnlyCollection<UserResponse>>(ErrorCodes.UnauthorizedError(errorContent));
    }

    var userResponse = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<UserResponse>>(cancellationToken);

    return userResponse is null
      ? ResultFactory.Failed<IReadOnlyCollection<UserResponse>>(ErrorCodes.EmptyDeserializationModelError<UserResponse>())
      : ResultFactory.Successful(userResponse);
  }
  
  public async Task<Result<Empty>> DeleteAsync(
    string id,
    CancellationToken cancellationToken)
  {
    var adminTokenResult = await GetAdminToken(cancellationToken);
    
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminTokenResult.Data!.AccessToken);
    
    var keycloakUrl = $"/admin/realms/{_options.RealmName}/users/{id}";
    var response = await _httpClient.DeleteAsync(keycloakUrl, cancellationToken);
    
    if (!response.IsSuccessStatusCode)
    {
      return ResultFactory.Failed<Empty>(ErrorCodes.UserNotFoundError(id));
    }

    return ResultFactory.Successful(new Empty());
  }
  
  private async Task<Result<TokenResponse>> GetAdminToken(CancellationToken cancellationToken)
  {
    using var httpClient = new HttpClient();
    
    var keycloakTokenUrl = $"/realms/{_options.RealmName}/protocol/openid-connect/token";
    
    var content = new FormUrlEncodedContent(new[]
    {
      new KeyValuePair<string, string>("client_id", "sdk-client"),
      new KeyValuePair<string, string>("grant_type", "client_credentials"),
      new KeyValuePair<string, string>("client_secret", _options.ClientSecret)
    });
    
    var response = await httpClient.PostAsync(keycloakTokenUrl, content, cancellationToken);
    if (!response.IsSuccessStatusCode)
    {
      var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
      return ResultFactory.Failed<TokenResponse>(ErrorCodes.UnauthorizedError(errorContent));
    }
    
    var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>(cancellationToken);
    
    return tokenResponse is null
      ? ResultFactory.Failed<TokenResponse>(ErrorCodes.EmptyDeserializationModelError<TokenResponse>())
      : ResultFactory.Successful(tokenResponse);
  }
}

public record TokenResponse(string AccessToken, string RefreshToken, string ExpiresIn);
public record UserResponse(string Id, string Email, string FirstName);
