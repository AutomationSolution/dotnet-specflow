Feature: WCFFeature

Scenario: Test WCF service basic usage
  When I send '115' number to WCF service
  Then I assert that WCF service response contains '115'

Scenario: Test WCF service contract usage false (no suffix)
  When I send the following object to WCF service contract
    | BoolValue | StringValue |
    | false     | TestString  |
  Then I assert that WCF service contract response contains the following object
    | BoolValue | StringValue |
    | false     | TestString  |

Scenario: Test WCF service contract usage true (with suffix)
  When I send the following object to WCF service contract
    | BoolValue | StringValue    |
    | true      | WCFAutomation  |
  Then I assert that WCF service contract response contains the following object
    | BoolValue | StringValue         |
    | true      | WCFAutomationSuffix |
