using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Models;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class CatalogViewModel : BaseViewModel
{
    private readonly IProductService _productService;

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private ObservableCollection<string> categories = new();

    [ObservableProperty]
    private string selectedCategory = "Всі";

    public CatalogViewModel(IProductService productService)
    {
        _productService = productService;
        Title = "Velvet Relics";

        // Ініціалізуємо список категорій для фільтрації
        Categories = new ObservableCollection<string>
        {
            "Всі",
            "Картини",
            "Старовинні книги",
            "Монети",
            "Меблі",
            "Прикраси",
            "Годинники"
        };
    }

    /// <summary>
    /// Команда завантаження товарів
    /// </summary>
    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Products.Clear();

            IEnumerable<Product> loadedProducts;

            if (SelectedCategory == "Всі")
            {
                loadedProducts = await _productService.GetProductsAsync();
            }
            else
            {
                // Наш сервіс приймає категорії в нижньому регістрі
                string categoryKey = SelectedCategory.ToLower();
                loadedProducts = await _productService.GetProductsByCategoryAsync(categoryKey);
            }

            foreach (var product in loadedProducts)
            {
                Products.Add(product);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", $"Не вдалося завантажити каталог: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Команда для вибору категорії та фільтрації товарів
    /// </summary>
    [RelayCommand]
    private async Task SelectCategoryAsync(string category)
    {
        if (SelectedCategory == category)
            return;

        SelectedCategory = category;
        await LoadProductsAsync();
    }

    /// <summary>
    /// Команда переходу на сторінку деталей
    /// </summary>
    [RelayCommand]
    private async Task GoToDetailsAsync(Product product)
    {
        if (product is null)
            return;

        // Передаємо ID товару через параметри маршруту для Етапу 7
        await Shell.Current.GoToAsync($"ProductDetailsPage?ProductId={product.Id}");
    }
}
