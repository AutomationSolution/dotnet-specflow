Feature: EnvironmentFeature

  @API
  @SolutionTesting
  @ConfigurationFeature
  Scenario: Environment variables are taken into priority over other sources in configuration
    Given 'featureTest.json' file is added as a source to ConfigurationManager
      And 'Priority' section is mapped to a MappedSectionObject object
      And Mapped object contain the following environment variables
        | VariableSection | VariableName | VariableValue     |
        | Priority        | Name         | NameValueFile     |
        | Priority        | Password     | PasswordValueFile |
      And The following environment variables are set
        | VariableSection | VariableName | VariableValue     |
        | Priority        | Name         | NameValueStep     |
        | Priority        | Password     | PasswordValueStep |
      And Environment variables are added as a source to ConfigurationManager
      And 'Priority' section is mapped to a MappedSectionObject object
    Then I assert that mapped object contain the following environment variables
      | VariableSection | VariableName | VariableValue     |
      | Priority        | Name         | NameValueStep     |
      | Priority        | Password     | PasswordValueStep |

  @API
  @SolutionTesting
  @ConfigurationFeature
  Scenario: Output EnvironmentModel values
    When I output EnvironmentModel values
