namespace Weather.Data;
public interface IAstroData : IDeepEquals
{
    string? Sunrise { get; }
    string? Sunset { get; }
    string? Moonrise { get; }
    string? Moonset { get; }
    string? MoonPhase { get; }
    int MoonIllumination { get; }
}
