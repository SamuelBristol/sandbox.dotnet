namespace App.Console.Configuration;

public class ApplicationInformation
{
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;

    public override string ToString() => $"{Name} v{Version}";
}
