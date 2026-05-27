namespace VelvetRelics.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Логіка входу буде реалізована в Етапі 3.
        // Поки що просто імітуємо вхід і переходимо на головну сторінку.
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnRegisterTapped(object sender, TappedEventArgs e)
    {
        // Перехід на сторінку реєстрації
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}
