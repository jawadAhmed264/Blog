using Blog.Attribute;
using Blog.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ImageController : Controller
    {
        [ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult Thumbnail(string filename)
        {
            //var partialName = new Uri(filename).PathAndQuery;
            string bannerPath = ConfigurationManager.AppSettings["blogBannerPath"].ToString();
            using (Image image = Image.FromFile(Server.MapPath("../"+bannerPath+filename)))
            {
                return new ImageResult(image.BestFit(720,1280));
            }
        }
    }
}