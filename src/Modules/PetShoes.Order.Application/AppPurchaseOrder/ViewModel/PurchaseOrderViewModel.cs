using PetShoes.Order.Domain.Entities;

namespace PetShoes.Order.Application.AppPurchaseOrder.ViewModel
{
    public class PurchaseOrderViewModel
    {

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PaymentMethod { get; set; }
        public string ShippingAddress { get; set; }
        public bool IsPaymentApproved { get; set; }
        public decimal TotalPurchase { get; set; }
        public List<PurchaseOrderItemViewModel> Items { get; set; }

        public class PurchaseOrderItemViewModel
        {
            public PurchaseOrderItemViewModel() { }
            public PurchaseOrderItemViewModel(Guid productId,
                                        Guid stockId,
                                        int quantity,
                                        decimal price)
            {
                ProductId = productId;
                StockId = stockId;
                Quantity = quantity;
                Price = price;
            }
            public Guid ProductId { get; set; }
            public Guid StockId { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

    }
}
