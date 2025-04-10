namespace PetShoes.Order.Application.AppPurchaseOrder.ViewModel
{
    public class PurchaseOrderViewModel
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }

        public PurchaseOrderViewModel(Guid id,
                                      Guid customerId,
                                      ProductViewModel product,
                                      int quantity)
        {
            Id = id;
            CustomerId = customerId;
            Product = product;
            Quantity = quantity;
        }
    }

    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public ProductViewModel(Guid id, string size, int quantity, decimal price)
        {
            Id = id;
            Size = size;
            Quantity = quantity;
            Price = price;
        }
    }
}
