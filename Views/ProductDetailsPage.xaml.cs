using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class ProductDetailsPage : ContentPage
{
    public ProductDetailsPage(ProductDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Анімація виїзду деталей товару знизу вгору
        DetailsScrollView.Opacity = 0;
        DetailsScrollView.TranslationY = 40;

        await Task.WhenAll(
            DetailsScrollView.FadeTo(1, 400, Easing.CubicOut),
            DetailsScrollView.TranslateTo(0, 0, 400, Easing.CubicOut)
        );
    }
}
