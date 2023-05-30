Feature: TestDataFeature

@SolutionTesting
@TestDataFeature
@ConfigurationFeature
Scenario: UserData is set correctly
  Then I assert that '<UserAlias>' does have '<Username>' username, '<Status>' status and '<Verification>' verification
  
  Examples:
    | UserAlias              | Username                   | Status     | Verification |
    | RegisteredUser         | RegisteredUsername         | Registered | NotVerified  |
    | RegisteredVerifiedUser | RegisteredVerifiedUsername | Registered | Verified     |
    | DraftUser              | DraftUsername              | Draft      | NotVerified  |
  