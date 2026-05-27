using VelvetRelics.Models;

namespace VelvetRelics.Services;

public interface IFavoritesService
{
    Task<List<Product>> GetFavoriteProductsAsync();
    Task AddToFavoritesAsync(int productId);
    Task RemoveFromFavoritesAsync(int productId);
    Task<bool> IsFavoriteAsync(int productId);
}
