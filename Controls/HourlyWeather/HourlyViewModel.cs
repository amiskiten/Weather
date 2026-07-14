namespace Weather.Controls.HourlyWeather;
public partial class HourlyViewModel(IMessenger messenger) : ObservableObject
{
    private readonly IMessenger _Messenger = messenger;
    public IHourlyWeather Data
    {
        get => field!;
        set
        {
            if (ReferenceEquals(value, field) || value == null || value.DeepEquals(field)) return;
            field = value;
            OnPropertyChanged();
        }
    }
    [RelayCommand]
    private void SendWeatherData()
    {
        _Messenger.Send(new HourlyWeatherMessage(Data!));
    }
}
