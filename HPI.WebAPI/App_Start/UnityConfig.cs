using System.Web.Http;
using Unity;
using Unity.WebApi;
using HPI.BusinessServices;
using HPI.DataAccessLayer.UnitOfWork;
using Unity.Lifetime;

namespace HPI.WebAPI
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<IProductServices, ProductServices>().RegisterType<UnitOfWork>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

       
    }
}