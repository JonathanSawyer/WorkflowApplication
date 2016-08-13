using BL;
using Microsoft.Practices.Unity;
using NHibernate;
using RateIT.Example.DalMappings;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MvcWebApi.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
            ISessionFactory sessionFactory = FluentNHibernateHelper.CreateSessionFactory();

            var container = new UnityContainer();
            container.RegisterInstance(typeof(ISessionFactory), sessionFactory);
            container.RegisterType<IEntityService<User>, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IWorkflowService<User>, WorkflowService<User>>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

        }
    }
}
