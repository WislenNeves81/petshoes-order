using Adapter.Stock.Sync.Model;
using Refit;

namespace Adapter.Stock.Sync.Gateway.Interfaces
{
    internal interface IStockSyncRequest
    {
        [Put("/Stock/{itemStockId}")]
        Task<ApiResponse<SyncBaseResponse<StockDefaultResponse>>> PutStockAsync(Guid itemStockId, [Body] SyncStockChangeInput stockInput);
    }
}
