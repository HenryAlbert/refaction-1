using System.Web.Http;
using System.Web.Http.ModelBinding.Binders;
using refactor_me.ModelBinder;
using System.Web.Http.ModelBinding;
using refactor_me.CustomFilters;

namespace refactor_me
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var provider = new SimpleModelBinderProvider( typeof(Models.Product), new ProductModelBinder());
            config.Services.Insert(typeof(ModelBinderProvider), 0, provider);
            
            // Web API configuration and services
            var formatters = GlobalConfiguration.Configuration.Formatters;
            formatters.Remove(formatters.XmlFormatter);
            formatters.JsonFormatter.Indent = true;

            config.Filters.Add(new ValidateGuidAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


         

        }
    }
}
