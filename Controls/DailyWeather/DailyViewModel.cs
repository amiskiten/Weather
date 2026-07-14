namespace Weather.Controls.DailyWeather;

public partial class DailyViewModel : ObservableObject
{
    public IAstroData AstroData
    {
        get => field!;
        set
        {
            if (ReferenceEquals(value, field) || value == null || value.Equals(field)) return;
            field = value;
            OnPropertyChanged();
        }
    }

    public IDayWeather DailyWeather
    {
        get => field!;
        set
        {
            if (ReferenceEquals(value, field) || value == null || value.DeepEquals(field)) return;
            field = value;
            OnPropertyChanged();
        }
    }
}
