using FlightsAggregator.Models;
using Microsoft.Extensions.Logging;

namespace FlightsAggregator.Services;

public class FlightsAggregatorServiceService(IFlightsProvidersFactory flightsProvidersFactory, ILogger<FlightsAggregatorServiceService> logger) : IFlightsAggregatorService
{
    private readonly IFlightsProvidersFactory _flightsProvidersFactory = flightsProvidersFactory;
    private readonly ILogger<FlightsAggregatorServiceService> _logger = logger;

    public async Task<Flight[]> GetAllFlightsAsync()
    {
        var providers = _flightsProvidersFactory.GetFlightsProviders();

        _logger.LogInformation("Providers are initialized");

        if (providers.Length == 0)
        {
            _logger.LogWarning("Empty providers");

            return [];
        }

        var timeout = TimeSpan.FromSeconds(_flightsProvidersFactory.WaitResponseTimeOutSecs);
        using var cts = new CancellationTokenSource(timeout);
        var getFLightsTasks = providers.Select(x => x.GetFlightsAsync(cts.Token)).ToArray();
        var results = await Task.WhenAll(getFLightsTasks);


        return results.SelectMany(x => x).ToArray();
    }
}