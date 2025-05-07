using System.Globalization;
using Marraia.MongoDb.Core;

namespace PetShoes.Order.Domain.Entities
{
    public class PurchaseOrder : Entity<Guid>
    {
        public PurchaseOrder()
        {
            Items = new List<PurchaseOrderItem>();
        }

        public PurchaseOrder(Guid userId, 
                                string paymentMethod, 
                                string shippingAddress,
                                List<PurchaseOrderItem> items)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Items = items ?? new List<PurchaseOrderItem>();
            PurchaseDate = DateTime.UtcNow;
            PaymentMethod = paymentMethod;
            ShippingAddress = shippingAddress;
            TotalPurchase = Items.Sum(item => item.Price * item.Quantity);
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public string PaymentMethod { get; private set; }
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
}
