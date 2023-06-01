using System.ComponentModel;

namespace AutomationMobile.Enums.FrameworkAdditions;

public enum ApplicationName
{
    [Description("iOS App for B2B")] iOSBusiness,
    [Description("iOS App for B2C")] iOSCustomer,
    [Description("Android App for B2B")] AndroidBusiness,
    [Description("Android App for B2C")] AndroidCustomer
}
