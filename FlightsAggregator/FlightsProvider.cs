using System.Text.Json;

namespace FlightsAggregator;

public class FlightsProvider(HttpClient httpClient, string apiUrl) : IFlightsProvider
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiUrl = apiUrl;

    public async Task<Flight[]> GetFlightsAsync()
    {
        var response = await _httpClient.GetAsync(_apiUrl);
        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var deserializedData = JsonSerializer.Deserialize<Flight[]>(jsonResponse,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return deserializedData ?? []; 
    }
}