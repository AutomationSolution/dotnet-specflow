namespace AutomationWeb.Models.TestData;

public class UsersCredentialsModel
{
    public SimpleUserModel StandardUser { get; set; }
    public SimpleUserModel LockedOutUser { get; set; }
    public SimpleUserModel ProblemUser { get; set; }
    public SimpleUserModel PerformanceGlitchUser { get; set; }
}

public class SimpleUserModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
