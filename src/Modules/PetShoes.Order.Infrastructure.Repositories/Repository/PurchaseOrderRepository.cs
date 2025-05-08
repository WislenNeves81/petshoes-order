using Marraia.MongoDb.Repositories;
using Marraia.MongoDb.Repositories.Interfaces;
using PetShoes.Order.Domain.Entities;
using PetShoes.Order.Domain.Interfaces;

namespace PetShoes.Order.Infrastructure.Repositories.Repository
{
    public class PurchaseOrderRepository : MongoDbRepositoryStandard<PurchaseOrder, Guid>, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(IMongoContext context) : base(context) { }

        public async Task InsertAsync(PurchaseOrder purchaseOrder)
        {
            await Collection
                  .InsertOneAsync(purchaseOrder)
                  .ConfigureAwait(false);
        }
    }
}
