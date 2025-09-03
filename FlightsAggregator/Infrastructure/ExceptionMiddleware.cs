using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;

namespace FlightsAggregator.Infrastructure;

public class ExceptionMiddleware(ILogger<ExceptionMiddleware> log) : IFunctionsWorkerMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _log = log;

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _log.LogError($"An unhandled exception occurred: {ex.Message}");
            throw;
        }
    }
}