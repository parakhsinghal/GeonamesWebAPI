using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using GeoDataAPI.Domain.Interfaces;
using GeoDataAPI.SQLRepository;

namespace GeoDataAPI.Service
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<ILanguageCodeRepository, LanguageCodeSQLRepository>();
            container.RegisterType<IContinentRepository, ContinentSQLRepository>();
            container.RegisterType<ITimeZoneRepository, TimeZoneSQLRepository>();
            container.RegisterType<IFeatureCategoryRepository, FeatureCategorySQLRepository>();
            container.RegisterType<IFeatureCode, FeatureCodeSQLRepository>();
            container.RegisterType<ICountryRepository,CountrySQLRepository>();
            container.RegisterType<IRawPostalRepository, RawPostalSQLRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}