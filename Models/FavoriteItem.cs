using SQLite;

namespace VelvetRelics.Models;

/// <summary>
/// Сутність, що репрезентує товар, який користувач додав до улюблених.
/// Зберігається в локальній SQLite базі.
/// </summary>
public class FavoriteItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int ProductId { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
