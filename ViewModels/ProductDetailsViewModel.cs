using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Models;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

/// <summary>
/// ViewModel для екрану детальної інформації про антикварний виріб.
/// Приймає параметр ProductId через Shell-навігацію та підвантажує відповідний товар.
/// </summary>
[QueryProperty(nameof(ProductId), "ProductId")]
public partial class ProductDetailsViewModel : BaseViewModel
{
    private readonly IProductService _productService;
    private readonly IFavoritesService _favoritesService;
    private readonly ICartService _cartService;

    [ObservableProperty]
    private Product? product;

    [ObservableProperty]
    private bool isFavorite;

    private int _productId;

    /// <summary>
    /// Властивість ProductId автоматично викликає підвантаження деталей товару при її зміні навігатором Shell.
    /// </summary>
    public int ProductId
    {
        get => _productId;
        set
        {
            SetProperty(ref _productId, value);
            LoadProductDetailsAsync(value);
        }
    }

    public ProductDetailsViewModel(IProductService productService, IFavoritesService favoritesService, ICartService cartService)
    {
        _productService = productService;
        _favoritesService = favoritesService;
        _cartService = cartService;
        Title = "Деталі шедевра";
    }

    /// <summary>
    /// Метод завантаження інформації про товар за його ID
    /// </summary>
    private async void LoadProductDetailsAsync(int id)
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            var loadedProduct = await _productService.GetProductByIdAsync(id);
            if (loadedProduct is not null)
            {
                Product = loadedProduct;
                Title = Product.Name;
                
                // Перевіряємо, чи товар вже є в улюблених
                IsFavorite = await _favoritesService.IsFavoriteAsync(id);
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка", "Товар не знайдено в каталозі.", "OK");
                await GoBackAsync();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", $"Не вдалося завантажити деталі: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Навігація назад
    /// </summary>
    [RelayCommand]
    private async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    /// <summary>
    /// Команда для додавання або видалення з обраного
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavoriteAsync()
    {
        if (Product is null)
            return;

        if (IsFavorite)
        {
            await _favoritesService.RemoveFromFavoritesAsync(Product.Id);
            IsFavorite = false;
        }
        else
        {
            await _favoritesService.AddToFavoritesAsync(Product.Id);
            IsFavorite = true;
        }
    }

    /// <summary>
    /// Команда для додавання в кошик
    /// </summary>
    [RelayCommand]
    private async Task AddToCartAsync()
    {
        if (Product is null)
            return;

        await _cartService.AddToCartAsync(Product.Id, 1);
        await Shell.Current.DisplayAlert("Кошик", $"«{Product.Name}» успішно додано до вашого кошика замовлень.", "OK");
    }
}
