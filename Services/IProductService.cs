using VelvetRelics.Models;

namespace VelvetRelics.Services;

/// <summary>
/// Інтерфейс для роботи з каталогом антикваріату.
/// </summary>
public interface IProductService
{
    /// <summary>
    /// Отримати всі товари
    /// </summary>
    Task<IEnumerable<Product>> GetProductsAsync();

    /// <summary>
    /// Отримати товари з певної категорії
    /// </summary>
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category);

    /// <summary>
    /// Отримати деталі одного конкретного товару за його ID
    /// </summary>
    Task<Product?> GetProductByIdAsync(int id);

    /// <summary>
    /// Здійснити пошук товарів за текстом (по назві або опису)
    /// </summary>
    Task<IEnumerable<Product>> SearchProductsAsync(string query);
}
