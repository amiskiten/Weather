namespace Weather.Data;
public record Location
    (
    [property: JsonPropertyName("name")] string CityName,
    [property: JsonPropertyName("region")] string? RegionName,
    [property: JsonPropertyName("lat")] float Latitude,
    [property: JsonPropertyName("lon")] float Longitude,
    [property: JsonPropertyName("tz_id")] string Timezone
    ) : ILocationData;