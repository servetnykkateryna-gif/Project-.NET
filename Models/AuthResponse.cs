namespace VelvetRelics.Models;

public class AuthResponse
{
    public User? User { get; set; }
    public string Token { get; set; } = string.Empty;
    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
}
