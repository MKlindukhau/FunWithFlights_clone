namespace FlightsAggregator;

public interface IFlightsAggregatorService
{
    Task<Flight[]> GetAllFlightsAsync();
}