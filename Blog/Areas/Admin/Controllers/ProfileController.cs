using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Areas.Admin.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Author()
        {
            return View();
        }
    }
}