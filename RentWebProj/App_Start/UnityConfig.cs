using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using RentWebProj.Interfaces;
using RentWebProj.Repositories;
using RentWebProj.Services;

namespace RentWebProj
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRedisRepository, RedisRepository>();
            container.RegisterType<IProductService, ProductService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}