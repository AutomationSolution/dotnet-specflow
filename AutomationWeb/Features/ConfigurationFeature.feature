Feature: ConfigurationFeature
	Test multiple configuration sources and it's content

@mytag
Scenario: Reading setting from appsettings.json file
	Then I assert that 'appsettings.json' file contains in ConfigurationManager sources
	
@mytag
Scenario: Reading setting from environment.json file
  Then I assert that 'environment.json' file contains in ConfigurationManager sources