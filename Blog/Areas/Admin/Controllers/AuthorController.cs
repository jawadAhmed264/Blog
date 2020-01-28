using Blog.Service.AuthorService;
using Blog.Service.BlogContentService;
using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using Blog.Service.MediaFileService;
using Blog.Service.TagService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.AuthorViewModels;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("Author")]
    public class AuthorController : Controller
    {
        private IAuthorService authorServices;
        
        public AuthorController(IAuthorService _authorServices)
        {
            authorServices = _authorServices;
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            List<AuthorViewModel> model = authorServices.GetAllAuthors().ToList();
            return View(model);
            
        }
        [HttpGet]
        public ActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAuthor(AuthorViewModel model)
        {
            int res = authorServices.AddAuthor(model);
            if (res == 1)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditAuthor(int id) {
            AuthorViewModel toEdit = authorServices.GetAuthorById(id);
            return View(toEdit);
        }

        [HttpPost]
        public ActionResult EditAuthor(AuthorViewModel model)
        {
            int res = authorServices.EditAuthor(model,model.Id);
            if (res == 1)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteAuthor(int id)
        {
            AuthorViewModel model = authorServices.GetAuthorById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteAuthor(AuthorViewModel model)
        {
            var id = model.Id;
            int res = authorServices.DeleteAuthor(id);
            if (res == 1)
            {
                return RedirectToAction("Index", "Author");
            }
            return RedirectToAction("Index", "Author");
        }

    }
}