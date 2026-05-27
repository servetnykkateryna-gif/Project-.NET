using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class CartPage : ContentPage
{
    private readonly CartViewModel _viewModel;

    public CartPage(CartViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Скидаємо стан перед анімацією
        CartContentGrid.Opacity = 0;
        CartContentGrid.TranslationY = 30;

        // Запускаємо завантаження та анімацію паралельно
        await Task.WhenAll(
            _viewModel.LoadCartAsync(),
            Task.WhenAll(
                CartContentGrid.FadeTo(1, 350, Easing.CubicOut),
                CartContentGrid.TranslateTo(0, 0, 350, Easing.CubicOut)
            )
        );
    }
}
