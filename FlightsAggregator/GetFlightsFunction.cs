using FlightsAggregator.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace FlightsAggregator;

public class GetFlightsFunction(IFlightsAggregatorService flightsAggregatorService, ILogger<GetFlightsFunction> logger)
{
    private readonly ILogger<GetFlightsFunction> _logger = logger;
    private readonly IFlightsAggregatorService _flightsAggregatorService = flightsAggregatorService;

    [Function("GetFlightsFunction")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "routes")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        var flights = await _flightsAggregatorService.GetAllFlightsAsync();

        return new OkObjectResult(flights);
    }
}