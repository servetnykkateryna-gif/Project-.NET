namespace VelvetRelics.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    // Для реального проекту пароль не зберігається в моделі на клієнті,
    // але для наочності та тестування ми додамо його
    public string Password { get; set; } = string.Empty;
}
