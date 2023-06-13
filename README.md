# .NET-SpecFlow

## Secrets
Local: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets

optional plugin for Rider to manage local secrets: https://plugins.jetbrains.com/plugin/10183--net-core-user-secrets
## Project structure
| Category                | Implementations               |                             |                    |             |
|-------------------------|-------------------------------|-----------------------------|--------------------|-------------|
| High level testing tool | ✅ BrowserStack Automate (Web) | ⬜ BrowserStack App Automate |                    |             |
| UI testing              | ✅ Selenium (Aquality)         | ✅ Appium (Aquality)         |                    |             |
| non-UI testing          | ⬜ REST                        | ✅ SignalR                   | ✅ WCF, ⬜ gRPC      | ⬜ OpenAPI   |
| BDD tool                | ✅ Specflow                    |                             |                    |             |
| Report tool             | ⬜ Aquality Tracking           | ⬜ Allure                    | ⬜ ExtentReports    |             |
| Code                    | ✅ C# with .NET 6.0            |                             |                    |             |
| Logger                  | ✅ NLog 5                      |                             |                    |             |
| Test Runner             | ✅ NUnit 3                     |                             |                    |             |
| CI/CD                   | ✅ GitHub Actions              | ✅ Jenkins                   | ⬜ Azure            | ⬜ CircleCI  |
| Secrets management      | ✅ Github                      | ✅ Jenkins                   | ✅ Azure            | ✅ Local     |
| Assertions              | ✅ FluentAssertions            |                             |                    |             |
| Visual Testing          | ⬜ OpenCV                      | ⬜ Tesseract                 | ⬜ Applitools Eyes  |             |
| Test Management         | ⬜ TestRail                    | ⬜ XRay                      |                    |             |
| App Provider            | ⬜ AppCenter API               |                             |                    |             |
| Data Generator          | ⬜ Faker                       | ⬜ AutoFixture               |                    |             |
| Code Scanner            | ⬜ SonarQube                   |                             |                    |             |
| Infrastructure          | ✅ Thread-safe configuration   | ✅ .json files               |                    |             |
| Database                | ⬜ SQL                         | ⬜ LINQ2DB                   |                    |             |
| Other                   | ✅ Fody, ⬜ AspectInjector      | ⬜ Conditional wait          | ⬜ BrowserStack API | ✅ Humanizer |

## Limitations
SpecFlow+ Runner is deprecated, so we're using NUnit test runner. NUnit test runner limits our SpecFlow features to have per-feature parallel execution.
That means, that we're unable to run scenarios within the same feature in parallel.

Once the test runner starts executing SpecFlow feature, it will complete all synchronous scenarios within the same thread.
That gives us an ability to define a static configuration per-scenario, using [ThreadStatic] attributes and [BeforeScenario] hooks together.

## Configuration
In order to make a custom Configuration, we need to 
1. Inherit from IAutomationConfiguration class.
2. Override needed methods from IAutomationConfiguration class.
3. Create [BeforeTestRun] hook and register your Configuration class as IAutomationConfiguration.
4. Add specflow assembly reference of AutomationFramework in your specflow configuration(specflow.json), so the Hooks from AutomationFramework will be executed and registered configuration will take its effect.
5. Make sure to use configuration only after it is being initialized in Hooks from AutomationFramework
