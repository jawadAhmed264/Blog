using Blog.Service.CategoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.CategoryViewModels;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private CategoryService categoryService;
        public HomeController(CategoryService _categoryService) {
            categoryService = _categoryService;
        }
        [Route("")]
        public ActionResult Index()
        {
            List<CategoryViewModel> model = categoryService.getAll().ToList();
            return View(model);
        }

        [HttpGet]
        public JsonResult Category(int id)
        {
            CategoryViewModel model = categoryService.getCategoryById(id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        [Route("BlogsCategory/{id}/{category}")]
        public ActionResult BlogsCategory(int id,string category) 
        {
            return View();
        }
    }
}