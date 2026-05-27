using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using VelvetRelics.Services;

namespace VelvetRelics.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    private readonly IAuthenticationService _authService;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    public RegisterViewModel(IAuthenticationService authService)
    {
        _authService = authService;
        Title = "Реєстрація";
    }

    [RelayCommand]
    private async Task RegisterAsync()
    {
        if (IsBusy)
            return;

        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            await Shell.Current.DisplayAlert("Помилка", "Всі поля є обов'язковими.", "OK");
            return;
        }

        try
        {
            IsBusy = true;
            
            var response = await _authService.RegisterAsync(Name, Email, Password);

            if (response.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Успіх", "Реєстрація успішна! Тепер ви можете увійти.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Помилка реєстрації", response.ErrorMessage, "OK");
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
    private async Task GoToLoginAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
