using System.Web.Http;
using UrlShortener.Data.EntityFramework.Initialization;
using UrlShortener.WebApi.App_Start;

namespace UrlShortener.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configure(IocConfig.Register);

            UrlShortenerDbMigrationsConfiguration.Initialize();
        }
    }
}
