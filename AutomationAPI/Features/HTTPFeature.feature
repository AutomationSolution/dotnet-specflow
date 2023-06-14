Feature: HTTPFeature

  Background: 
    Given HTTP connection is opened
  
  @OpenAPIFeature
  Scenario: Test Legacy REST service
    When I send '/weatherforecast' request via 'GET' method by HTTP
    Then I assert that response contains Weather Forecast for 5 days by HTTP
