using Polly.Retry;
using Polly;
using Refit;
using Microsoft.Extensions.DependencyInjection;
using Adapter.Stock.Sync.Interfaces;
using Adapter.Stock.Sync.Gateway.Interfaces;
using Adapter.Stock.Sync.Gateway;
using System.Net;

namespace Adapter.Stock.Sync.Configurations
{
    public static class StockSyncExtensionConfiguration
    {
        public static IServiceCollection AddStockSync(this IServiceCollection service, string urlService)
        {
            service.AddScoped<IStockSyncAdapter, StockSyncAdapter>();
            service.AddScoped<IStockSyncClient, StockSyncClient>();

            service
                .AddSingleton<AsyncPolicy>
                (CreateWaitAndRetryPolicy(new[]
                {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    TimeSpan.FromSeconds(15)
                }));

            service
                .AddRefitClient<IStockSyncRequest>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(urlService));

            return service;
        }

        public static AsyncRetryPolicy CreateWaitAndRetryPolicy(IEnumerable<TimeSpan> sleepsBeetweenRetries)
        {
            return Policy
                .Handle<HttpRequestException>()
                .Or<ApiException>(ex => ((int)ex.StatusCode) >= ((int)HttpStatusCode.BadRequest))
                .WaitAndRetryAsync(
                    sleepDurations: sleepsBeetweenRetries,
                    onRetry: (ex, span, retry, _) =>
                    {
                        Console.WriteLine(ex);

                        var backgroundColor = Console.BackgroundColor;
                        var foregroundColor = Console.ForegroundColor;

                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;

                        Console.Out.WriteLineAsync($" {DateTime.Now:HH:mm:ss} | " +
                                                    $"Retentativa: {retry} | " +
                                                    $"Tempo de espera em segundos: {span.TotalSeconds} |" +
                                                    $"[ERROR] : {ex.Message}");

                        Console.BackgroundColor = backgroundColor;
                        Console.ForegroundColor = foregroundColor;
                    });
        }

    }
}
