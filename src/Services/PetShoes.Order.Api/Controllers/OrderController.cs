using Microsoft.AspNetCore.Mvc;
using PetShoes.Order.Application.AppPurchaseOrder.Input;
using PetShoes.Order.Application.AppPurchaseOrder.Interface;

namespace PetShoes.Order.Api.Controllers
{
    [Route("petshoes/api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IPurchaseOrderAppService _purchaseOrderAppService;

        public OrderController(IPurchaseOrderAppService purchaseOrderAppService)
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

            return Ok(itemStock);
        }
    }
}
