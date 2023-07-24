using AutomationAPI.Configuration;
using AutomationAPI.OpenAPI;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AutomationAPI.StepDefinitions;

[Binding]
public class OpenApiStepDefinitions
{
    private readonly ScenarioContext scenarioContext;
    private const string OpenApiResponseAlias = "OpenAPIResponse";

    public OpenApiStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }

    [Given(@"WeatherForecastService is initialized")]
    public void GivenWeatherForecastServiceIsInitialized()
    {
        var httpclient = scenarioContext.Get<HttpClient>();

        var weatherForecastService = new WeatherForecastService(AutomationApiConfiguration.OpenApiData.OpenAPIEndpoint.ToString(), httpclient);

        scenarioContext.Set(weatherForecastService);
    }

    [When(@"I send GetWeatherForecast GET request")]
    public void WhenISendGetWeatherForecastGetRequest()
    {
        var weatherForecastService = scenarioContext.Get<WeatherForecastService>();
        var weatherForecasts = weatherForecastService.GetWeatherForecastAsync().Result;
        scenarioContext.Set(weatherForecasts, OpenApiResponseAlias);
    }

    [Then(@"I assert that response contains Weather Forecast for (.*) days")]
    public void ThenIAssertThatResponseContainsWeatherForecastForDays(int days)
    {
        var weatherForecasts = scenarioContext.Get<ICollection<WeatherForecast>>(OpenApiResponseAlias);

        weatherForecasts.Count.Should().Be(days);

        var dayOffset = 1;
        var dateTimeNow = DateTimeOffset.Now;
        foreach (var weatherForecast in weatherForecasts)
        {
            weatherForecast.Date.Offset.Should().Be(dateTimeNow.Offset, "Date offset should match expected offset");
            weatherForecast.Date.Day.Should().Be(dateTimeNow.AddDays(dayOffset).Day);
            dayOffset++;
        }
    }
}
