Feature: DriverFeature

  @UI
  @TestRailId_C555
  @BrowserstackFeature
  Scenario: Run a test against web UI
    Given I am on a Login Page
    When I fill in Standard User credentials on Login Page
      And I click Login button on Login Page
    Then I am on a Main Page
