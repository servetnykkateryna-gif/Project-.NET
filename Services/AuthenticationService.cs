using System.Net.Http.Json;
using System.Text.Json;
using VelvetRelics.Models;

namespace VelvetRelics.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    // URL вашого бекенду. Наприклад: "https://api.velvetrelics.com"
    private const string BaseUrl = "https://jsonplaceholder.typicode.com"; // Використовуємо dummy URL для прикладу

    public AuthenticationService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(BaseUrl)
        };
    }

    public async Task<AuthResponse> LoginAsync(string email, string password)
    {
        try
        {
            // Імітація затримки мережі
            await Task.Delay(1500);

            // ТУТ МАЄ БУТИ РЕАЛЬНИЙ ВИКЛИК API:
            // var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new { Email = email, Password = password });
            // if (response.IsSuccessStatusCode) ...

            // Fake API Response для розробки — приймаємо будь-які дані
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthResponse { IsSuccess = false, ErrorMessage = "Email та пароль обов'язкові." };
            }

            // ✅ Успішний вхід з будь-якими даними (до підключення реального API)
            return new AuthResponse
            {
                IsSuccess = true,
                Token = $"fake-jwt-token-{Guid.NewGuid():N}",
                User = new User { Id = 1, Name = email.Split('@')[0], Email = email }
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse { IsSuccess = false, ErrorMessage = $"Помилка мережі: {ex.Message}" };
        }
    }

    public async Task<AuthResponse> RegisterAsync(string name, string email, string password)
    {
        try
        {
            await Task.Delay(1500);

            // ТУТ МАЄ БУТИ РЕАЛЬНИЙ ВИКЛИК API:
            // var response = await _httpClient.PostAsJsonAsync("/api/auth/register", new { Name = name, Email = email, Password = password });

            // Fake API Response
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return new AuthResponse { IsSuccess = false, ErrorMessage = "Всі поля обов'язкові." };
            }

            return new AuthResponse
            {
                IsSuccess = true,
                Token = "fake-jwt-token-67890",
                User = new User { Id = 2, Name = name, Email = email }
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse { IsSuccess = false, ErrorMessage = $"Помилка мережі: {ex.Message}" };
        }
    }
}
