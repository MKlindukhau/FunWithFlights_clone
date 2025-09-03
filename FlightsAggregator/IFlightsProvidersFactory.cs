namespace FlightsAggregator;

public interface IFlightsProvidersFactory
{
    IFlightsProvider[] GetFlightsProviders();
}