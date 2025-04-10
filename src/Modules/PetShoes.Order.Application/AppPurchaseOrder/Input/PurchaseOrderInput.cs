namespace PetShoes.Order.Application.AppPurchaseOrder.Input
{
    public class PurchaseOrderInput
    {
        public Guid CustomerId { get; set; }
        public List<ShoeInput> Shoe { get; set; }

        public PurchaseOrderInput(Guid customerId, List<ShoeInput> shoes)
        {
            CustomerId = customerId;
            Shoe = shoes;
        }
    }

    public class ShoeInput
    {
        public Guid Id { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
