using FlightsAggregator.Models;

namespace FlightsAggregator.Services;

public interface IFlightsAggregatorService
{
    Task<Flight[]> GetAllFlightsAsync();
}