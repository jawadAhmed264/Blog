using Blog.Service.CategoryServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.BlogPostViewModels;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("BlogPost")]
    public class BlogPostController : Controller
    {
        private ICategoryService catService;
        public BlogPostController(ICategoryService _catService) 
        {
            catService = _catService;  
        }

        // GET: Admin/BlogPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddBlog() {
            AddBlogViewModel model = new AddBlogViewModel();
            model.CategoryList = catService.getAll();
            return View(model);
        }
        
        [HttpPost]
        public ActionResult AddBlog(AddBlogViewModel model)
        {
            return View(model);
        }

        
        //Upload for TinyMCE
        [Route("{controller}/{action}/{name}")]
        public ActionResult Upload(string name)
        {
            var file = Request.Files["file"];

            string extension = Path.GetExtension(file.FileName);
            string fileid = name+"_"+Guid.NewGuid().ToString();
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
                string path = ConfigurationManager.AppSettings["tempImagesPath"];
                string savePath = Server.MapPath(@"/"+path+fileid);
                file.SaveAs(savePath);

                draft = new { location = Path.Combine("/"+path, fileid).Replace('\\', '/') };
            }


            return Json(draft, JsonRequestBehavior.AllowGet);
        }
    }
}