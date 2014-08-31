using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace PowerMonitor.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);


            // Setup LightInject
            var container = new ServiceContainer();
            container.RegisterApiControllers();
            container.EnablePerWebRequestScope();
            container.EnableWebApi(GlobalConfiguration.Configuration);
            container.Register<IXDocumentLoader, XDocumentLoader>(); 
        }
    }
}
