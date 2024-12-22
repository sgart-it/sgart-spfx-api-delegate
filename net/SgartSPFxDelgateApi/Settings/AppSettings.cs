namespace SgartSPFxDelgateApi.Settings;

public class AppSettings
{
    public static string KEY_NAME = "AppSettings";

    public DemoSettings Demo { get; set; } = new();

    public string[] Cors { get; set; } = [];
}

public class DemoSettings
{
    public string Url { get; set; } = string.Empty;
}
