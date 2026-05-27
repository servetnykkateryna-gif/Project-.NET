using CommunityToolkit.Mvvm.ComponentModel;
using VelvetRelics.Models;

namespace VelvetRelics.Services;

public partial class CartItemDto : ObservableObject
{
    public Product Product { get; set; } = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TotalPrice))]
    private int quantity;

    public decimal TotalPrice => Product.Price * Quantity;
}

public interface ICartService
{
    Task<List<CartItemDto>> GetCartItemsAsync();
    Task AddToCartAsync(int productId, int quantity = 1);
    Task RemoveFromCartAsync(int productId);
    Task UpdateQuantityAsync(int productId, int quantity);
    Task ClearCartAsync();
    Task<decimal> GetCartTotalAsync();
}
