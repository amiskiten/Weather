namespace Weather.Data;

public record WeatherData
(
    [property: JsonPropertyName("location")] Location? Location,
    [property: JsonPropertyName("current")] CurrentWeather CurrentWeather,
    [property: JsonPropertyName("forecast")] Forecast Forecast
);
