namespace Weather.Services;
public interface IWeatherServices
{
    Task<WeatherData> GetWeather(string city);
}
