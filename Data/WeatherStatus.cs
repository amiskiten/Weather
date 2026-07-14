namespace Weather.Data;
public record WeatherStatus(
    [property: JsonPropertyName("text")] string Description,
    [property: JsonPropertyName("code")] int Code);