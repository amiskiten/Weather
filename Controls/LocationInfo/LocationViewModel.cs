namespace Weather.Controls.LocationInfo;
public class LocationViewModel : ObservableObject
{
    public ILocationData LocationData
    {
        get => field!;
        set
        {
            if (field != null && field!.Equals(value)) return;
            field = value;
            OnPropertyChanged();
        }
    }
}
