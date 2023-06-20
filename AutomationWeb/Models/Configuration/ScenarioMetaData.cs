namespace AutomationWeb.Models.Configuration;

public class ScenarioMetaData
{
    public const string ScenarioMetaDataJsonSectionName = "ScenarioMetaData";

    public DateTime StartTime { get; set; }
    public Guid ScenarioGuid { get; set; }
}
