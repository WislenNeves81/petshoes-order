using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetShoes.Order.Infrastructure.IoC.Application;
using PetShoes.Order.Infrastructure.IoC.Repository;

namespace PetShoes.Order.Infrastructure.IoC
{
    public class RootBootstrapper
    {
        public void BootstrapperRegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            new RepositoryBootstrapper().ChildServiceRegister(services, configuration);
            new ApplicationBootstrapper().ChildServiceRegister(services);
        }

    }
}
