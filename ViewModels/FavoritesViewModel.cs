using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Models;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class FavoritesViewModel : BaseViewModel
{
    private readonly IFavoritesService _favoritesService;

    [ObservableProperty]
    private ObservableCollection<Product> favoriteProducts = new();

    [ObservableProperty]
    private bool isEmpty = true;

    public FavoritesViewModel(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
        Title = "Обране";
    }

    [RelayCommand]
    public async Task LoadFavoritesAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            FavoriteProducts.Clear();
            
            var favorites = await _favoritesService.GetFavoriteProductsAsync();
            foreach (var product in favorites)
            {
                FavoriteProducts.Add(product);
            }

            IsEmpty = FavoriteProducts.Count == 0;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", $"Не вдалося завантажити улюблені товари: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task RemoveFromFavoritesAsync(Product product)
    {
        if (product == null) return;

        await _favoritesService.RemoveFromFavoritesAsync(product.Id);
        FavoriteProducts.Remove(product);
        IsEmpty = FavoriteProducts.Count == 0;
    }

    [RelayCommand]
    private async Task GoToDetailsAsync(Product product)
    {
        if (product is null) return;
        await Shell.Current.GoToAsync($"ProductDetailsPage?ProductId={product.Id}");
    }

    /// <summary>
    /// Переходить на вкладку каталогу з порожнього стану обраного.
    /// </summary>
    [RelayCommand]
    private async Task GoToCatalogAsync()
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
}
