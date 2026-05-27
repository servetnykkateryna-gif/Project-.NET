namespace VelvetRelics.Models;

/// <summary>
/// Модель антикварного товару.
/// </summary>
public class Product
{
    public int Id { get; set; }
    
    /// <summary>
    /// Назва антикварного виробу
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Детальний художній опис предмету
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Рік або століття створення предмету (наприклад: 1892, XVII ст.)
    /// </summary>
    public string Year { get; set; } = string.Empty;

    /// <summary>
    /// Категорія товару: картини, старовинні книги, монети, меблі, прикраси, годинники
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Вартість товару в USD або EUR (зберігаємо у десятковому форматі для точності фінансів)
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// URL посилання на преміальне зображення товару
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;
}
