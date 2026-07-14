namespace Weather.Services;

public class SunDayConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not IAstroData data) throw new NotImplementedException();
        if (!DateTime.TryParseExact(data.Sunrise!, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sunrise)
          | !DateTime.TryParseExact(data.Sunset!, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sunset))
            return "-";
        TimeSpan sunDay = sunset - sunrise;
        if (sunDay < TimeSpan.Zero) sunDay += TimeSpan.FromDays(1);
        return $"{sunDay.Hours:D2} ч {sunDay.Minutes:D2} мин";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
