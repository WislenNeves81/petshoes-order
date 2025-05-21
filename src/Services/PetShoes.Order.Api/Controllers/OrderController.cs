using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.Interface;

namespace PetShoes.Order.Api.Controllers
{
    [Route("petshoes/api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;

        public OrderController(IPurchaseOrderAppService purchaseOrderAppService, 
                               INotificationHandler<DomainNotification> notification)
        : base(notification)
        {
            _purchaseOrderAppService = purchaseOrderAppService;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PostAsync([FromBody] PurchaseOrderInput purchaseOrderInput)
        {
            var itemStock = await _purchaseOrderAppService
                                            .InsertAsync(purchaseOrderInput)
                                            .ConfigureAwait(false);

            return OkOrNotFound(itemStock);
        }
    }
}
