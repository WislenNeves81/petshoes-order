using Microsoft.Extensions.DependencyInjection;
using PetShoes.Order.Application;
using PetShoes.Order.Application.AppPurchaseOrder.Interface;

namespace PetShoes.Order.Infrastructure.IoC.Application
{
    internal class ApplicationBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection service)
        {
            service.AddScoped<IPurchaseOrderAppService, PurchaseOrderAppService>();
        }
    }
}
