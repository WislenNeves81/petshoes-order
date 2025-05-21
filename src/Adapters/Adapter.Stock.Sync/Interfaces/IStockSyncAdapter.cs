using Adapter.Stock.Sync.Model;

namespace Adapter.Stock.Sync.Interfaces
{
    public interface IStockSyncAdapter
    {
        Task<Guid> PutChangeStockAsync(Guid stockId, SyncStockChangeInput stockChangeInput);
    }
}
