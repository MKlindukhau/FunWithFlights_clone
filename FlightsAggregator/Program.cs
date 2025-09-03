using FlightsAggregator.Infrastructure;
using FlightsAggregator.Services;
using FlightsAggregator.Settings;
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
builder.Services.AddHttpClient();

builder.Services.AddOptions<ApiUrlsOptions>().Configure<IConfiguration>((options, configuration) =>
{
    configuration.GetSection(ApiUrlsOptions.ApiUrlsSettings).Bind(options);
});

builder.UseMiddleware<ExceptionMiddleware>();

builder.Build().Run();
