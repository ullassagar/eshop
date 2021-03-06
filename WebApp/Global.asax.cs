﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using BusinessLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

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

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<AdminRepository>().As<IAdminRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<OrdersRepository>().As<IOrdersRepository>();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>();
            builder.RegisterType<OrderOrderStatusRepository>().As<IOrderOrderStatusRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();

            builder.Register(c => new AdminHandler(c.Resolve<IAdminRepository>(), c.Resolve<IUnitOfWork>()));
            builder.Register(c => new MemberHandler(c.Resolve<IMemberRepository>(), c.Resolve<IUnitOfWork>()));
            builder.Register(c => new ProductHandler(c.Resolve<IProductRepository>(), c.Resolve<IUnitOfWork>()));
            builder.Register(c => new CategoryHandler(c.Resolve<ICategoryRepository>(), c.Resolve<IUnitOfWork>()));
            builder.Register(c => new CartHandler(c.Resolve<ICartRepository>(), c.Resolve<ProductHandler>()));
            builder.Register(c => new OrderHandler(c.Resolve<IOrdersRepository>(), c.Resolve<IOrderItemRepository>(), c.Resolve<IOrderOrderStatusRepository>(), c.Resolve<IUnitOfWork>()));

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