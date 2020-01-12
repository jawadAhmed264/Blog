using System;
using System.Collections.Generic;
using System.IO;
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

        //Upload for TinyMCE
        public ActionResult Upload()
        {
            var file = Request.Files["file"];

            string extension = Path.GetExtension(file.FileName);
            string fileid = Guid.NewGuid().ToString();
            fileid = Path.ChangeExtension(fileid, extension);

            var draft = new { location = "" };

            if (file != null && file.ContentLength > 0)
            {
                const int megabyte = 1024 * 1024;

                if (!file.ContentType.StartsWith("image/"))
                {
                    throw new InvalidOperationException("Invalid MIME content type.");
                }

                string[] extensions = { ".gif", ".jpg", ".png" };
                if (!extensions.Contains(extension))
                {
                    throw new InvalidOperationException("Invalid file extension.");
                }

                if (file.ContentLength > (8 * megabyte))
                {
                    throw new InvalidOperationException("File size limit exceeded.");
                }

                string savePath = Server.MapPath(@"/Content/Images/tempImages/" + fileid);
                file.SaveAs(savePath);

                draft = new { location = Path.Combine("/Content/Images/tempImages/", fileid).Replace('\\', '/') };
            }


            return Json(draft, JsonRequestBehavior.AllowGet);
        }
    }
}