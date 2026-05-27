using VelvetRelics.Data;
using VelvetRelics.Models;

namespace VelvetRelics.Services;

public class CartService : ICartService
{
    private readonly VelvetRelicsDatabase _database;
    private readonly IProductService _productService;

    public CartService(VelvetRelicsDatabase database, IProductService productService)
    {
        _database = database;
        _productService = productService;
    }

    public async Task<List<CartItemDto>> GetCartItemsAsync()
    {
        var cartItems = await _database.GetCartItemsAsync();
        var allProducts = await _productService.GetProductsAsync();

        var result = new List<CartItemDto>();
        
        foreach (var item in cartItems)
        {
            var product = allProducts.FirstOrDefault(p => p.Id == item.ProductId);
            if (product != null)
            {
                result.Add(new CartItemDto
                {
                    Product = product,
                    Quantity = item.Quantity
                });
            }
        }

        return result;
    }

    public async Task AddToCartAsync(int productId, int quantity = 1)
    {
        await _database.AddToCartAsync(productId, quantity);
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        await _database.RemoveFromCartAsync(productId);
    }

    public async Task ClearCartAsync()
    {
        await _database.ClearCartAsync();
    }

    public async Task<decimal> GetCartTotalAsync()
    {
        var items = await GetCartItemsAsync();
        return items.Sum(i => i.TotalPrice);
    }
}
