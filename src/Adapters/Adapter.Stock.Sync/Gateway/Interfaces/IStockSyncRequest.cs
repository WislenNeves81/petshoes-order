using Adapter.Stock.Sync.Model;
using Refit;

namespace Adapter.Stock.Sync.Gateway.Interfaces
{
    internal interface IStockSyncRequest
    {
        [Put("/Stock")]
        Task<ApiResponse<SyncBaseResponse<StockDefaultResponse>>> PutStockAsync([Query] Guid stockId, [Body] SyncStockChangeInput stockChangeInput);
    }
}
