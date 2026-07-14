namespace Weather.Controls.CurrentWeather;
public partial class CurrentViewModel : ObservableObject
{
    private readonly IMessenger _Messenger;
    private IHourlyWeather? _BackUpedData;
    public CurrentViewModel(IMessenger messenger)
    {
        _Messenger = messenger;
        _Messenger.Register<HourlyWeatherMessage>(this, OnHourlyData);
    }

    private void OnHourlyData(object recipient, HourlyWeatherMessage message)
    {
        ToggleCurrentData = true;
        Data = message.Data;
    }
    public IHourlyWeather Data
    {
        get => field!;
        set
        {
            if (ReferenceEquals(value, field) || value == null || value.DeepEquals(field)) return;
            field = value;
            OnPropertyChanged();
            if (ToggleCurrentData ||
                ReferenceEquals(value, _BackUpedData) || 
                value.DeepEquals(_BackUpedData)) return;
            _BackUpedData = value;
        }
    }
    public bool ToggleCurrentData
    {
        get;
        set
        {
            if (value == field) return;
            if (!value)
            {
                Data = _BackUpedData!;
            }
            field = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ButtonOpacity));
        }
    } = false;
    [RelayCommand]
    private void EnableCurrentData() =>
        ToggleCurrentData = false;

    public double ButtonOpacity
    {
        get => ToggleCurrentData ? 1d : 0d;
    }
}
