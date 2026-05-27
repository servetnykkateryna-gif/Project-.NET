using SQLite;
using VelvetRelics.Models;

namespace VelvetRelics.Data;

/// <summary>
/// Головний клас для роботи з локальною SQLite базою даних.
/// Він відповідає за створення таблиць та надання методів для CRUD операцій.
/// </summary>
public class VelvetRelicsDatabase
{
    private SQLiteAsyncConnection? _database;

    // Шлях до файлу бази даних на пристрої
    private static readonly string DbPath = Path.Combine(
        FileSystem.AppDataDirectory,
        "VelvetRelics.db3"
    );

    // Список всіх таблиць, які потрібно створити
    private static readonly Type[] Tables =
    {
        typeof(UserSession)
        // У наступних етапах тут додамо FavoriteItem, CartItem
    };

    /// <summary>
    /// Ліниве (lazy) підключення до бази. Відкривається лише коли потрібно вперше.
    /// </summary>
    private async Task<SQLiteAsyncConnection> GetDatabaseAsync()
    {
        if (_database is not null)
            return _database;

        _database = new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        // Створюємо всі таблиці, якщо вони ще не існують
        await _database.CreateTablesAsync(CreateFlags.None, Tables);

        return _database;
    }

    // ─── UserSession Methods ─────────────────────────────────────────────────────

    /// <summary>
    /// Зберегти сесію користувача після успішного входу.
    /// </summary>
    public async Task SaveSessionAsync(UserSession session)
    {
        var db = await GetDatabaseAsync();
        // Спочатку видаляємо стару сесію (якщо є)
        await db.DeleteAllAsync<UserSession>();
        // Зберігаємо нову
        await db.InsertAsync(session);
    }

    /// <summary>
    /// Отримати поточну збережену сесію.
    /// Повертає null, якщо сесії немає (користувач не увійшов).
    /// </summary>
    public async Task<UserSession?> GetCurrentSessionAsync()
    {
        var db = await GetDatabaseAsync();
        return await db.Table<UserSession>().FirstOrDefaultAsync();
    }

    /// <summary>
    /// Видалити сесію (вихід з акаунту).
    /// </summary>
    public async Task ClearSessionAsync()
    {
        var db = await GetDatabaseAsync();
        await db.DeleteAllAsync<UserSession>();
    }

    /// <summary>
    /// Перевірити, чи є активна сесія (чи увійшов користувач).
    /// </summary>
    public async Task<bool> IsLoggedInAsync()
    {
        var session = await GetCurrentSessionAsync();
        return session is not null && !string.IsNullOrEmpty(session.Token);
    }
}
