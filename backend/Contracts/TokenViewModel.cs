namespace backend.Contracts;

public record TokenViewModel(string AccessToken, string RefreshToken, string ExpiresIn);
