using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.BlogPostViewModels;

namespace Blog.Areas.Admin.Controllers
{
    public class BlogPostController : Controller
    {
        // GET: Admin/BlogPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddBlog() {
            return View();
        }
        
        [HttpPost]
        public ActionResult AddBlog(AddBlogViewModel model)
        {
            return View(model);
        }
    }
}