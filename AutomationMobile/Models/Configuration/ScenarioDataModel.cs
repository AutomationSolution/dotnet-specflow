﻿using AutomationMobile.Enums.FrameworkAdditions;
using AutomationMobile.Utilities.SpecFlow;
using TechTalk.SpecFlow;

namespace AutomationMobile.Models.Configuration;

public class ScenarioDataModel
{
    private ScenarioType ScenarioType { get; set; }
    private string ScenarioName { get; set; }
    public string TestRailId { get; set; }
    public string TestFeatureName { get; set; }
    public bool InstallFromAppCenter { get; set; }
    public bool BiometricAuthEnabled { get; set; }
    public string InformativeScenarioName => $"[{TestFeatureName}] {ScenarioName}";
    
    public ScenarioDataModel(ScenarioContext scenarioContext)
    {
        ScenarioType = TagsUtilities.GetScenarioType(scenarioContext);
        ScenarioName = ScenarioContextUtilities.GetScenarioTitle(scenarioContext);
        TestRailId = TagsUtilities.GetTestRailId(scenarioContext);
        TestFeatureName = TagsUtilities.GetTestFeatureName(scenarioContext);
        InstallFromAppCenter = TagsUtilities.GetInstallFromAppCenterFlag(scenarioContext);
        BiometricAuthEnabled = TagsUtilities.GetBiometricAuthFlag(scenarioContext);
    }
}