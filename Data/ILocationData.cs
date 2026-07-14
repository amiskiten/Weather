namespace Weather.Data;
public interface ILocationData
{
    string CityName { get; }
    string? RegionName { get; }
    float Latitude { get; }
    float Longitude { get; }
    string Timezone { get; }
}
