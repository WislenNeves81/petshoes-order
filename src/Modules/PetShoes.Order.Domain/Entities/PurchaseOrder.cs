using System.Globalization;
using Marraia.MongoDb.Core;

namespace PetShoes.Order.Domain.Entities
{
    public class PurchaseOrder : Entity<Guid>
    {
        public PurchaseOrder(Guid userId, 
                                string paymentMethod, 
                                string shippingAddress,
                                List<PurchaseOrderItem> items)
        {
            UserId = userId;
            Items = items ?? new List<PurchaseOrderItem>();
            PurchaseDate = DateTime.UtcNow;
            PaymentType = paymentMethod;
            ShippingAddress = shippingAddress;
            TotalPurchase = Items.Sum(item => item.Price * item.Quantity);
        }
        public Guid UserId { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public string PaymentType { get; set; } = "CreditCard";
        public string ShippingAddress { get; set; }
        public bool IsPaymentApproved { get; private set; }
        public decimal TotalPurchase { get; private set; }
        public List<PurchaseOrderItem> Items { get; private set; }
        public void ApprovePayment()
        {
            IsPaymentApproved = true;
        }
        public void RejectPayment()
        {
            IsPaymentApproved = false;
        }
    }
    public class PurchaseOrderItem
    {
        public PurchaseOrderItem() {}
        public PurchaseOrderItem(Guid productId, 
                                    Guid stockId, 
                                    int quantity, 
                                    decimal price)
        {
            ProductId = productId;
            StockId = stockId;
            Quantity = quantity;
            Price = price;
        }
        public Guid ProductId { get; private set; }
        public Guid StockId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
    public class PurchaseOrderCreatedEvent
    {
        public string PaymentType { get; set; }
        public PurchaseOrderCreatedEvent(string paymentType)
        {
            PaymentType = paymentType;
        }
    }
}
