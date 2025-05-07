namespace PetShoes.Order.Application.AppPurchaseOrder.Input
{
    public class PurchaseOrderInput
    {
        public PurchaseOrderInput() { }

        public PurchaseOrderInput(Guid userId,
                                    string paymentMethod,
                                    string shippingAddress,
                                    List<OrderItemInput> items)
        {
            UserId = userId;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            Items = items ?? new List<OrderItemInput>();
        }
        public Guid UserId { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public List<OrderItemInput> Items { get; set; } = new List<OrderItemInput>();
    }
    public class OrderItemInput
    {
        public Guid ProductId { get; private set; }
        public Guid StockId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

    }
}
