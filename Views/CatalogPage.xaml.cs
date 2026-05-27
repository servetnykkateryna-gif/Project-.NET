using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class CatalogPage : ContentPage
{
    private readonly CatalogViewModel _viewModel;

    public CatalogPage(CatalogViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        // Автоматично завантажуємо товари при переході на сторінку
        if (_viewModel.Products.Count == 0)
        {
            await _viewModel.LoadProductsAsync();
        }
    }
}
