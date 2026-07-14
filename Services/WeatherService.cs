namespace Weather.Services;
public class WeatherServices : IWeatherServices
{
    private readonly IHttpClientFactory _Factory;
    private readonly string? APIKey;
    public WeatherServices(IHttpClientFactory factory)
    {
        _Factory = factory;
        string keyString = DataUtils.ReadEmbeddedResource("Weather.StaticData.secret.json");
        try
        {
            Dictionary<string, string> secrets = JsonSerializer.Deserialize<Dictionary<string, string>>(keyString) ?? throw new("secret.json deserialization error");
            APIKey = secrets["WeatherAPI:API_KEY"];
        }
        catch (Exception ex)
        {
            Logger.Log("WeatherServices constructor", ProcessStatus.Error, ex.Message);
            Logger.Log("WeatherServices constructor", ProcessStatus.Error, "CRITICAL ERROR: cannot start programm without WeatherAPI api-key");
            throw new NullReferenceException("CRITICAL ERROR: NO API KEY");
        }
    }
    public async Task<WeatherData> GetWeather(string city)
    {
        try
        {
            //base uri: https://api.weatherapi.com/v1/
            HttpClient client = _Factory.CreateClient("WeatherAPI");
            string url = $"forecast.json?key={APIKey}&q={city}&lang=ru&days=3";
            var data = await client.GetFromJsonAsync<WeatherData>(url) ?? throw new("unable get data from WeatherAPI");
            Logger.Log("WeatherServices.GetWeather", ProcessStatus.Success, "successfully got data");
            return data;
        }
        catch (Exception ex)
        {
            Logger.Log("WeatherServices.GetWeather", ProcessStatus.Warning, ex.Message);
            throw new();
        }
    }
}
