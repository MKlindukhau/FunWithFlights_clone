using FlightsAggregator;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton<IFlightsProvidersFactory, FlightsProvidersFactory>();
builder.Services.AddSingleton<IFlightsAggregatorService, FlightsAggregatorServiceService>();
builder.Services.AddHttpClient<FlightsProvidersFactory>();

builder.Services.AddOptions<ApiUrlsOptions>().Configure<IConfiguration>((options, configuration) =>
{
    configuration.GetSection(ApiUrlsOptions.ApiUrlsSettings).Bind(options);
});

builder.Build().Run();
