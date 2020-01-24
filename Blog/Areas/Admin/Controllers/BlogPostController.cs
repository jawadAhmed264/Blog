using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.BlogPostViewModels;
using ViewModel.Enums;
using ViewModel.MediaFileViewModels;
using ViewModel.TagViewModels;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("BlogPost")]
    public class BlogPostController : Controller
    {
        private ICategoryService catService;
        private IBlogService blogService;
        public BlogPostController(ICategoryService _catService, IBlogService _blogService)
        {
            catService = _catService;
            blogService = _blogService;
        }

        // GET: Admin/BlogPost
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddBlog()
        {
             AddBlogViewModel model = new AddBlogViewModel();
             model.CategoryList = catService.getAll();
             return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBlog(AddBlogViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> imageSrc = getImageSourceList(model.Content);
                    List<string> imageName = getImageNames(imageSrc);
                    List<MediaFileViewModel> mediafiles = InsertMediaFilesInModel(imageName, "");
                    List<TagViewModel> tags = InsertTagsinModel(model.Tags);

                    string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
                    string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
                    string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);

                    //Save BannerImage
                    string bannerUrl = SaveBannerImage(model.BannerImage, bannerPath, model.Title);

                    mediafiles.Add(new MediaFileViewModel()
                    {
                        MediaType = MediaTypeEnum.Banner.ToString(),
                        Url = bannerUrl,
                        FileName = bannerUrl,
                        Description = ""
                    });

                    //Populate Model
                    model.MediaFiles = mediafiles;
                    model.TagList = tags;
                    model.Active = false;
                    model.CreateBy = "Admin";
                    model.CreateDate = DateTime.Now;

                    //Call blog Service
                    int res = blogService.AddBlog(model);

                    if (res > 0) {
                        ModelState.Clear();
                        return RedirectToAction("Index");
                    }
                    model.CategoryList = catService.getAll();
                    return View(model);
                }
                model.CategoryList = catService.getAll();
                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        private string SaveBannerImage(HttpPostedFileBase bannerImage,string bannerPath,string blogTitle)
        {
            HttpPostedFileBase file = bannerImage;
            string extension = Path.GetExtension(file.FileName);
            string fileid =  Guid.NewGuid().ToString();
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
                
                string savePath = bannerPath+fileid;
                file.SaveAs(savePath);

            }
            return fileid;
        }

        private List<TagViewModel> InsertTagsinModel(string tags)
        {
            List<TagViewModel> list = new List<TagViewModel>();
            list.Clear();
            string[] tagsArray = tags.Split(',');
            foreach (var tag in tagsArray)
            {
                TagViewModel tvm = new TagViewModel()
                {
                    TagName = tag,
                    Active = true,
                };
                list.Add(tvm);
            }
            return list;
        }

        private List<MediaFileViewModel> InsertMediaFilesInModel(List<string> imageName, string Description)
        {
            List<MediaFileViewModel> list = new List<MediaFileViewModel>();
            list.Clear();
            foreach (var img in imageName)
            {
                //string input = img;
                //string pattern = @"\temp\b";
                //string replace = title;
                //string result = Regex.Replace(input, pattern, replace, RegexOptions.IgnoreCase);

                MediaFileViewModel mf = new MediaFileViewModel()
                {
                    MediaType = MediaTypeEnum.BlogImage.ToString(),
                    Url = img,
                    FileName = img,
                    Description = Description
                };
                list.Add(mf);
            }
            return list;
        }

        private List<string> getImageNames(List<string> imageSrc)
        {
            List<string> ImageNames = new List<string>();
            ImageNames.Clear();
            foreach (string name in imageSrc)
            {
                string[] splitArray = name.Split('/');
                string ImageName = splitArray.Last();
                ImageNames.Add(ImageName);
            }
            return ImageNames;
        }

        private List<string> getImageSourceList(string html)
        {

            List<string> srcList = new List<string>();
            srcList.Clear();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(@"" + html);
            foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//img[@src]"))
            {
                string src = link.GetAttributeValue("src", "").ToString();
                srcList.Add(src);
            }
            return srcList;
        }

        //Upload for TinyMCE
        public ActionResult Upload()
        {
            var file = Request.Files["file"];

            string extension = Path.GetExtension(file.FileName);
            string fileid =  Guid.NewGuid().ToString();
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
                string path = ConfigurationManager.AppSettings["blogImagesPath"];
                string savePath = Server.MapPath(@"/" + path + fileid);
                file.SaveAs(savePath);

                draft = new { location = Path.Combine("/" + path, fileid).Replace('\\', '/') };
            }


            return Json(draft, JsonRequestBehavior.AllowGet);
        }

        //ImageThumbnail
        private void CreateThumbnail(int ThumbnailMax, string OriginalImagePath, string ThumbnailImagePath)
        {
            // Loads original image from file
            Image imgOriginal = Image.FromFile(OriginalImagePath);
            // Finds height and width of original image
            float OriginalHeight = imgOriginal.Height;
            float OriginalWidth = imgOriginal.Width;
            // Finds height and width of resized image
            int ThumbnailWidth;
            int ThumbnailHeight;
            if (OriginalHeight > OriginalWidth)
            {
                ThumbnailHeight = ThumbnailMax;
                ThumbnailWidth = (int)((OriginalWidth / OriginalHeight) * (float)ThumbnailMax);
            }
            else
            {
                ThumbnailWidth = ThumbnailMax;
                ThumbnailHeight = (int)((OriginalHeight / OriginalWidth) * (float)ThumbnailMax);
            }
            // Create new bitmap that will be used for thumbnail
            Bitmap ThumbnailBitmap = new Bitmap(ThumbnailWidth, ThumbnailHeight);
            Graphics ResizedImage = Graphics.FromImage(ThumbnailBitmap);
            // Resized image will have best possible quality
            ResizedImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
            ResizedImage.CompositingQuality = CompositingQuality.HighQuality;
            ResizedImage.SmoothingMode = SmoothingMode.HighQuality;
            // Draw resized image
            ResizedImage.DrawImage(imgOriginal, 0, 0, ThumbnailWidth, ThumbnailHeight);
            // Save thumbnail to file
            ThumbnailBitmap.Save(ThumbnailImagePath);
        }
    }
}
