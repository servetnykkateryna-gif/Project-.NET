using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class FavoritesPage : ContentPage
{
    private readonly FavoritesViewModel _viewModel;

    public FavoritesPage(FavoritesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Скидаємо стан перед анімацією
        FavoritesContentGrid.Opacity = 0;
        FavoritesContentGrid.TranslationY = 30;

        // Запускаємо завантаження та анімацію паралельно
        await Task.WhenAll(
            _viewModel.LoadFavoritesAsync(),
            Task.WhenAll(
                FavoritesContentGrid.FadeTo(1, 350, Easing.CubicOut),
                FavoritesContentGrid.TranslateTo(0, 0, 350, Easing.CubicOut)
            )
        );
    }
}
