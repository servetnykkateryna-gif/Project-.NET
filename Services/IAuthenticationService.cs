using VelvetRelics.Models;

namespace VelvetRelics.Services;

public interface IAuthenticationService
{
    Task<AuthResponse> LoginAsync(string email, string password);
    Task<AuthResponse> RegisterAsync(string name, string email, string password);
}
