using SQLite;

namespace VelvetRelics.Models;

/// <summary>
/// Сутність, що репрезентує товар у кошику покупок.
/// Зберігається в локальній SQLite базі даних.
/// </summary>
public class CartItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Indexed]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
