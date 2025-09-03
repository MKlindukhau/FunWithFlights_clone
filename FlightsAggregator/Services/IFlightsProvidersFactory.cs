namespace FlightsAggregator.Services;

public interface IFlightsProvidersFactory
{
    int WaitResponseTimeOutSecs { get; }

    IFlightsProvider[] GetFlightsProviders();
}