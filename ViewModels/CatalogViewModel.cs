using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Models;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class CatalogViewModel : BaseViewModel
{
    private readonly IProductService _productService;

    // Локальний кеш ВСІХ товарів для швидкої фільтрації без зайвих запитів до API
    private List<Product> _allProducts = new();

    [ObservableProperty]
    private ObservableCollection<Product> products = new();

    [ObservableProperty]
    private ObservableCollection<CategoryItemViewModel> categories = new();

    [ObservableProperty]
    private string selectedCategory = "Всі";

    [ObservableProperty]
    private string searchText = string.Empty;

    [ObservableProperty]
    private bool isSearchVisible = false;

    public CatalogViewModel(IProductService productService)
    {
        _productService = productService;
        Title = "Velvet Relics";

        Categories = new ObservableCollection<CategoryItemViewModel>
        {
            new() { Name = "Всі",             IsSelected = true  },
            new() { Name = "Картини",          IsSelected = false },
            new() { Name = "Старовинні книги", IsSelected = false },
            new() { Name = "Монети",           IsSelected = false },
            new() { Name = "Меблі",            IsSelected = false },
            new() { Name = "Прикраси",         IsSelected = false },
            new() { Name = "Годинники",        IsSelected = false },
        };
    }

    /// <summary>
    /// Завантажуємо товари з API та зберігаємо у локальний кеш
    /// </summary>
    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            // Завантажуємо ВСІ товари один раз та кешуємо
            var loaded = await _productService.GetProductsAsync();
            _allProducts = loaded.ToList();

            // Застосовуємо поточні фільтри до кешу
            ApplyFilters();
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
    /// Вибір категорії — оновлює IsSelected для кожного елемента та фільтрує локально.
    /// </summary>
    [RelayCommand]
    private void SelectCategory(CategoryItemViewModel selected)
    {
        if (selected is null || SelectedCategory == selected.Name)
            return;

        // Скидаємо IsSelected для всіх категорій
        foreach (var cat in Categories)
            cat.IsSelected = false;

        // Позначаємо обрану
        selected.IsSelected = true;
        SelectedCategory = selected.Name;
        ApplyFilters();
    }

    /// <summary>
    /// Текстовий пошук — викликається при зміні SearchText
    /// </summary>
    [RelayCommand]
    private void Search()
    {
        ApplyFilters();
    }

    /// <summary>
    /// Показати/сховати пошукову панель
    /// </summary>
    [RelayCommand]
    private void ToggleSearch()
    {
        IsSearchVisible = !IsSearchVisible;
        if (!IsSearchVisible)
        {
            SearchText = string.Empty;
            ApplyFilters();
        }
    }

    /// <summary>
    /// Очистити пошуковий запит
    /// </summary>
    [RelayCommand]
    private void ClearSearch()
    {
        SearchText = string.Empty;
        ApplyFilters();
    }

    /// <summary>
    /// Навігація на детальний екран товару
    /// </summary>
    [RelayCommand]
    private async Task GoToDetailsAsync(Product product)
    {
        if (product is null)
            return;

        await Shell.Current.GoToAsync($"ProductDetailsPage?ProductId={product.Id}");
    }

    /// <summary>
    /// Основна логіка фільтрації — поєднує категорію та пошуковий рядок.
    /// Виконується миттєво за кешованими даними без звернення до API.
    /// </summary>
    private void ApplyFilters()
    {
        IEnumerable<Product> filtered = _allProducts;

        // 1. Фільтрація за категорією
        if (SelectedCategory != "Всі")
        {
            string categoryKey = SelectedCategory.ToLower();
            filtered = filtered.Where(p => p.Category.Equals(categoryKey, StringComparison.OrdinalIgnoreCase));
        }

        // 2. Фільтрація за пошуковим текстом
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string query = SearchText.ToLower().Trim();
            filtered = filtered.Where(p =>
                p.Name.ToLower().Contains(query) ||
                p.Description.ToLower().Contains(query) ||
                p.Year.ToLower().Contains(query));
        }

        // 3. Оновлюємо колекцію (зберігаємо плавність UI)
        Products.Clear();
        foreach (var product in filtered)
            Products.Add(product);
    }

    /// <summary>
    /// Автоматична реакція на зміну SearchText через OnPropertyChanged
    /// </summary>
    partial void OnSearchTextChanged(string value) => ApplyFilters();
}
