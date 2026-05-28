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
}