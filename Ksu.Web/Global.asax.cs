using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Arabia.Service.Infrastructure;
using Arabia.Web.Common;
using Arabia.Core.Common;

namespace Arabia.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DataTypeAttribute), typeof(DataTypeAttributeAdapter));
            var existingProvider = ModelValidatorProviders.Providers
        .Single(x => x is ClientDataTypeModelValidatorProvider);
            ModelValidatorProviders.Providers.Remove(existingProvider);
            ModelValidatorProviders.Providers.Add(new ClientNumberValidatorProvider()); 

            Bootstrapper.Initialise();
        }
    }
}