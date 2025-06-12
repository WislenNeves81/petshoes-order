using PetShoes.Order.Application.AppPurchaseOrder.ViewModel;
using PetShoes.Order.Domain.Entities;
using static PetShoes.Order.Application.AppPurchaseOrder.ViewModel.PurchaseOrderViewModel;

namespace PetShoes.Order.Application.AppPurchaseOrder.Mapping
{
    public static class PurchaseOrderMapping
    {
        public static PurchaseOrderViewModel ToViewModel(this PurchaseOrder purchaseOrder)
        {
            return new PurchaseOrderViewModel
            {
                Id = purchaseOrder.Id,
                UserId = purchaseOrder.UserId,
                PaymentMethod = purchaseOrder.PaymentType,
                ShippingAddress = purchaseOrder.ShippingAddress,
                TotalPurchase = purchaseOrder.TotalPurchase,
                Items = purchaseOrder.Items.Select(item => new PurchaseOrderItemViewModel
                {
                    ProductId = item.ProductId,
                    StockId = item.StockId,
                    Quantity = item.Quantity,
                    Price = item.Price
                }).ToList()
            };
        }
    }
}
