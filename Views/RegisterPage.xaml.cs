using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Плавна поява форми реєстрації
        RegisterFormView.Opacity = 0;
        RegisterFormView.TranslationY = 20;

        await Task.WhenAll(
            RegisterFormView.FadeTo(1, 400, Easing.CubicOut),
            RegisterFormView.TranslateTo(0, 0, 400, Easing.CubicOut)
        );
    }
}
