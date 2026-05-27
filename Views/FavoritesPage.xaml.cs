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
        // Завжди оновлюємо список при переході на сторінку, щоб відобразити найсвіжіші зміни
        await _viewModel.LoadFavoritesAsync();
    }
}
