Feature: gRPCFeature

  Background: 
    Given gRPC connection is established
    
  @gRPCFeature
  Scenario: Test gRPC greeter service
	  Given Greeter service is initialized
	  When I send SayHello gRPC request with '<Message>' message
	  Then I assert that SayHello gRPC response contains 'Hello <Message>'
	  
	  Examples: 
      | Message |
      | Dmitry  |
