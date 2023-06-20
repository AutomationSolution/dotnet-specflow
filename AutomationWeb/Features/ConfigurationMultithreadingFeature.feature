Feature: ConfigurationMultithreadingFeature
  Test multiple configuration sources and it's content

  @API
  @ParallelizationTesting
  @SolutionTesting
  @ConfigurationFeature
  Scenario: GUID in Configuration is set at scenario level and thread safe
    Given The GUID is set in Configuration for specific scenario
    When I wait for '5' seconds
    Then I assert that GUID set in Configuration is the same
