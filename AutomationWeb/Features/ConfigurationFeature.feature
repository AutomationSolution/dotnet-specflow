Feature: ConfigurationFeature
	Test multiple configuration sources and it's content

@SolutionTesting
@ConfigurationFeature
Scenario: appsettings.json file contains in ConfigurationManager sources
	Then I assert that 'appsettings.json' file contains in ConfigurationManager sources

@SolutionTesting
@ConfigurationFeature
Scenario: environment.json file contains in ConfigurationManager sources
  Then I assert that 'environment.json' file contains in ConfigurationManager sources

@SolutionTesting
@ConfigurationFeature
Scenario: environment variables contains in ConfigurationManager sources
  Then I assert that environment variables contains in ConfigurationManager sources
