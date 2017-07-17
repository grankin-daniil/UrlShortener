using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using UrlShortener.Core.Services;
using UrlShortener.Data.Contracts;
using UrlShortener.Data.EntityFramework.Repositories;
using UrlShortener.WebApi.Contracts.Services;

namespace UrlShortener.WebApi.App_Start
{
    public static class IocConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            container.RegisterType<IUrlShortenerService, UrlShortenerService>(new HierarchicalLifetimeManager());
            container.RegisterType<IShortenedUrlRepository, ShortenedUrlRepository>();
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}