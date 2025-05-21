using Adapter.Stock.Sync.Model;

namespace Adapter.Stock.Sync.Gateway.Interfaces
{
    internal interface IStockSyncClient
    {
        Task<ResponseStock<Guid>> PutStockAsync(Guid stockId, SyncStockChangeInput stockChangeInput);
    }
}
