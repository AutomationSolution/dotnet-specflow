Feature: OpenAPIFeature

  Background: 
    Given HTTP connection is opened
  
  @OpenAPIFeature
  Scenario: Test OpenAPI REST service
    When I send GetWeatherForecast GET request
    Then I assert that response contains Weather Forecast for 5 days
