using Blog.Controllers;
using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace Blog
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IBlogService, BlogService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}