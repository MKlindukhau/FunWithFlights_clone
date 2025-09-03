using FlightsAggregator;
using Microsoft.Extensions.Logging;
using Moq;

namespace FunWithFlights.Tests;

public class FlightsAggregatorServiceTests
{
    private readonly Mock<IFlightsProvidersFactory> _flightsProvidersFactoryMock;
    private readonly FlightsAggregatorServiceService _flightsAggregatorService;

    public FlightsAggregatorServiceTests()
    {
        _flightsProvidersFactoryMock = new Mock<IFlightsProvidersFactory>();
        var loggerMock = new Mock<ILogger<FlightsAggregatorServiceService>>();

        _flightsAggregatorService = new FlightsAggregatorServiceService(
            _flightsProvidersFactoryMock.Object,
            loggerMock.Object
        );
    }

    [Fact]
    public async Task GetAllFlightsAsync_EmptyProviders_ReturnsEmptyArray()
    {
        // Arrange
        _flightsProvidersFactoryMock
            .Setup(x => x.GetFlightsProviders())
            .Returns([]);

        // Act
        var result = await _flightsAggregatorService.GetAllFlightsAsync();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetAllFlightsAsync_ProvidersReturnFlights_ReturnsAggregatedFlights()
    {
        // Arrange
        var fakeProvider1 = new Mock<IFlightsProvider>();
        var fakeProvider2 = new Mock<IFlightsProvider>();

        var flightsFromProvider1 = new[]
        {
            new Flight("Airline1", "JFK", "LAX", "None", 1, "Boeing 737"),
            new Flight("Airline2", "LHR", "CDG", "None", 0, null)
        };
        var flightsFromProvider2 = new[]
        {
            new Flight("Airline3", "ORD", "MIA", "None", 0, null)
        };

        fakeProvider1
            .Setup(x => x.GetFlightsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(flightsFromProvider1);

        fakeProvider2
            .Setup(x => x.GetFlightsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(flightsFromProvider2);

        _flightsProvidersFactoryMock
            .Setup(x => x.GetFlightsProviders())
            .Returns([fakeProvider1.Object, fakeProvider2.Object]);

        _flightsProvidersFactoryMock
            .Setup(x => x.WaitResponseTimeOutSecs)
            .Returns(10);

        // Act
        var result = await _flightsAggregatorService.GetAllFlightsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Length);
        Assert.Contains(result, f => f.Airline == "Airline1");
        Assert.Contains(result, f => f.Airline == "Airline2");
        Assert.Contains(result, f => f.Airline == "Airline3");
    }

    [Fact]
    public async Task GetAllFlightsAsync_ProviderReturnsEmptyArrayOnTimeout_AggregatesAllResults()
    {
        // Arrange
        var fakeProvider1 = new Mock<IFlightsProvider>();
        var fakeProvider2 = new Mock<IFlightsProvider>();

        // Simulate successful flight retrieval by one provider
        var flightsFromProvider1 = new[]
        {
            new Flight("Airline1", "JFK", "LAX", "None", 1, "Boeing 737")
        };

        fakeProvider1
            .Setup(x => x.GetFlightsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(flightsFromProvider1);

        // Simulate a timeout scenario (provider returns an empty array)
        fakeProvider2
            .Setup(x => x.GetFlightsAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        _flightsProvidersFactoryMock
            .Setup(x => x.GetFlightsProviders())
            .Returns([fakeProvider1.Object, fakeProvider2.Object]);

        _flightsProvidersFactoryMock
            .Setup(x => x.WaitResponseTimeOutSecs)
            .Returns(10);

        // Act
        var result = await _flightsAggregatorService.GetAllFlightsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result); // Only one flight from provider1
        Assert.Contains(result, f => f.Airline == "Airline1");
    }
}