namespace AutomationMobile.Utilities.BrowserStack;

public class BrowserStackUtilities
{
    // private static string GetBrowserStackAppLink()
    // {
    //     var environmentType = AutomationMobileConfiguration.MobileEnvironment.MobileEnvironmentType;
    //     
    //     if (AutomationMobileConfiguration.BrowserStackMobileSettingsModel.BrowserStackGetAppFromConfig)
    //     {
    //         switch (AutomationMobileConfiguration.MobileEnvironment.MobileEnvironmentType)
    //         {
    //             case MobileEnvironmentType.BrowserStack:
    //                 return AutomationMobileConfiguration.BrowserStackMobileData.BrowserStackAppAddress;
    //             case MobileEnvironmentType.Local:
    //             case MobileEnvironmentType.LocalFromExternalNetwork:
    //                 break;
    //             default:
    //                 throw new ArgumentOutOfRangeException(nameof(environmentType), environmentType, $"{nameof(environmentType)} is unsupported");
    //         }
    //     }
    //
    //     if (!AutomationMobileConfiguration.ScenarioDataModel.InstallFromAppCenter) return AutomationMobileConfiguration.BrowserStackMobileData.BrowserStackShareableId;
    //
    //     ConfigurationNative.ScenarioData.AppCenterReleaseModel = AppCenter.GetAppInfoAppFromAppCenter();
    //     var browserStackUploadResponseModel =
    //         BrowserStackUtils.UploadAppToBrowserStack(ConfigurationNative.ScenarioData.AppCenterReleaseModel);
    //
    //     return browserStackUploadResponseModel.ShareableId;
    // }
    //
    // public static string SetUpBrowserStackApp()
    // {
    //     return GetBrowserStackAppLink() ?? throw new InvalidOperationException(
    //         $"BrowserStack app link is null. Make sure you specified app links for scenario team in browserStackSettings.json file. Scenario TeamName is: @{ConfigurationNative.ScenarioData.TeamName}.");
    // }
}
