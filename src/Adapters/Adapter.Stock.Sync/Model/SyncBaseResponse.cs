namespace Adapter.Stock.Sync.Model
{
    internal class SyncBaseResponse<T>
        where T : class
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }

    public class StockDefaultResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
       
    }
}
