using Marraia.MongoDb.Core;

namespace PetShoes.Order.Domain.Entities
{
    public class Shoe
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Shoe(Guid id, string size, int quantity, decimal price)
        {
            Id = id;
            Size = size;
            Quantity = quantity;
            Price = price;
        }
    }
    public class PurchaseOrder : Entity<Guid>
    {
        public PurchaseOrder(Guid customerId,
                             Shoe shoe)
        {
            CustomerId = customerId;
            Shoe = shoe;

            SetDefaultValues();

        }
        public Guid CustomerId { get; private set; }
        public Shoe Shoe { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; }
        public bool Paid { get; private set; }

        public void UpdateProduct(Shoe product)
        {
            Shoe = product;
        }

        public void UpdateCustomer(Guid customerId)
        {
            CustomerId = customerId;
        }

        private void SetDefaultValues()
        {
            Id = Guid.NewGuid();
            Active = true;
        }
    }
}
