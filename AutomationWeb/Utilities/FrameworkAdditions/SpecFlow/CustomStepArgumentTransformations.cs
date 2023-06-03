﻿using AutomationWeb.Configuration;
using AutomationWeb.Models.TestData;
using TechTalk.SpecFlow;

namespace AutomationWeb.Utilities.FrameworkAdditions.SpecFlow;
[Binding]
public class CustomStepArgumentTransformations
{
    [StepArgumentTransformation(@"(.*)")]
    public UserDataModel UserDataModelFromUserAliasTransform(string userAlias)
    {
        try
        {
            return AutomationWebConfiguration.UsersDataModel.GetType().GetProperty(userAlias).GetValue(AutomationWebConfiguration.UsersDataModel, null) as UserDataModel;
        }
        catch (NullReferenceException e)
        {
            throw new ArgumentOutOfRangeException(userAlias, $"Can't find a specified property in {nameof(AutomationWebConfiguration.UsersDataModel)} object");
        }
    }

    [StepArgumentTransformation(@"(.*)")]
    public SimpleUserModel SimpleUserModelFromUserAliasTransform(string userAlias)
    {
        var userAliasFormatted = userAlias.Replace(" ", string.Empty);

        try
        {
            return AutomationWebConfiguration.UsersCredentialsModel.GetType().GetProperty(userAliasFormatted).GetValue(AutomationWebConfiguration.UsersCredentialsModel, null) as SimpleUserModel;
        }
        catch (NullReferenceException e)
        {
            throw new ArgumentOutOfRangeException(userAliasFormatted, $"Can't find a specified property in {nameof(AutomationWebConfiguration.UsersCredentialsModel)} object");
        }
    }
}
