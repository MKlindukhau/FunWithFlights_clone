namespace FlightsAggregator;

public interface IFlightsProvider
{
    Task<Flight[]> GetFlightsAsync();
}