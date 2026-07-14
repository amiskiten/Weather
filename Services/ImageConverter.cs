namespace Weather.Services;

public class ImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string imageCode) return null!;
        Func<UserControl> func = DataUtils.GetWeatherAction(imageCode);
        UserControl control = func();
        return control;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
