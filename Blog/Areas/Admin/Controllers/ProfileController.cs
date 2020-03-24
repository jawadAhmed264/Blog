using Blog.Service.AuthorService;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using ViewModel.AuthorViewModels;

namespace Blog.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        private IAuthorService authorService;

        public ProfileController(IAuthorService _authorService) {
            authorService = _authorService;
        }
        [Authorize(Roles = "Author")]
        [HttpGet]
        public ActionResult Author()
        {
            AuthorViewModel model = authorService.GetAuthorByIdentityId(User.Identity.GetUserId());
            return View(model);
        }
        [Authorize(Roles = "Author")]
        [HttpPost]
        public ActionResult Author(AuthorViewModel model)
        {
            if (ModelState.IsValid) { 
               
            }
            return View(model);
        }

    }
}