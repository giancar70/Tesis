using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace tesis
{


    public class MvcApplication : System.Web.HttpApplication
    {
        private static ILog log = LogManager.GetLogger(typeof(MvcApplication));
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
           AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            log4net.Config.XmlConfigurator.Configure();
            log.Debug("--- Iniciando el Sitio Web Stardust -----");

            log.Debug("Cargando los mapeos dentro del sistema");

        }
    }
}