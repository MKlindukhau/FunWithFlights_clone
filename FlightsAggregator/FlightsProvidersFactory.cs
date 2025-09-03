using Microsoft.Extensions.Options;

namespace FlightsAggregator;

public class FlightsProvidersFactory(IOptions<ApiUrlsOptions> apiUrlsOptions, HttpClient httpClient) : IFlightsProvidersFactory
{
    private readonly IOptions<ApiUrlsOptions> _apiUrlsOptions = apiUrlsOptions;
    private readonly HttpClient _httpClient = httpClient;

    public IFlightsProvider[] GetFlightsProviders()
    {
        var urls = _apiUrlsOptions.Value.ApiUrls;

        if (urls.Length == 0) return [];

        return Array.ConvertAll<string, IFlightsProvider>(_apiUrlsOptions.Value.ApiUrls,
            x => new FlightsProvider(_httpClient, x));
    }
}