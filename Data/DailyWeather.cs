using System.Diagnostics;

namespace Weather.Data;

public class DailyWeather : IDayWeather
{
    [JsonIgnore]
    public string ImageData
    {
        get => WeatherStatus!.Code.ToWeatherCode(true);
    }
    [JsonPropertyName("maxtemp_c")]
    public float MaxTemperature { get; set; }
    [JsonPropertyName("mintemp_c")]
    public float MinTemperature { get; set; }
    [JsonPropertyName("avgtemp_c")]
    public float AverageTemperature { get; set; }
    [JsonPropertyName("maxwind_mph")]
    public float MaxWindSpeed { get; set; }
    [JsonPropertyName("totalprecip_mm")]
    public float TotalPrecip { get; set; }
    [JsonPropertyName("totalsnow_cm")]
    public float TotalSnow { get; set; }
    [JsonPropertyName("avgvis_km")]
    public float AverageVision { get; set; }
    [JsonPropertyName("avghumidity")]
    public int AverageHumidity { get; set; }
    [JsonPropertyName("daily_chance_of_rain")]
    public int RainProbability { get; set; }
    [JsonPropertyName("daily_chance_of_snow")]
    public int SnowProbability { get; set; }
    [JsonPropertyName("condition")]
    public WeatherStatus? WeatherStatus { get; set; }
    [JsonPropertyName("uv")]
    public float UVIndex { get; set; }

    public bool DeepEquals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        DailyWeather d1 = this;
        DailyWeather d2 = (obj as DailyWeather)!;
        return
            d1.AverageHumidity == d2.AverageHumidity &&
            d1.AverageTemperature == d2.AverageTemperature &&
            d1.AverageVision == d2.AverageVision &&
            d1.ImageData == d2.ImageData &&
            d1.MaxTemperature == d2.MaxTemperature &&
            d1.MinTemperature == d2.MinTemperature &&
            d1.RainProbability == d2.RainProbability &&
            d1.SnowProbability == d2.SnowProbability &&
            d1.TotalPrecip == d2.TotalPrecip &&
            d1.TotalSnow == d2.TotalSnow &&
            d1.UVIndex == d2.UVIndex &&
            d1.WeatherStatus!.Description == d2.WeatherStatus!.Description;
    }
}
