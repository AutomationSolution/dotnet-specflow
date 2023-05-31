# .NET-SpecFlow

## Secrets
Local: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets

optional plugin for Rider to manage local secrets: https://plugins.jetbrains.com/plugin/10183--net-core-user-secrets

## Project structure
| Category            | Implementations     |           |                     |                 |
|---------------------|---------------------|-----------|---------------------|-----------------|
| Testing UI tool     | Selenium            | Appium    | Aquality Selenium   | Aquality Appium |
| Testing non-UI tool | REST                | WCF       | SignalR(WebSockets) |                 |
| BDD tool            | Specflow            |           |                     |                 |
| Report tool         | Aquality Tracking   | Allure    | ExtentReports       |                 |
| Code                | C# with .NET Core 6 |           |                     |                 |
| Logger              | NLog 5              |           |                     |                 |
| Test Runner         | NUnit 3             |           |                     |                 |
| CI/CD               | GitHub Actions      | Jenkins   | Azure               | CloudCI         |
| Secrets management  | Local               | Jenkins   | Azure               | Github          |
| Assertions          | FluentAssertions    |           |                     |                 |
| Visual Testing      | OpenCV              | Tesseract | Applitools Eyes     |                 |
| Other               | Fody                |           |                     |                 |
