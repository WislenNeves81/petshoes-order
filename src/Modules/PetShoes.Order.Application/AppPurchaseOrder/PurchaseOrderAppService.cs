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
            //var shoes = purchaseOrderInput.Shoe.Select(p => new Shoe(p.Id, p.Size, p.Quantity, p.Price)).ToList();
            //var purchaseOrder = new PurchaseOrder(purchaseOrderInput.CustomerId, shoes);

            //await _purchaseOrderRepository
            //            .Insert(purchaseOrder)
            //            .ConfigureAwait(false);

            //return new PurchaseOrderViewModel(purchaseOrder.Id, purchaseOrder.CustomerId, purchaseOrder.Shoes, purchaseOrder.Shoes.Sum(s => s.Quantity));

            return default;
        }
    }
}
