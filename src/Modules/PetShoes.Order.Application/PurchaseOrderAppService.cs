using Adapter.Stock.Sync.Interfaces;
using Adapter.Stock.Sync.Model;
using Marraia.Notifications.Interfaces;
using MyProfit.Foundation.Redis.Repositories.Interfaces;
using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.Interface;
using PetShoes.Order.Application.AppPurchaseOrder.Mapping;
using PetShoes.Order.Application.AppPurchaseOrder.ViewModel;
using PetShoes.Order.Domain.Entities;
using PetShoes.Order.Domain.Entities.ValueObjects;
using PetShoes.Order.Domain.Interfaces;

namespace PetShoes.Order.Application
{
    public class PurchaseOrderAppService : IPurchaseOrderAppService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ICacheRepository _cacheRepository;
        private readonly ISmartNotification _smartNotification;
        private readonly IStockSyncAdapter _stockSyncAdapter;
        public PurchaseOrderAppService(IPurchaseOrderRepository purchaseOrderRepository,
                                        ICacheRepository cacheRepository,
                                        ISmartNotification smartNotification,
                                        IStockSyncAdapter stockSyncAdapter)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _cacheRepository = cacheRepository;
            _smartNotification = smartNotification;
            _stockSyncAdapter = stockSyncAdapter;

        }

        public async Task<PurchaseOrderViewModel> InsertAsync(PurchaseOrderInput purchaseOrderInput)
        {

            var purchaseOrderItems = purchaseOrderInput.Items.Select(input => new PurchaseOrderItem(input.ProductId, input.StockId, input.Quantity, input.Price));

            var purchaseOrder = new PurchaseOrder(purchaseOrderInput.UserId,
                                                        purchaseOrderInput.PaymentMethod,
                                                        purchaseOrderInput.ShippingAddress,
                                                        purchaseOrderItems.ToList());
            if (purchaseOrderInput.Items.Count == 0)
            {
                _smartNotification
                   .NewNotificationConflict($"Nenhum item foi adicionado ao pedido {purchaseOrder.Id}.");

                return default!;
            }

            foreach (var item in purchaseOrderInput.Items)
            {
                if (item.Quantity <= 0)
                {
                    _smartNotification.NewNotificationConflict($"A quantidade do item {item.ProductId} deve ser maior que zero.");

                    return default!;
                }

                var keyStock = $"stock:productId:{item.ProductId}:stockId:{item.StockId}";

                var stockItem = await GetStockByCacheAsync(keyStock).ConfigureAwait(false);

                if (stockItem == null)
                {
                    _smartNotification.NewNotificationConflict($"O item {item.ProductId} não foi encontrado no estoque.");

                    return default!;
                }

                if (StockValidation(stockItem, item.Quantity))
                {
                    stockItem.UpdateQuantity(item.Quantity);
                    stockItem.UpdatedAt = DateTime.Now;

                    var keyShoeCatalog = $"stock:productId:{stockItem.ProductId}:stockId:{stockItem.Id}";

                    await _cacheRepository
                             .InsertAsync(keyShoeCatalog, stockItem)
                             .ConfigureAwait(false);
                }

                await _stockSyncAdapter
                            .PutChangeStockAsync(stockItem.Id, new SyncStockChangeInput(stockItem.Quantity))
                            .ConfigureAwait(false);

            }

            await _purchaseOrderRepository
                            .InsertAsync(purchaseOrder)
                            .ConfigureAwait(false);

            var purchaseOrderViewModel = purchaseOrder.ToViewModel();

            //ENVIAR EMAIL INFORMANDO A COMPRA E O STATUS DO PAGAMENTO

            //ENVIAR PARA CONSUMER DE PGTO E AGUARDA O RETORNO

            //ENVIA EMAIL COM O STATUS DO PGTO

            //ATUALIZAR O STATUS DO PEDIDO

            //ENVIAR EMAIL DE PAGAMENTO APROVADO

            return default;
        }

        #region 
        public async Task<StockValueObject?> GetStockByCacheAsync(string keyStock)
        {
            var currentStock = await _cacheRepository
                                        .GetByKeyAsync<StockValueObject>(keyStock)
                                        .ConfigureAwait(false);

            return currentStock;
        }
        public bool StockValidation(StockValueObject stockItem, int quantity)
        {
            if (stockItem == null)
                _smartNotification.NewNotificationConflict($"O item não foi encontrado no estoque.");
            if (stockItem!.Quantity < quantity)
                _smartNotification.NewNotificationConflict($"O item {stockItem.ProductId} não possui estoque suficiente. Estoque atual: {stockItem.Quantity} - Quantidade solicitada: {quantity}");
            return true;
        }
        #endregion
    }
}
