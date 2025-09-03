using Microsoft.Extensions.Logging;

namespace FlightsAggregator;

public class FlightsAggregatorServiceService(IFlightsProvidersFactory flightsProvidersFactory, ILogger<FlightsProvidersFactory> logger) : IFlightsAggregatorService
{
    private readonly IFlightsProvidersFactory _flightsProvidersFactory = flightsProvidersFactory;
    private readonly ILogger<FlightsProvidersFactory> _logger = logger;

    public async Task<Flight[]> GetAllFlightsAsync()
    {
        var providers = _flightsProvidersFactory.GetFlightsProviders();

        _logger.LogInformation("Providers are initialized");

        if (providers.Length == 0)
        {
            _logger.LogInformation("Empty providers");

            return [];
        }

        var getFLightsTasks = providers.Select(x => x.GetFlightsAsync());
        var results = await Task.WhenAll(getFLightsTasks);

        return results.SelectMany(x => x).ToArray();
    }
}