namespace FlightsAggregator.Settings;

public class ApiUrlsOptions
{
    public const string ApiUrlsSettings = "ApiUrlsSettings";

    public string[] ApiUrls { get; set; }
    public int WaitResponseTimeOutSecs { get; set; }
}