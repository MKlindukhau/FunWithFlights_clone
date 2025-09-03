namespace FlightsAggregator;

public class FlightsAggregatorServiceService(IFlightsProvidersFactory flightsProvidersFactory) : IFlightsAggregatorService
{
    private readonly IFlightsProvidersFactory _flightsProvidersFactory = flightsProvidersFactory;

    public async Task<Flight[]> GetAllFlightsAsync()
    {
        var providers = _flightsProvidersFactory.GetFlightsProviders();

        if (providers.Length == 0) return [];

        var getFLightsTasks = providers.Select(x => x.GetFlightsAsync());
        var results = await Task.WhenAll(getFLightsTasks);

        return results.SelectMany(x => x).ToArray();
    }
}