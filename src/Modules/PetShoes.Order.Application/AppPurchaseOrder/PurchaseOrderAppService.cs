using Microsoft.VisualBasic;
using MyProfit.Foundation.Redis.Repositories.Interfaces;
using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.Interface;
using PetShoes.Order.Application.AppPurchaseOrder.ViewModel;
using PetShoes.Order.Domain.Entities;
using PetShoes.Order.Domain.Entities.ValueObjects;
using PetShoes.Order.Domain.Interfaces;

namespace PetShoes.Order.Application.AppPurchaseOrder
{
    public class PurchaseOrderAppService : IPurchaseOrderAppService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ICacheRepository _cacheRepository;
        public PurchaseOrderAppService(IPurchaseOrderRepository purchaseOrderRepository, ICacheRepository cacheRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _cacheRepository = cacheRepository;
        }

        public async Task<PurchaseOrderViewModel> InsertAsync(PurchaseOrderInput purchaseOrderInput)
        {
           
            var purchaseOrderItem = new List<PurchaseOrderItem>((IEnumerable<PurchaseOrderItem>)purchaseOrderInput.Items);

            var purchaseOrder = new PurchaseOrder(purchaseOrderInput.UserId,
                                                        purchaseOrderInput.PaymentMethod,
                                                        purchaseOrderInput.ShippingAddress,
                                                        purchaseOrderItem);
            if (purchaseOrderInput.Items.Count == 0)
                throw new Exception("Nenhum item foi adicionado ao pedido.");

            foreach (var item in purchaseOrderInput.Items)
            {
                if (item.Quantity <= 0)
                    throw new Exception("A quantidade do item deve ser maior que zero.");

                var keyStock = $"Stock :: Product ID: {item.ProductId} - Item ID: {item.StockId}";

                var stockItem = await GetStockByCacheAsync(keyStock).ConfigureAwait(false);

                if (StockValidation(stockItem, item.Quantity))
                {
                    stockItem.Quantity -= item.Quantity;

                    var keyShoeCatalog = $"Stock :: Product ID: {stockItem.ProductId} - Item ID: {stockItem.StockId}";

                    await _cacheRepository
                             .InsertAsync<StockValueObject>(keyShoeCatalog, stockItem)
                             .ConfigureAwait(false);

                }

            }


            //BUSCAR NO REDIS OS PRODUTOS E VERIFICAR SE TEM EM ESTOQUE

            //EM CASO POSITIVO, ATUALIZAR NO REDIS E NO BANCO DE DADOS

            //EM CASO NEGATIVO, RETORNAR ERRO

            //INSERIR NO BANCO DE DADOS

            //ENVIAR EMAIL INFORMANDO A COMPRA

            //VALIDAR O PAGAMENTO

            //ATUALIZAR O STATUS DO PAGAMENTO

            //ATUALIZAR O STATUS DO PEDIDO

            //ENVIAR EMAIL DE PAGAMENTO APROVADO

            return default;
        }

        #region 
        public async Task<StockValueObject> GetStockByCacheAsync(string keyStock)
        {
            var currentStock = await _cacheRepository
                                        .GetByKeyAsync<IEnumerable<StockValueObject>>(keyStock)
                                        .ConfigureAwait(false);

            return currentStock?.FirstOrDefault();
        }
        public bool StockValidation(StockValueObject stockItem, int quantity)
        {
            if (stockItem == null)
                throw new Exception($"O item {stockItem.ProductId} não foi encontrado no estoque.");
            if (stockItem.Quantity < quantity)
                throw new Exception($"O item {stockItem.ProductId} não possui estoque suficiente. Estoque atual: {stockItem.Quantity} - Quantidade solicitada: {quantity}");
            return true;
        }
        #endregion
    }
}
