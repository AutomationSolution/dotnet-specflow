Feature: ConfigurationMultithreadingFeature
  Test multiple configuration sources and it's content

@ParallelizationTesting
@SolutionTesting
@ConfigurationFeature
Scenario: Time is set correctly
  Given The GUID is set in Configuration for specific scenario
  When I wait for '5' seconds
  Then I assert that GUID set in Configuration is the same
