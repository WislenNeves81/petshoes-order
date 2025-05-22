using Adapter.Stock.Sync.Configurations;
using Adapter.Stock.Sync.Interfaces;
using Marraia.MongoDb.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProfit.Foundation.Redis.Configurations;
using PetShoes.Order.Domain.Interfaces;
using PetShoes.Order.Infrastructure.Repositories.Repository;

namespace PetShoes.Order.Infrastructure.IoC.Repository
{
    internal class RepositoryBootstrapper
    {
        internal void ChildServiceRegister(IServiceCollection service, IConfiguration configuration)
        {
            service.AddMongoDb();
            service.AddRedis(configuration);
            service.AddScoped<IPurchaseOrderRepository, PurchaseOrderRepository>();
            service.AddStockSync(configuration.GetSection("StockSync:Url").Value!);
        }
    }
}
