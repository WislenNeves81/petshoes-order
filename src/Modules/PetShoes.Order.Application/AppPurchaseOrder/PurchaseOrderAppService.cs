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
           
            var purchaseOrderItem = new List<PurchaseOrderItem>((IEnumerable<PurchaseOrderItem>)purchaseOrderInput.Items);

            var purchaseOrder = new PurchaseOrder(purchaseOrderInput.UserId,
                                                        purchaseOrderInput.PaymentMethod,
                                                        purchaseOrderInput.ShippingAddress,
                                                        purchaseOrderItem);

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
    }
}
