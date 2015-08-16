using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using Arabia.Data;
using Arabia.Core.Data;
using Arabia.Data.Model;
using Arabia.Service.Lookups;

namespace Arabia.Service.Infrastructure
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var _container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();   

      _container.RegisterType<IDbContext, SportsEntities>(new PerResolveLifetimeManager());
      //_container.RegisterType<IDbContext, ArabiaEntities>();
      _container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));
         

        //Lookups
      _container.RegisterType<ICompetitionService, CompetitionService>(); 


      RegisterTypes(_container);

      return _container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}