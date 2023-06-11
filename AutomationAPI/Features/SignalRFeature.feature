Feature: SignalRFeature

  @SignalRFeature
  Scenario: Test SignalR connection
    Given SignalR connection is opened
    When I send a SignalR message with 'MessageName' name and 'MessageValue' value
    Then I assert that SignalR message with 'MessageName' name and 'MessageValue' value is received
