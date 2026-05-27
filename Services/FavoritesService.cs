using VelvetRelics.Data;
using VelvetRelics.Models;

namespace VelvetRelics.Services;

public class FavoritesService : IFavoritesService
{
    private readonly VelvetRelicsDatabase _database;
    private readonly IProductService _productService;

    public FavoritesService(VelvetRelicsDatabase database, IProductService productService)
    {
        _database = database;
        _productService = productService;
    }

    public async Task<List<Product>> GetFavoriteProductsAsync()
    {
        var favoriteIds = await _database.GetFavoriteProductIdsAsync();
        
        // Отримуємо всі товари та фільтруємо ті, що є в улюблених
        var allProducts = await _productService.GetProductsAsync();
        
        return allProducts.Where(p => favoriteIds.Contains(p.Id)).ToList();
    }

    public async Task AddToFavoritesAsync(int productId)
    {
        await _database.AddToFavoritesAsync(productId);
    }

    public async Task RemoveFromFavoritesAsync(int productId)
    {
        await _database.RemoveFromFavoritesAsync(productId);
    }

    public async Task<bool> IsFavoriteAsync(int productId)
    {
        return await _database.IsFavoriteAsync(productId);
    }
}
