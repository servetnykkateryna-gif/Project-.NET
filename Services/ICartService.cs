using VelvetRelics.Models;

namespace VelvetRelics.Services;

public class CartItemDto
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal TotalPrice => Product.Price * Quantity;
}

public interface ICartService
{
    Task<List<CartItemDto>> GetCartItemsAsync();
    Task AddToCartAsync(int productId, int quantity = 1);
    Task RemoveFromCartAsync(int productId);
    Task ClearCartAsync();
    Task<decimal> GetCartTotalAsync();
}
