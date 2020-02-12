using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize(Roles = "Author")]
        public ActionResult Author()
        {
            return View();
        }
    }
}