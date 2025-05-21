namespace Adapter.Stock.Sync.Model
{
    public class SyncStockChangeInput
    {
        public SyncStockChangeInput(int quantity)
        {
            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;

        }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
