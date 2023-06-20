Feature: SignalRFeature

  Background: 
    Given SignalR connection is opened
  
  @SignalRFeature
  Scenario: Test SignalR connection
    When I send a SignalR message with 'MessageName' name and 'MessageValue' value
    Then I assert that SignalR message with 'MessageName' name and 'MessageValue' value is received
