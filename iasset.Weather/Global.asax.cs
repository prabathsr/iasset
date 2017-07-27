using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using iasset.Weather.Domain.Contracts;
using iasset.Weather.Infrastructure.Remote.OpenWeather;
using iasset.Weather.Infrastructure.Services;
using iasset.Weather.Infrastructure.Remote.WebServiceX;
using iasset.Weather.Logger;

namespace iasset.Weather
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterAutofacIoc();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        ///     Registering Autofac IOC so we can use it in the application
        /// </summary>
        private void RegisterAutofacIoc()
        {
            // Register Autofac first
            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(assembly);
            builder.RegisterApiControllers(assembly);
            builder.RegisterSource(new ViewRegistrationSource());

            // Inject types needs to be used with IOC
            InjectTypes(builder);

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private void InjectTypes(ContainerBuilder builder)
        {
            // Inject types
            // Register our custom logger
            builder.RegisterGeneric(typeof(ApiLogger<>)).As(typeof(IApiLogger<>));
            // Register other types
            builder.RegisterType<WebServiceXClient>();
            builder.RegisterType<CountryWebserviceXClient>().As<ICountryRemoteApiClient>();
            builder.RegisterType<WeatherWebserviceXClient>().As<IWeatherRemoteApiClient>();
            builder.RegisterType<OpenWeatherApiClient>().As<IWeatherRemoteApiClient>().PreserveExistingDefaults();
            builder.RegisterType<CountryService>().As<ICountryService>();
            builder.RegisterType<WeatherService>().As<IWeatherService>();
        }
    }
}