namespace PetShoes.Order.Application.AppPurchaseOrder.Interface
{
    public interface IPurchaseOrderAppService
    {
        Task<Guid> CreatePurchaseOrderAsync(Guid customerId, Guid productId, int quantity);
        Task UpdatePurchaseOrderAsync(Guid id, Guid customerId, Guid productId, int quantity);
        Task DeletePurchaseOrderAsync(Guid id);
       
    }
}
