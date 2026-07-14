namespace Weather;

public partial class MainViewModel : ObservableObject
{
    private const int UpdateButtonCoolDown = 5;
    private readonly IMessenger _Messenger;
    private readonly IWeatherServices _WeatherServices;
    public ObservableCollection<HourlyViewModel> HourlyData { get; } = [];
    private WeatherData Data
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            LocationData.LocationData = value.Location!;
            try
            {
                if (value.Forecast.Days[0].Hourly!.Count != HourlyData.Count) throw new();
                foreach ((var data, var control) in value.Forecast.Days[0].Hourly!.Zip(HourlyData))
                {
                    control.Data = data;
                }
            }
            catch
            {
                Logger.Log("MainViewModel.DataProperty", ProcessStatus.Error, $"count of hourly data not equals 24 ({value.Forecast.Days[0].Hourly!.Count})");
            }
            CurrentData.ToggleCurrentData = false;
            CurrentData.Data = value.CurrentWeather;
            DaysList = DataUtils.GetDates(value.Forecast);
            try
            {
                if (HourlyData.Count != value.Forecast.Days[0].Hourly.Count) throw new();
                foreach ((var vm, var data) in HourlyData.Zip(value.Forecast.Days![0].Hourly))
                {
                    vm.Data = data;
                }
            }
            catch
            {
                Logger.Log("MainViewModel.DataProperty", ProcessStatus.Warning, "Count of Hourly viewmodels not equals count of hourly data");
            }
            DailyData.DailyWeather = Data.Forecast.Days[0].Daily;
            DailyData.AstroData = Data.Forecast.Days[0].AstroData;
            SelectedDay = DaysList.FirstOrDefault() ?? "";
        }
    }


    public static async Task<MainViewModel> CreateAsync(IHost host)
    {
        var services = host.Services.GetRequiredService<WeatherServices>();
        var messenger = host.Services.GetRequiredService<IMessenger>();

        WeatherData data = await services.GetWeather(AppSettings.Settings.MainCity);
        MainViewModel model = new(data, services, messenger);
        return model;
    }

    private MainViewModel(WeatherData data, IWeatherServices weatherServices, IMessenger messenger)
    {
        _Messenger = messenger;
        _WeatherServices = weatherServices;
        for (int i = 0; i < 24; i++)
        {
            HourlyData.Add
                (
                new(_Messenger));
        }
        DailyData = new();
        CurrentData = new(messenger);
        LocationData = new();

        Data = data;
        DaysList = DataUtils.GetDates(data.Forecast);
        SelectedDay = DaysList.FirstOrDefault() ?? "";

        CitiesList = DataUtils.RussianCities ?? ["Хабаровск"];
        SelectedCity = AppSettings.Settings.MainCity;
        SearchData = new(_Messenger, CitiesList, SelectedCity);
        _Messenger.Register<NewSearchValueMessage>(this, UpdateCity);
        WindowTitle = $"{SelectedCity}. Погода";

        CooldownTimer = new()
        {
            Interval = TimeSpan.FromSeconds(UpdateButtonCoolDown),
        };
        CooldownTimer.Tick += (s, e) =>
        {
            CooldownTimer.Stop();
            IsCooldownActive = false;
            UpdateDataCommand.NotifyCanExecuteChanged();
        };
        Logger.Log("MainViewModel constructor", ProcessStatus.Success, "succussfully initialized MainViewModel");
    }

    //LocationControl
    [ObservableProperty]
    public partial LocationViewModel LocationData { get; set; }

    //DailyWeather
    [ObservableProperty]
    public partial DailyViewModel DailyData { get; set; }

    //CurrentControl
    [ObservableProperty]
    public partial CurrentViewModel CurrentData { get; set; }

    private void RefreshWeatherData(OneDayData data)
    {
        Logger.Log("MainViewModel.RefreshWeatherData", ProcessStatus.Started, "started to refreshing main weather interface(except currentControl)");
        DailyData.DailyWeather = data.Daily!;
        DailyData.AstroData = data.AstroData!;
        try
        {
            if (data.Hourly.Count != HourlyData.Count) throw new($"hourly data is not equals 24 {data.Hourly.Count}");
            foreach ((IHourlyWeather hourData, var control) in data.Hourly!.Zip(HourlyData))
            {
                control.Data = hourData;
            }
            Logger.Log("MainViewModel.RefreshWeatherData", ProcessStatus.Success, "successfuly updated main weather interface");

        }
        catch (Exception ex)
        {
            Logger.Log("MainViewModel.RefreshWeatherData", ProcessStatus.Error, ex.Message);
        }
    }

    private readonly DispatcherTimer CooldownTimer;
    [ObservableProperty]
    private partial bool IsCooldownActive { get; set; }
    private bool CanGetWeather() =>
        !string.IsNullOrEmpty(SelectedCity) && !IsCooldownActive;
    [RelayCommand(CanExecute = nameof(CanGetWeather))]
    private async Task UpdateDataAsync()
    {
        try
        {
            Logger.Log("MainViewModel.UpdateDataAsync", ProcessStatus.Started, "Weather data update started");
            Data = await _WeatherServices.GetWeather(SelectedCity!);
            WindowTitle = $"{SelectedCity!}. Погода";
            Logger.Log("MainViewModel.UpdateDataAsync", ProcessStatus.Ended, "Weather data update successfully ended");
            LocationData.LocationData = Data.Location ?? throw new("No location data in request");
        }
        catch (Exception ex)
        {
            Logger.Log("MainViewModel.UpdateDataAsync", ProcessStatus.Error, ex.Message);
        }
        IsCooldownActive = true;
        CooldownTimer.Start();
    }



    public IEnumerable<string> DaysList
    {
        get;
        set
        {
            if (field != null && field.SequenceEqual(value)) return;
            field = value;
            OnPropertyChanged();
        }
    }

    public string SelectedDay
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            OnPropertyChanged();
            int index = DaysList.Select((item, idx) =>
                new { Item = item, Index = idx }).FirstOrDefault(item => item.Item == value)?.Index ?? -1;
            RefreshWeatherData(Data.Forecast.Days[index]);
        }
    }

    [ObservableProperty]
    public partial bool IsCurrentSticky { get; set; }


    //city
    private void UpdateCity(object? sender, NewSearchValueMessage message) =>
        SelectedCity = message.Value;


    [ObservableProperty]
    public partial SearchViewModel SearchData { get; set; }

    [ObservableProperty]
    public partial IEnumerable<string> CitiesList { get; set; }

    [ObservableProperty]
    public partial string? SelectedCity { get; set; }


    [ObservableProperty]
    public partial string WindowTitle { get; set; }
}

