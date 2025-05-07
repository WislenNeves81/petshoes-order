using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.ViewModel;
using PetShoes.Order.Domain.Entities;
using PetShoes.Order.Domain.Interfaces;

namespace PetShoes.Order.Application.AppPurchaseOrder
{
    public class PurchaseOrderAppService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        public PurchaseOrderAppService(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<PurchaseOrderViewModel> InsertAsync(PurchaseOrderInput purchaseOrderInput)
        {
           

            return default;
        }
    }
}
