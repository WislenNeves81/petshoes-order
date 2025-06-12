using Adapter.Email.Interfaces;
using Adapter.Stock.Sync.Interfaces;
using Marraia.Notifications.Interfaces;
using MassTransit;
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
        private readonly IEmailNotificationAdapter _emailNotificationAdapter;
        private readonly IPublishEndpoint _publishEndpoint;
        public PurchaseOrderAppService(IPurchaseOrderRepository purchaseOrderRepository,
                                        ICacheRepository cacheRepository,
                                        ISmartNotification smartNotification,
                                        IStockSyncAdapter stockSyncAdapter,
                                        IEmailNotificationAdapter emailNotificationAdapter,
                                        IPublishEndpoint publishEndpoint)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _cacheRepository = cacheRepository;
            _smartNotification = smartNotification;
            _stockSyncAdapter = stockSyncAdapter;
            _emailNotificationAdapter = emailNotificationAdapter;
            _publishEndpoint = publishEndpoint;

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

            //foreach (var item in purchaseOrderInput.Items)
            //{
            //    if (item.Quantity <= 0)
            //    {
            //        _smartNotification.NewNotificationConflict($"A quantidade do item {item.ProductId} deve ser maior que zero.");

            //        return default!;
            //    }

            //    var keyStock = $"stock:productId:{item.ProductId}:stockId:{item.StockId}";

            //    var stockItem = await GetStockByCacheAsync(keyStock).ConfigureAwait(false);

            //    if (stockItem == null)
            //    {
            //        _smartNotification.NewNotificationConflict($"O item {item.ProductId} não foi encontrado no estoque.");

            //        return default!;
            //    }

            //    await _stockSyncAdapter
            //                .PutChangeStockAsync(stockItem.Id, new SyncStockChangeInput(item.Quantity))
            //                .ConfigureAwait(false);

            //}

            await _purchaseOrderRepository
                            .InsertAsync(purchaseOrder)
                            .ConfigureAwait(false);

            //_emailNotificationAdapter.SendPurchaseOrderCreatedMail("Wislen", "wislen.neves@gmail.com");
            //ENVIAR EMAIL INFORMANDO A COMPRA E O STATUS DO PAGAMENTO

            await _publishEndpoint
                            .Publish(new PurchaseOrderCreatedEvent("CreditCard"))
                            .ConfigureAwait(false);


            // enviar o pedido para a fila

            //ENVIAR PARA CONSUMER DE PGTO E AGUARDA O RETORNO****

            //ENVIA EMAIL COM O STATUS DO PGTO

            //ATUALIZAR O STATUS DO PEDIDO

            //ENVIAR EMAIL DE PAGAMENTO APROVADO

            var purchaseOrderViewModel = purchaseOrder.ToViewModel();

            return purchaseOrderViewModel;
        }

        #region 
        public async Task<StockValueObject?> GetStockByCacheAsync(string keyStock)
        {
            var currentStock = await _cacheRepository
                                        .GetByKeyAsync<StockValueObject>(keyStock)
                                        .ConfigureAwait(false);

            return currentStock;
        }
        #endregion
    }
}
