using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FlightsAggregator;

public class FlightsProvider(HttpClient httpClient, string apiUrl, ILogger<FlightsProvider> logger) : IFlightsProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiUrl = apiUrl;
    private readonly ILogger<FlightsProvider> _logger = logger;

    public async Task<Flight[]> GetFlightsAsync(CancellationToken waitResponseCancellationToken)
    {
        try
        {
            var response = await _httpClient.GetAsync(_apiUrl, waitResponseCancellationToken);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync(waitResponseCancellationToken);

            var deserializedData = JsonSerializer.Deserialize<Flight[]>(jsonResponse,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});

            return deserializedData ?? [];
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning($"Request to {_apiUrl} was canceled due to timeout");

            return [];
        }
    }
}