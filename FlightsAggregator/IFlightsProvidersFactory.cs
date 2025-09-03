namespace FlightsAggregator;

public interface IFlightsProvidersFactory
{
    int WaitResponseTimeOutSecs { get; }

    IFlightsProvider[] GetFlightsProviders();
}