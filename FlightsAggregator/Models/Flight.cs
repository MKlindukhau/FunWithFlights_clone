namespace FlightsAggregator.Models;

public record Flight(string Airline, string SourceAirport, string DestinationAirport, string CodeShare, int Stops,  string? Equipment);