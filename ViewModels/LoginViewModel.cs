using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authService;
    private readonly SessionService _sessionService;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    // Отримуємо обидва сервіси через Dependency Injection
    public LoginViewModel(IAuthenticationService authService, SessionService sessionService)
    {
        _authService = authService;
        _sessionService = sessionService;
        Title = "Вхід";
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть Email та пароль.", "OK");
            return;
        }

        try
        {
            IsBusy = true;

            var response = await _authService.LoginAsync(Email, Password);

            if (response.IsSuccess)
            {
                // ✅ Зберігаємо сесію в SQLite для автоматичного входу наступного разу
                await _sessionService.SaveSessionAsync(response);

                // Переходимо на головну сторінку
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка авторизації", response.ErrorMessage, "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Помилка", $"Щось пішло не так: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToRegisterAsync()
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }
}
