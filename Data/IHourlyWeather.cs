namespace Weather.Data;
public interface IHourlyWeather : IDeepEquals
{
    string Date { get; }
    float Temperature { get; }
    WeatherStatus? WeatherStatus { get; }
    string ImageData { get; }
    float WindSpeed { get; }
    int WindDegree { get; }
    string WindDirection { get; }
    float Precip { get; }
    float Pressure { get; }
    int Humidity { get; }
    int CloudPercent { get; }
    float ApparentTemperature { get; }
    float WindChill { get; }
    float HeatIndex { get; }
    float DewPoint { get; }
    float Visibility { get; }
    float UVIndex { get; }
    float GustSpeed { get; }
    int RainProbability { get; }
    int SnowProbability { get; }
    bool IsDay { get; }
}
