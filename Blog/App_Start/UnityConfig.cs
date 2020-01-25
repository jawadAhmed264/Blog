using Blog.Controllers;
using Blog.Service.AuthorService;
using Blog.Service.BlogContentService;
using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using Blog.Service.MediaFileService;
using Blog.Service.TagService;
using Blog.Service.UserService;
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
            container.RegisterType<IAuthorService, AuthorService>();
            container.RegisterType<IBlogContentService, BlogContentService>();
            container.RegisterType<IMediaFileService, MediaFileService>();
            container.RegisterType<ITagService, TagService>();
            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}