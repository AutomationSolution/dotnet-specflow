using System.Globalization;

namespace AutomationFramework.Models.Configuration;

public class TestRunConfig
{
    public CultureInfo DefaultCulture = new("en-US");
    public DateTime TestRunStartTime { get; set; }
}