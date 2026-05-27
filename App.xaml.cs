using VelvetRelics.Services;

namespace VelvetRelics;

public partial class App : Application
{
    private readonly SessionService _sessionService;

    // SessionService приходить через Dependency Injection
    public App(SessionService sessionService)
    {
        InitializeComponent();
        _sessionService = sessionService;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

    protected override async void OnStart()
    {
        base.OnStart();

        // Перевіряємо, чи є збережена сесія в SQLite
        bool isLoggedIn = await _sessionService.IsLoggedInAsync();

        if (isLoggedIn)
        {
            // Якщо сесія є — переходимо одразу на головну сторінку, минаючи логін
            await Shell.Current.GoToAsync("//MainPage");
        }
        // Якщо сесії немає — AppShell за замовчуванням показує LoginPage
    }
}