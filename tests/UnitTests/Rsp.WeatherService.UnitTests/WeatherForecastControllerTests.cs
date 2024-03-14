using Moq.AutoMock;
using Rsp.WeatherService.WebApi;
using Rsp.WeatherService.WebApi.Controllers;
using Shouldly;

namespace Rsp.WeatherService.UnitTests;

public class WeatherForecastControllerTests
{
    private readonly AutoMocker _mocker;
    private readonly WeatherForecastController _controller;

    public WeatherForecastControllerTests()
    {
        _mocker = new AutoMocker();
        _controller = _mocker.CreateInstance<WeatherForecastController>();
    }

    [Fact]
    public void Get_Returns_IEnumerableOfWeatherForecast()
    {
        // Act
        var result = _controller.Get();

        // Assert
        result.ShouldBeAssignableTo<IEnumerable<WeatherForecast>>();
    }
}