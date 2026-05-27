using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly ICartService _cartService;

    [ObservableProperty]
    private ObservableCollection<CartItemDto> cartItems = new();

    [ObservableProperty]
    private decimal totalPrice;

    [ObservableProperty]
    private bool isEmpty = true;

    [ObservableProperty]
    private bool isNotEmpty = false;

    public CartViewModel(ICartService cartService)
    {
        _cartService = cartService;
        Title = "Кошик";
    }

    [RelayCommand]
    public async Task LoadCartAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            CartItems.Clear();
            
            var items = await _cartService.GetCartItemsAsync();
            foreach (var item in items)
            {
                CartItems.Add(item);
            }

            UpdateTotals();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", $"Не вдалося завантажити кошик: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task RemoveFromCartAsync(CartItemDto item)
    {
        if (item == null) return;

        await _cartService.RemoveFromCartAsync(item.Product.Id);
        CartItems.Remove(item);
        UpdateTotals();
    }

    [RelayCommand]
    private async Task CheckoutAsync()
    {
        if (IsEmpty) return;

        await Shell.Current.DisplayAlert("Оформлення замовлення", 
            $"Ваше замовлення на суму ${TotalPrice:N0} успішно прийнято в обробку. Наш менеджер зв'яжеться з вами найближчим часом.", 
            "Чудово");
            
        await _cartService.ClearCartAsync();
        CartItems.Clear();
        UpdateTotals();
    }

    private void UpdateTotals()
    {
        TotalPrice = CartItems.Sum(i => i.TotalPrice);
        IsEmpty = CartItems.Count == 0;
        IsNotEmpty = !IsEmpty;
    }
}
