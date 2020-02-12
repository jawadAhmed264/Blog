using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        [Route("Blog/{id}/{title}")]
        public ActionResult Index(long Id,string title)
        {
            return View();
        }
    }
}