using Blog.Models;
using Blog.Service.AuthorService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ViewModel.AuthorViewModels;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("Author")]
    [Authorize(Roles = "Admin")]
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
        [ValidateAntiForgeryToken]
        public ActionResult AddAuthor(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext Db = new ApplicationDbContext())
                {
                    //var roleStore = new RoleStore<IdentityRole>(Db);
                    //var roleMngr = new RoleManager<IdentityRole>(roleStore);
                    var userStore = new UserStore<ApplicationUser>(Db);
                    var userMngr = new UserManager<ApplicationUser>(userStore);
                    if (userMngr.FindByEmail(model.Email) == null)
                    {
                        ApplicationUser user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                        var result = userMngr.Create(user, model.Password);
                        if (result.Succeeded)
                        {
                            userMngr.AddToRole(user.Id, "User");
                            model.AspnetUser = user.Id;

                            int res = authorServices.AddAuthor(model);
                            if (res == 1)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.Error = true;
                                ViewBag.ErrorMessage = "Please Validate the form first.";
                                return View(model);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = "User is already exist.";
                        return View(model);
                    }
                }
            }
            ViewBag.Error = true;
            ViewBag.ErrorMessage = "Please Validate the form first.";
            return View(model);
        }

        [HttpGet]
        public ActionResult EditAuthor(int id)
        {
            AuthorViewModel toEdit = authorServices.GetAuthorById(id);
            return View(toEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(AuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = authorServices.EditAuthor(model, model.Id);
                if (res == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = true;
                    ViewBag.ErrorMessage = "Please Valodate the form first.";
                    return View(model);
                }
            }
            ViewBag.Error = true;
            ViewBag.ErrorMessage = "Please Valodate the form first.";
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