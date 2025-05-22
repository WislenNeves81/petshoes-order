namespace Adapter.Stock.Sync.Model
{
    public class SyncStockChangeInput
    {
        public SyncStockChangeInput(int quantity)
        {
            Quantity = quantity;

        }
        public int Quantity { get; set; }
    }
}
