using SQLite;

namespace VelvetRelics.Models;

/// <summary>
/// Таблиця в SQLite для збереження сесії користувача.
/// Атрибут [Table] визначає назву таблиці в базі даних.
/// </summary>
[Table("UserSession")]
public class UserSession
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>
    /// ID користувача з API
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Ім'я користувача
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email користувача
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// JWT токен для авторизованих запитів до API
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Час, коли сесія була збережена
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
