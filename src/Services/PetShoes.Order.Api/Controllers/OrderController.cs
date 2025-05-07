using Microsoft.AspNetCore.Mvc;
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
    }
}
