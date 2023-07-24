using AutomationAPI.Configuration;
using AutomationAPI.Models;
using AutomationFramework.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace AutomationAPI.StepDefinitions;

[Binding]
public class HttpApiStepDefinitions
{
    private readonly ScenarioContext scenarioContext;
    private const string HttpApiResponseAlias = "HttpAPIResponse";

    public HttpApiStepDefinitions(ScenarioContext scenarioContext)
    {
        this.scenarioContext = scenarioContext;
    }
    
    [Given(@"HTTP connection is opened")]
    public void GivenHttpConnectionIsOpened()
    {
        var httpclient = new HttpClient();

        scenarioContext.Set(httpclient);
    }

    [When(@"I send '(.*)' request via '(.*)' method by HTTP")]
    public void WhenISendRequestViaMethodByHttp(string requestPath, string httpMethod)
    {
        var httpClient = scenarioContext.Get<HttpClient>();
        
        var request = new HttpRequestMessage();
        request.Method = new HttpMethod(httpMethod);
        request.RequestUri = AutomationApiConfiguration.OpenApiData.OpenAPIEndpoint.Append(requestPath);

        var response = httpClient.Send(request);
        
        var weatherForecasts = JsonConvert.DeserializeObject<ICollection<WeatherForecastHttp>>(response.Content.ReadAsStringAsync().Result);

        scenarioContext.Set(weatherForecasts, HttpApiResponseAlias);
    }

    [Then(@"I assert that response contains Weather Forecast for (.*) days by HTTP")]
    public void ThenIAssertThatResponseContainsWeatherForecastForDaysByHttp(int days)
    {
        var weatherForecasts = scenarioContext.Get<ICollection<WeatherForecastHttp>>(HttpApiResponseAlias);

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
