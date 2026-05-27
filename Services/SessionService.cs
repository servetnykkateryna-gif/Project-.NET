using VelvetRelics.Data;
using VelvetRelics.Models;

namespace VelvetRelics.Services;

/// <summary>
/// Сервіс для управління сесією користувача.
/// Відповідає за збереження/читання токена та даних юзера з SQLite.
/// Це "клей" між AuthenticationService та базою даних.
/// </summary>
public class SessionService
{
    private readonly VelvetRelicsDatabase _database;

    // Кеш поточної сесії у пам'яті, щоб не читати з диска щоразу
    private UserSession? _cachedSession;

    public SessionService(VelvetRelicsDatabase database)
    {
        _database = database;
    }

    /// <summary>
    /// Зберегти токен та дані користувача після входу.
    /// </summary>
    public async Task SaveSessionAsync(AuthResponse authResponse)
    {
        if (authResponse.User is null)
            return;

        var session = new UserSession
        {
            UserId = authResponse.User.Id,
            Name = authResponse.User.Name,
            Email = authResponse.User.Email,
            Token = authResponse.Token,
            CreatedAt = DateTime.Now
        };

        await _database.SaveSessionAsync(session);
        _cachedSession = session;
    }

    /// <summary>
    /// Отримати поточного залогіненого користувача.
    /// </summary>
    public async Task<UserSession?> GetCurrentSessionAsync()
    {
        // Спочатку перевіряємо кеш
        if (_cachedSession is not null)
            return _cachedSession;

        // Якщо кешу немає — читаємо з бази
        _cachedSession = await _database.GetCurrentSessionAsync();
        return _cachedSession;
    }

    /// <summary>
    /// Отримати JWT токен для API запитів.
    /// </summary>
    public async Task<string?> GetTokenAsync()
    {
        var session = await GetCurrentSessionAsync();
        return session?.Token;
    }

    /// <summary>
    /// Перевірити, чи залогінений користувач.
    /// </summary>
    public async Task<bool> IsLoggedInAsync()
    {
        return await _database.IsLoggedInAsync();
    }

    /// <summary>
    /// Вийти з системи: очистити базу та кеш.
    /// </summary>
    public async Task LogoutAsync()
    {
        await _database.ClearSessionAsync();
        _cachedSession = null;
    }
}
