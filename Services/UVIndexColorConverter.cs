namespace Weather.Services;

public class UVIndexColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not float uv) throw new();
        if (uv >= 0 && uv < 3) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#12B800"));
        else if (uv >= 3 && uv < 6) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F2DB00"));
        else if (uv >= 6 && uv < 8) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6700"));
        else if (uv >= 8 && uv < 11) return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E80000"));
        else return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C100E8"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
