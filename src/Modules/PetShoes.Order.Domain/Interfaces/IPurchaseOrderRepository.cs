using PetShoes.Order.Domain.Entities;

namespace PetShoes.Order.Domain.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task Insert(PurchaseOrder purchaseOrder);
       
    }
}
