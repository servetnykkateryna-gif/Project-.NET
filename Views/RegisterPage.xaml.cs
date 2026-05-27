namespace VelvetRelics.Views;

public partial class RegisterPage : ContentPage
{
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        // Логіка реєстрації буде реалізована в Етапі 3.
        // Поки що просто повертаємося на сторінку логіну або на головну.
        await Shell.Current.GoToAsync("..");
    }

    private async void OnLoginTapped(object sender, TappedEventArgs e)
    {
        // Повернення на сторінку логіну
        await Shell.Current.GoToAsync("..");
    }
}
