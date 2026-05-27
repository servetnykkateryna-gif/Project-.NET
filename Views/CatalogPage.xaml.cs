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

        // Плавна анімація появи контентної зони (slide-up + fade-in)
        CatalogContentGrid.Opacity = 0;
        CatalogContentGrid.TranslationY = 30;

        // Завантажуємо товари якщо ще не завантажені
        if (_viewModel.Products.Count == 0)
        {
            await _viewModel.LoadProductsAsync();
        }

        // Запускаємо анімацію паралельно
        await Task.WhenAll(
            CatalogContentGrid.FadeTo(1, 350, Easing.CubicOut),
            CatalogContentGrid.TranslateTo(0, 0, 350, Easing.CubicOut)
        );
    }
}
