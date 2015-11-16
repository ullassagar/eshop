using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLayer;
using RepositoryLayer;

namespace WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            #region Autofac

            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof (MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterType<AdminRepository>().As<IAdminRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<OrdersRepository>().As<IOrdersRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.Register(c => new ProductHandler(c.Resolve<IProductRepository>()));
            builder.Register(c => new MemberHandler(c.Resolve<IMemberRepository>()));
            builder.Register(c => new CartHandler(c.Resolve<ICartRepository>(), c.Resolve<ProductHandler>()));
            builder.Register(c => new OrderHandler(c.Resolve<IOrdersRepository>()));
            builder.Register(c => new AdminHandler(c.Resolve<IAdminRepository>()));

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #endregion

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}