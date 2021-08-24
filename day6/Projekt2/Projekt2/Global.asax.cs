using Autofac;
using Autofac.Integration.WebApi;
using Project.Model;
using Project.Model.Common;
using Project.Repository;
using Project.Service;
using Projekt2.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Projekt2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterModule<DIServiceModule>();
            builder.RegisterModule<DIRepositoryModule>();
            
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            
        }
    }
}
