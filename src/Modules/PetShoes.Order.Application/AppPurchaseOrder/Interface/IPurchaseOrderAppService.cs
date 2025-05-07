using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.ViewModel;

namespace PetShoes.Order.Application.AppPurchaseOrder.Interface
{
    public interface IPurchaseOrderAppService
    {
        Task<PurchaseOrderViewModel>InsertAsync(PurchaseOrderInput purchaseOrderInput);
        //Task UpdatePurchaseOrderAsync(Guid id, Guid customerId, Guid productId, int quantity);
        //Task DeletePurchaseOrderAsync(Guid id);
       
    }
}
