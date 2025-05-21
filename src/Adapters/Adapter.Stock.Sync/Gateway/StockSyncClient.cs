using Adapter.Stock.Sync.Gateway.Interfaces;
using Adapter.Stock.Sync.Model;
using Polly;

namespace Adapter.Stock.Sync.Gateway
{
    internal class StockSyncClient : IStockSyncClient
    {
        private readonly IStockSyncRequest _stockSyncRequest;
        private readonly AsyncPolicy _asyncPolicy;
        public StockSyncClient(IStockSyncRequest stockSyncRequest, AsyncPolicy asyncPolicy)
        {
            _stockSyncRequest = stockSyncRequest;
            _asyncPolicy = asyncPolicy;
        }
        public async Task<ResponseStock<Guid>> PutStockAsync(Guid stockId, SyncStockChangeInput stockChangeInput)
        {
            var response = new ResponseStock<Guid>();

            try
            {
                var request = await _asyncPolicy
                                        .ExecuteAsync(async () => await _stockSyncRequest
                                                                            .PutStockAsync(stockId, stockChangeInput)
                                                                            .ConfigureAwait(false));

                if (request.IsSuccessStatusCode)
                    response.WithSuccess(request.Content!.Data.Id);
                else
                    response.WithError($"{request.StatusCode} - {request.Error}");

            }
            catch (Exception ex)
            {

                response.WithError(ex.Message);
            }

            throw new NotImplementedException();
        }
    }
}
