namespace AutomationWeb.Models.Configuration;

public class ScenarioMetaData
{
    public const string JsonSectionName = "ScenarioMetaData";

    public DateTime StartTime { get; set; }
    public Guid ScenarioGuid { get; set; }
}
