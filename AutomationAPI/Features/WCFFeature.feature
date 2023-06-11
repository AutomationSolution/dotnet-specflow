Feature: WCFFeature

Scenario: Test WCF service basic usage
  When I send '115' number to WCF service
  Then I assert that WCF service response contains '115'

Scenario: Test WCF service contract usage
  When I send the following object to WCF service contract
    | BoolValue | StringValue |
    | false     | TestString  |
  Then I assert that WCF service contract response contains the following object
    | BoolValue | StringValue |
    | false     | TestString  |
