namespace Weather.Static;
public static class Logger
{
    private static string Logs = "";
    public static void Log(string from, ProcessStatus status, string message)
    {
        Logs += $"[{DateTime.Now:dd.MM HH:mm:ss}] [Operation {from}] [{status}] Message: {message}\n";
    }
    public static void Write()
    {
        File.WriteAllText("logs.txt", Logs);
        Logs = "";
    }

    public static void ClearLogs() =>
            File.WriteAllText("logs.txt", "");

}
public enum ProcessStatus
{
    Error,
    Success,
    Warning,
    Started,
    Ended
}
