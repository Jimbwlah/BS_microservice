﻿namespace BS_Microservice
{
    using System.Web.Http;
    using BS_Api.Services;
    using Microsoft.Practices.Unity;
    using Unity;

    /// <summary>
    /// Primary configuration class for Web API
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Web API application registration for parameters
        /// </summary>
        /// <param name="config">The <see cref="HttpConfiguration"/>.</param>
        public static void Register(HttpConfiguration config)
        {
            // DI
            var container = new UnityContainer();
            container.RegisterType<ILoggingService, RestLoggingService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Attribute routing
            config.MapHttpAttributeRoutes();

            // Convention-based routing
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}