using Weather.WeatherImages.Images;
namespace Weather.Static;

public static class DataUtils
{
    private static readonly Dictionary<string, string>? MoonVars;
    private static readonly Dictionary<string, string>? WeatherCodes;
    private static readonly Dictionary<string, Func<UserControl>> ImagesFuncs;
    private static readonly Dictionary<char, char> BaseWindDirections;
    public static IEnumerable<string>? RussianCities
    {
        get;
        private set;
    }
    static DataUtils()
    {
        static void SuccLog(string name) => Logger.Log("Weather", ProcessStatus.Success, $"successfully loaded {name}");
        static void ErLog(string message) => Logger.Log("DataUtils constructor", ProcessStatus.Warning, message);
        string moonPhasesString = ReadEmbeddedResource("Weather.StaticData.MoonPhases.json");
        try
        {
            MoonVars = JsonSerializer.Deserialize<Dictionary<string, string>>(moonPhasesString)
                ?? throw new("MoonPhases.json deserialization error");
            SuccLog(nameof(MoonVars));
        }
        catch (Exception ex)
        {
            ErLog(ex.Message);
        }
        string weatherCodesString = ReadEmbeddedResource("Weather.StaticData.WeatherCodes.json");
        try
        {
            WeatherCodes = JsonSerializer.Deserialize<Dictionary<string, string>>(weatherCodesString)
                ?? throw new("WeatherCodes.json deserialization error");
            SuccLog(nameof(WeatherCodes));
        }
        catch (Exception ex)
        {
            ErLog(ex.Message);
        }
        ImagesFuncs = new()
        {
            ["ClearDay"] = () => new ClearDay(),
            ["ClearNight"] = () => new ClearNight(),
            ["Cloudy"] = () => new Cloudy(),
            ["Drizzle"] = () => new Drizzle(),
            ["Fog"] = () => new Fog(),
            ["Hail"] = () => new Hail(),
            ["Mist"] = () => new Mist(),
            ["Overcast"] = () => new Overcast(),
            ["PartlyCloudyDay"] = () => new PartlyCloudyDay(),
            ["PartlyCloudyNight"] = () => new PartlyCloudyNight(),
            ["Snow"] = () => new Snow(),
            ["Rain"] = () => new Rain(),
            ["Sleet"] = () => new Sleet(),
            ["Thunderstorms"] = () => new Thunderstorms(),
            ["ThunderstormsRain"] = () => new ThunderstormsRain(),
            ["ThunderstormsSnow"] = () => new ThunderstormsSnow(),
        };
        BaseWindDirections = new()
        {
            ['N'] = 'С',
            ['E'] = 'В',
            ['S'] = 'Ю',
            ['W'] = 'З'
        };
        string rusCitiesString = ReadEmbeddedResource("Weather.StaticData.Cities.json");
        try
        {
            RussianCities = JsonSerializer.Deserialize<IEnumerable<string>>(rusCitiesString) ?? throw new("Cities.json deserialization error");
            SuccLog(nameof(RussianCities));
        }
        catch (Exception ex)
        {
            ErLog(ex.Message);
        }
    }

    public static string TranslateMoonStatus(this string status)
    {
        if (!MoonVars!.TryGetValue(status, out string? tr)) return "";
        return tr ?? "-";
    }

    public static string ToWeatherCode(this int code, bool isDay)
    {
        return WeatherCodes![code.ToString()] + code switch
        {
            1000 or 1003 => isDay ? "Day" : "Night",
            _ => ""
        };
    }

    public static Func<UserControl> GetWeatherAction(this string name) =>
        ImagesFuncs[name];

    public static string GetWindDirection(string windDir)
    {
        string windDirection = "";
        foreach (char c in windDir)
        {
            if (!BaseWindDirections.TryGetValue(c, out char n)) return "-";
            windDirection += n;
        }
        return windDirection;
    }

    public static IEnumerable<string> GetDates(Forecast data) =>
    data.Days.Select(oneDay => oneDay.Date.DateToString());

    private static readonly CultureInfo ru = new("ru-RU");
    private static string DateToString(this DateTime date) =>
        date.ToString("d MMMM, ddd", ru);


    public static string ReadEmbeddedResource(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(name);
        if (stream == null)
        {
            Logger.Log("DataUtils.ReadEmbeddedResource", ProcessStatus.Warning, $"cannot find embbeded resource named {name}");
            return "";
        }
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }
}
