namespace Weather.Static;

public static class AppSettings
{
    public static Settings Settings
    {
        get => field!;
        private set;
    }

    static AppSettings()
    {
        string appSettings = DataUtils.ReadEmbeddedResource("Weather.StaticData.AppSettings.json");
        if (string.IsNullOrEmpty(appSettings)) 
        {
            Settings = new("Хабаровск", false, false);
            return;
        }
        try
        {
            Settings = JsonSerializer.Deserialize<Settings>(appSettings) ?? throw new();
        }
        catch
        {
            Logger.Log("AppSettings constructor", ProcessStatus.Error, "cannot load app settings, setting settings into base values");
            Settings = new("Хабаровск", false, false);
        }
    }
}

