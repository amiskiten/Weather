namespace Weather.Services;

public class UVIndexToolTipConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not float uv) throw new NotImplementedException();
        if (uv >= 0 && uv < 3) return "Маленький риск, защита не требуется";
        else if (uv >= 3 && uv < 6) return "Средний риск, защита рекомендуется";
        else if (uv >= 6 && uv < 8) return "Высокий риск, защита обязательна";
        else if (uv >= 8 && uv < 11) return "Очень высокий риск, нахождение на солнце без защиты крайне опасно";
        else return "Крайне высокий риск, рекомендуется избегать пребывания на солнце";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
