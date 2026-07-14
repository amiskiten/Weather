namespace Weather.Data;

public record Settings(
    [property: JsonPropertyName("main_city")] string MainCity,
    [property: JsonPropertyName("remove_logs_on_start")] bool RemoveLogsOnStart,
    [property: JsonPropertyName("logs")] bool Logs);
