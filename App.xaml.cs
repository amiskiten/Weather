namespace Weather;

public partial class App : Application
{
    private IHost? _Host;
    protected override async void OnStartup(StartupEventArgs e)
    {
        if (AppSettings.Settings.Logs && AppSettings.Settings.RemoveLogsOnStart) Logger.ClearLogs();
        base.OnStartup(e);
        _Host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services =>
                    {
                        services.AddHttpClient("WeatherAPI", client =>
                        {
                            client.BaseAddress = new Uri("https://api.weatherapi.com/v1/");
                        }).AutoSetting();
                        services.AddSingleton<WeatherServices>();
                        services.AddSingleton<IMessenger>(WeakReferenceMessenger.Default);
                    })
                .Build();
        _Host.Start();
        MainViewModel vm;
        try
        {
            vm = await MainViewModel.CreateAsync(_Host);
        }
        catch (Exception ex)
        {
            Logger.Log("App.OnStartup", ProcessStatus.Error, "Cannot start app by critical error. error: " + ex.Message);
            if (AppSettings.Settings.Logs) Logger.Write();
            throw new();
        }
        try
        {
            MainWindow window = new()
            {
                DataContext = vm
            };
            window.Show();
        }
        catch (Exception ex)
        {
            Logger.Log("App.OnStartup", ProcessStatus.Error, $"App was closed by uncatched exception: {ex.Message}");
            if (AppSettings.Settings.Logs) Logger.Write();
            throw new(ex.Message);
        }
    }

    protected override void OnExit(ExitEventArgs e)
    {
        if (AppSettings.Settings.Logs) Logger.Write();
        base.OnExit(e);
        _Host?.Dispose();
    }
}
