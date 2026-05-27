using VelvetRelics.ViewModels;

namespace VelvetRelics.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Плавна поява форми входу
        LoginFormView.Opacity = 0;
        LoginFormView.TranslationY = 20;

        await Task.WhenAll(
            LoginFormView.FadeTo(1, 400, Easing.CubicOut),
            LoginFormView.TranslateTo(0, 0, 400, Easing.CubicOut)
        );
    }
}
