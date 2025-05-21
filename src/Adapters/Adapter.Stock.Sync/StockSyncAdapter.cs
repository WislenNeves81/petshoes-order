using Adapter.Stock.Sync.Gateway.Interfaces;
using Adapter.Stock.Sync.Interfaces;
using Adapter.Stock.Sync.Model;

namespace Adapter.Stock.Sync
{
    internal class StockSyncAdapter : IStockSyncAdapter
    {
        private readonly IStockSyncClient _stockSyncClient;

        public StockSyncAdapter(IStockSyncClient stockSyncClient) 
        {
            _stockSyncClient = stockSyncClient;
        }
       
        public async Task<Guid> PutChangeStockAsync(Guid stockId, SyncStockChangeInput stockChangeInput)
        {

            var request = await _stockSyncClient
                                    .PutStockAsync(stockId, stockChangeInput)
                                    .ConfigureAwait(false); ;

            if (request.Success)
                return request.Result;

            return default!;
        }
    }
}
