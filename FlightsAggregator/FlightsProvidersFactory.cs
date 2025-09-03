using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FlightsAggregator;

public class FlightsProvidersFactory : IFlightsProvidersFactory
{
    private readonly IOptions<ApiUrlsOptions> _apiUrlsOptions;
    private readonly HttpClient _httpClient;
    private readonly ILoggerFactory _loggerFactory;
    private readonly ILogger<FlightsProvidersFactory> _logger;

    public FlightsProvidersFactory(IOptions<ApiUrlsOptions> apiUrlsOptions, HttpClient httpClient, ILoggerFactory loggerFactory)
    {
        _apiUrlsOptions = apiUrlsOptions;
        _httpClient = httpClient;
        _loggerFactory = loggerFactory;

        _logger = _loggerFactory.CreateLogger<FlightsProvidersFactory>();
    }

    public int WaitResponseTimeOutSecs => _apiUrlsOptions.Value.WaitResponseTimeOutSecs;

    public IFlightsProvider[] GetFlightsProviders()
    {
        var urls = _apiUrlsOptions.Value.ApiUrls;

        if (urls.Length == 0)
        {
            _logger.LogInformation("Empty urls array");

            return [];
        }

        _logger.LogInformation($"Urls: {string.Join("; ", urls)}");

        return Array.ConvertAll<string, IFlightsProvider>(_apiUrlsOptions.Value.ApiUrls,
            x => new FlightsProvider(_httpClient, x, _loggerFactory.CreateLogger<FlightsProvider>()));
    }
}