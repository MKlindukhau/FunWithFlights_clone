using FlightsAggregator.Models;

namespace FlightsAggregator.Services;

public interface IFlightsProvider
{
    Task<Flight[]> GetFlightsAsync(CancellationToken waitResponseCancellationToken);
}