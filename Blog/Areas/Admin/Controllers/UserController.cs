using Blog.Models;
using Blog.Service.UserService;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.UserViewModel;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("User")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IUserService userservice;
        public UserController(IUserService _userservice)
        {
            userservice = _userservice;
        }
        [HttpGet]
        public ActionResult Index()
        {
            IList<UserViewModel> users = userservice.GetAllUsers();
            return View(users);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserViewModel model)
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
                            int res = userservice.AddUser(model);
                            if (res == 1)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                ViewBag.Error = true;
                                ViewBag.ErrorMessage = "Please Validate the form First.";
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
        public ActionResult EditUser(int id)
        {
            UserViewModel toEdit = userservice.GetUserById(id);
            return View(toEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int res = userservice.EditUser(model, model.Id);
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
            ViewBag.Error = true;
            ViewBag.ErrorMessage = "Please Validate the form first.";
            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            UserViewModel model = userservice.GetUserById(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult DeleteUser(UserViewModel model)
        {
                var id = model.Id;
                int res = userservice.DeleteUser(id);
                if (res == 1)
                {
                    return RedirectToAction("Index", "User");
                }
            return View(model);
        }
    }
}