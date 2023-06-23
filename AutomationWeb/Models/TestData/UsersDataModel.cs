using AutomationWeb.Enums.TestData;

namespace AutomationWeb.Models.TestData;

public class UsersDataModel
{
    public const string JsonSectionName = "UserData";

    public UserDataModel RegisteredUser { get; set; }
    public UserDataModel RegisteredVerifiedUser { get; set; }
    public UserDataModel DraftUser { get; set; }
}

public class UserDataModel
{
    public string Username { get; set; }
    public UserStatus Status { get; set; }
    public UserVerification Verification { get; set; }
}
