namespace Weather.Data;

public class AstroData : IAstroData
{
    private static string To24Format(string? time)
    {
        if (DateTime.TryParseExact(time, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
            return dt.ToString("HH:mm");
        return time ?? "";
    }
    [JsonPropertyName("sunrise")]
    public string? Sunrise
    {
        get;
        set
        {
            if (value == "Polar Day")
            {
                field = "Полярный день";
                return;
            }
            else if (value == "Polar Night")
            {
                field = "Полярная ночь";
            }
            else field = To24Format(value!);
        }
    }

    [JsonPropertyName("sunset")]
    public string? Sunset
    {
        get;
        set
        {
            if (value == "Polar Day")
            {
                field = "Полярный день";
                return;
            }
            else if (value == "Polar Night")
            {
                field = "Полярная ночь";
            }
            else field = To24Format(value!);
        }
    }
    [JsonPropertyName("moonrise")]
    public string? Moonrise
    {
        get;
        set => field = (value is null or "" or "Does not rise today") ? "Не вставала" : To24Format(value);
    }
    [JsonPropertyName("moonset")]
    public string? Moonset
    {
        get;
        set => field = (value is null or "" or "Does not set today") ? "Не садилась" : To24Format(value);
    }
    [JsonPropertyName("moon_phase")]
    public string? MoonPhase
    {
        get;
        set => field = (value ?? "-").TranslateMoonStatus();
    }
    [JsonPropertyName("moon_illumination")]
    public int MoonIllumination { get; set; }

    public bool DeepEquals(object? obj)
    {
        if (obj is null || GetType() != obj.GetType())
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        AstroData data1 = this;
        AstroData data2 = (obj as AstroData)!;
        return data1.Sunrise == data2.Sunrise &&
               data1.Sunset == data2.Sunset &&
               data1.Moonrise == data2.Moonrise &&
               data1.Moonset == data2.Moonset &&
               data1.MoonIllumination == data2.MoonIllumination &&
               data1.MoonPhase == data2.MoonPhase;
    }
}
