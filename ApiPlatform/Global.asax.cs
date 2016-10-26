using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using System.Reflection;
using ApiPlatform.App_Start;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using ApiPlatform.Controllers;

namespace ApiPlatform
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            #region Autofac

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterWebApiFilterProvider(config);

            Assembly repositoryAss = Assembly.Load("Cache");
            Type[] rtypes = repositoryAss.GetTypes();
            builder.RegisterTypes(rtypes)
                .AsImplementedInterfaces();

            Assembly servicesAss = Assembly.Load("Service");
            Type[] stypes = servicesAss.GetTypes();
            builder.RegisterTypes(stypes)
                .AsImplementedInterfaces();

            builder.RegisterWebApiFilterProvider(config);
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            #endregion
        }
    }
}
