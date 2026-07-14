namespace Weather.Data;
public class HourlyWeather : IHourlyWeather
{
    private static string ToHourFormat(string time)
    {
        if (DateTime.TryParseExact(time, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            return dt.ToString("HH:mm");
        throw new($"Неправильный формат даты: {time} (правильный yyyy-MM-dd HH:mm)");
    }
    [JsonIgnore]
    public string ImageData
    {
        get => WeatherStatus!.Code.ToWeatherCode(IsDay);
    }
    [JsonPropertyName("time")]
    public string Date
    {
        get => field!;
        set => field = ToHourFormat(value ?? "");
    }
    [JsonPropertyName("temp_c")]
    public float Temperature { get; set; }
    [JsonPropertyName("condition")]
    public WeatherStatus? WeatherStatus { get; set; }
    [JsonPropertyName("wind_mph")]
    public float WindSpeed { get; set; }
    [JsonPropertyName("wind_degree")]
    public int WindDegree { get; set; }
    [JsonPropertyName("wind_dir")]
    public string WindDirection
    { 
        get;
        set => field = DataUtils.GetWindDirection(value); 
    } = string.Empty;
    [JsonPropertyName("pressure_mb")]
    public float Pressure { get; set; }
    [JsonPropertyName("precip_mm")]
    public float Precip { get; set; }
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
    [JsonPropertyName("cloud")]
    public int CloudPercent { get; set; }
    [JsonPropertyName("feelslike_c")]
    public float ApparentTemperature { get; set; }
    [JsonPropertyName("windchill_c")]
    public float WindChill { get; set; }
    [JsonPropertyName("heatindex_c")]
    public float HeatIndex { get; set; }
    [JsonPropertyName("dewpoint_c")]
    public float DewPoint { get; set; }
    [JsonPropertyName("vis_km")]
    public float Visibility { get; set; }
    [JsonPropertyName("uv")]
    public float UVIndex { get; set; }
    [JsonPropertyName("gust_mph")]
    public float GustSpeed { get; set; }
    [JsonPropertyName("chance_of_rain")]
    public int RainProbability { get; set; }
    [JsonPropertyName("chance_of_snow")]
    public int SnowProbability { get; set; }
    [JsonPropertyName("is_day")]
    public int IsDayRaw { get; set; }
    [JsonIgnore]
    public bool IsDay
    { 
        get => IsDayRaw == 1; 
    }

    public bool DeepEquals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        var data1 = this;
        var data2 = (obj as HourlyWeather)!;
        return
            data1.ApparentTemperature == data2.ApparentTemperature &&
            data1.CloudPercent == data2.CloudPercent &&
            data1.Date == data2.Date &&
            data1.DewPoint == data2.DewPoint &&
            data1.GustSpeed == data2.GustSpeed &&
            data1.HeatIndex == data2.HeatIndex &&
            data1.Humidity == data2.Humidity &&
            data1.ImageData == data2.ImageData &&
            data1.IsDay == data2.IsDay &&
            data1.Precip == data2.Precip &&
            data1.Pressure == data2.Pressure &&
            data1.Pressure == data2.Pressure &&
            data1.RainProbability == data2.RainProbability &&
            data1.SnowProbability == data2.SnowProbability &&
            data1.Temperature == data2.Temperature &&
            data1.UVIndex == data2.UVIndex &&
            data1.Visibility == data2.Visibility &&
            data1.WeatherStatus!.Description == data2.WeatherStatus!.Description &&
            data1.WindChill == data2.WindChill &&
            data1.WindDirection == data2.WindDirection &&
            data1.WindSpeed == data2.WindSpeed;
    }
}
