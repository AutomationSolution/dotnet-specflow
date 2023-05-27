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

@ParallelizationTesting
@SolutionTesting
@ConfigurationFeature
Scenario: Time is set correctly
  Given The GUID is set in Configuration for specific scenario
  When I wait for '5' seconds
  Then I assert that GUID set in Configuration is the same
