using Blog.Attribute;
using Blog.Common;
using System.Configuration;
using System.Drawing;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class ImageController : Controller
    {
        [ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult Thumbnail(string filename,int height,int width)
        {
            //var partialName = new Uri(filename).PathAndQuery;
            string path = ConfigurationManager.AppSettings["thumbnailsPath"].ToString();
            using (Image image = Image.FromFile(Server.MapPath("../"+path+filename)))
            {
                return new ImageResult(image.BestFit(height,width));
            }
        }
        [ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult Banner(string filename, int height, int width)
        {
            //var partialName = new Uri(filename).PathAndQuery;
            string path = ConfigurationManager.AppSettings["blogBannerPath"].ToString();
            using (Image image = Image.FromFile(Server.MapPath("../" + path + filename)))
            {
                return new ImageResult(image.BestFit(height, width));
            }
        }
        [ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult AuthorImage(string filename, int height, int width)
        {
            //var partialName = new Uri(filename).PathAndQuery;
            string path = ConfigurationManager.AppSettings["AurthorPath"].ToString();
            using (Image image = Image.FromFile(Server.MapPath("../" + path + filename)))
            {
                return new ImageResult(image.BestFit(height, width));
            }
        }
        [ETag, OutputCache(Duration = 3600, VaryByParam = "filename")]
        public ActionResult UserImage(string filename, int height, int width)
        {
            //var partialName = new Uri(filename).PathAndQuery;
            string path = ConfigurationManager.AppSettings["UserPath"].ToString();
            using (Image image = Image.FromFile(Server.MapPath("../" + path + filename)))
            {
                return new ImageResult(image.BestFit(height, width));
            }
        }
    }
}