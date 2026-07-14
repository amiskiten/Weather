namespace Weather.Data;
public interface IDayWeather : IDeepEquals
{
    string ImageData { get; }
    float MaxTemperature { get; }
    float MinTemperature { get; }
    float AverageTemperature { get; }
    float MaxWindSpeed { get; }
    float TotalPrecip { get; }
    float TotalSnow { get; }
    float AverageVision { get; }
    int AverageHumidity { get; }
    int RainProbability { get; }
    int SnowProbability { get; }
    WeatherStatus? WeatherStatus { get; }
    float UVIndex { get; }
}
