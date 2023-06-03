# .NET-SpecFlow

## Secrets
Local: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets

optional plugin for Rider to manage local secrets: https://plugins.jetbrains.com/plugin/10183--net-core-user-secrets
## Project structure
| Category                | Implementations               |                             |                       |              |
|-------------------------|-------------------------------|-----------------------------|-----------------------|--------------|
| High level testing tool | ✅ BrowserStack Automate (Web) | ⬜ BrowserStack App Automate |                       |              |
| Testing UI tool         | ✅ Selenium (Aquality)         | ✅ Appium (Aquality)         |                       |              |
| Testing non-UI tool     | ⬜ REST                        | ⬜ WCF                       | ⬜ SignalR(WebSockets) |              |
| BDD tool                | ✅ Specflow                    |                             |                       |              |
| Report tool             | ⬜ Aquality Tracking           | ⬜ Allure                    | ⬜ ExtentReports       |              |
| Code                    | ✅ C# with .NET 6.0            |                             |                       |              |
| Logger                  | ✅ NLog 5                      |                             |                       |              |
| Test Runner             | ✅ NUnit 3                     |                             |                       |              |
| CI/CD                   | ✅ GitHub Actions              | ✅ Jenkins                   | ⬜ Azure               | ⬜ CircleCI   |
| Secrets management      | ✅ Github                      | ✅ Jenkins                   | ✅ Azure               | ✅ Local      |
| Assertions              | ✅ FluentAssertions            |                             |                       |              |
| Visual Testing          | ⬜ OpenCV                      | ⬜ Tesseract                 | ⬜ Applitools Eyes     |              |
| Test Management         | ⬜ TestRail                    | ⬜ XRay                      |                       |              |
| App Provider            | ⬜ AppCenter API               |                             |                       |              |
| Data Generator          | ⬜ Faker                       | ⬜ AutoFixture               |                       |              |
| Code Scanner            | ⬜ SonarQube                   |                             |                       |              |
| Infrastructure          | ✅ Thread-safe configuration   | ✅ .json files               |                       |              |
| Other                   | ✅ Fody                        | ⬜ AspectInjector            | ⬜ BrowserStack API    | ✅ Humanizer  |
