using Blog.Service.BlogContentService;
using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using Blog.Service.MediaFileService;
using Blog.Service.TagService;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
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
        private ITagService tagService;
        private IMediaFileService mediaFileService;
        private IBlogContentService blogContentService;

        public BlogPostController(ICategoryService _catService,
            IBlogService _blogService,
            ITagService _tagService,
            IMediaFileService _mediaFileService,
            IBlogContentService _blogContentService)
        {
            catService = _catService;
            blogService = _blogService;
            tagService = _tagService;
            mediaFileService = _mediaFileService;
            blogContentService = _blogContentService;

        }

        // GET: Admin/BlogPost
        public ActionResult Index()
        {
            IEnumerable<IndexBlogViewModel> model = blogService.getAllBlogs().ToList();
            if (Convert.ToBoolean(TempData["Error"]) == true)
            {
                ViewBag.Error = true;
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult UpdateBlogList()
        {
            IEnumerable<IndexBlogViewModel> model = blogService.getAllBlogs().ToList();
            return PartialView("_partialBlogList", model);
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
                    List<MediaFileViewModel> mediafiles = InsertMediaFilesInModel(imageName, "", null);
                    List<TagViewModel> tags = InsertTagsinModel(model.Tags, null);
                    string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
                    string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
                    string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);

                    string Error, bannerUrl;

                    //Save BannerImage
                    SaveBannerImage(model.BannerImage, bannerPath, model.Title, out Error, out bannerUrl);

                    if (Error != null)
                    {
                        model.CategoryList = catService.getAll();
                        ModelState.AddModelError("", Error);
                        ViewBag.Error = true;
                        ViewBag.ErrorMessage = "Invalid Form Validation";
                        return View(model);
                    }
                    if (bannerUrl == null)
                    {
                        mediafiles.Add(new MediaFileViewModel()
                        {
                            MediaType = MediaTypeEnum.Banner.ToString(),
                            Description = ""
                        });

                    }
                    else
                    {
                        mediafiles.Add(new MediaFileViewModel()
                        {
                            MediaType = MediaTypeEnum.Banner.ToString(),
                            Url = bannerUrl,
                            FileName = bannerUrl,
                            Description = ""
                        });
                    }
                    //Populate Model
                    model.MediaFiles = mediafiles;
                    model.TagList = tags;
                    if (model.btnSubmit == "Publish")
                    {
                        model.Active = true;
                    }
                    if (model.btnSubmit == "Save")
                    {
                        model.Active = false;
                    }
                    model.CreateBy = "Admin";
                    model.CreateDate = DateTime.Now;

                    //Call blog Service
                    int res = blogService.AddBlog(model);

                    if (res > 0)
                    {
                        ModelState.Clear();
                        TempData["Error"] = true;
                        TempData["ErrorMessage"] = "New blog created successfully";
                        return RedirectToAction("Index");
                    }

                }
                model.CategoryList = catService.getAll();
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Invalid Form Validation";
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult EditBlog(long Id)
        {
            if (Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Route");
            }

            AddBlogViewModel model = blogService.getBlogById(Id);

            if (model == null)
            {
                return HttpNotFound();
            }

            IList<MediaFileViewModel> mediafiles = mediaFileService.GetAllMediaFilesByBlogId(model.BlogPostId);
            IList<TagViewModel> tags = tagService.GetAllTagsByBlogId(model.BlogPostId);
            string html = blogContentService.getBlogContentByBlogId(model.BlogPostId).Content;
            int count = 0;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var ImageTags = doc.DocumentNode.SelectNodes("//img[@src]");
            if (ImageTags != null)
            {
                foreach (HtmlNode img in ImageTags)
                {
                    img.SetAttributeValue("src", "/Content/Images/blogImages/"+ mediafiles[count].FileName);
                    count++;
                }
                count = 0;
            }
            var newHtml = doc.DocumentNode.WriteTo();
            model.Content = newHtml;
            model.BannerUrl = mediafiles.FirstOrDefault(m => m.MediaType == MediaTypeEnum.Banner.ToString()).Url;
            model.Tags = String.Join(",", tags.Select(m => m.TagName));
            model.CategoryList = catService.getAll();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBlog(AddBlogViewModel model, long BlogId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<string> imageSrc = getImageSourceList(model.Content);
                    List<string> imageName = getImageNames(imageSrc);
                    List<MediaFileViewModel> mediafiles = InsertMediaFilesInModel(imageName, "", BlogId);
                    List<TagViewModel> tags = InsertTagsinModel(model.Tags, BlogId);
                    string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
                    string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
                    string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);


                    string Error, bannerUrl;

                    //Save BannerImage
                    if (model.BannerImage != null)
                    {
                        SaveBannerImage(model.BannerImage, bannerPath, model.Title, out Error, out bannerUrl);

                        if (Error != null)
                        {
                            model.CategoryList = catService.getAll();
                            ModelState.AddModelError("", Error);
                            ViewBag.Error = true;
                            ViewBag.ErrorMessage = "Invalid Form Validation";
                            return View(model);
                        }
                        if (bannerUrl == null)
                        {
                            mediafiles.Add(new MediaFileViewModel()
                            {
                                MediaType = MediaTypeEnum.Banner.ToString(),
                                Description = ""
                            });

                        }
                        else
                        {
                            mediafiles.Add(new MediaFileViewModel()
                            {
                                MediaType = MediaTypeEnum.Banner.ToString(),
                                Url = bannerUrl,
                                FileName = bannerUrl,
                                Description = ""
                            });
                        }
                    }
                    else
                    {
                        mediafiles.Add(new MediaFileViewModel()
                        {
                            MediaType = MediaTypeEnum.Banner.ToString(),
                            Url = model.BannerUrl,
                            FileName = model.BannerUrl,
                            Description = ""
                        });

                    }
                    //Populate Model
                    model.MediaFiles = mediafiles;
                    model.TagList = tags;

                    if (model.btnSubmit == "Publish")
                    {
                        model.Active = true;
                    }

                    if (model.btnSubmit == "Save")
                    {
                        model.Active = false;
                    }

                    model.ModifyBy = "Admin";
                    model.ModifyDate = DateTime.Now;

                    //Call blog Service
                    int res = blogService.EditBlog(model, BlogId);

                    if (res > 0)
                    {
                        ModelState.Clear();
                        TempData["Error"] = true;
                        TempData["ErrorMessage"] = "Blog updated successfully";
                        return RedirectToAction("Index");
                    }

                }
                ViewBag.Error = true;
                ViewBag.ErrorMessage = "Invalid Form Validation";
                model.CategoryList = catService.getAll();
                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Delete(long Id)
        {
            int res = blogService.DeleteBlog(Id);
            if (res > 0)
            {
                DeleteBlogImages(Id);
            }
            else
            {
                throw new InvalidOperationException("Error while deleting blog");
            }
            IEnumerable<IndexBlogViewModel> model = blogService.getAllBlogs().ToList();
            return PartialView("_partialBlogList", model);
        }
        private void DeleteBlogImages(long blogId)
        {
            string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
            string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
            string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);

            IList<MediaFileViewModel> files = mediaFileService.GetAllMediaFilesByBlogId(blogId).ToList();
            if (files.Count > 0)
            {
                if (files.Any(m => m.MediaType == MediaTypeEnum.Banner.ToString()))
                {
                    string bannerName = files.FirstOrDefault(m => m.MediaType == MediaTypeEnum.Banner.ToString()).FileName;
                    if (System.IO.File.Exists(bannerPath + bannerName)) { System.IO.File.Delete(bannerPath + bannerName); }
                }
                foreach (var file in files)
                {
                    if (file.MediaType == MediaTypeEnum.BlogImage.ToString())
                    {
                        if (System.IO.File.Exists(blogPath + file.FileName)) { System.IO.File.Delete(blogPath + file.FileName); }
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult Publish(long Id)
        {
            int res = blogService.Publish(Id);
            IEnumerable<IndexBlogViewModel> model = blogService.getAllBlogs().ToList();
            return PartialView("_partialBlogList", model);
        }
        private void SaveBannerImage(HttpPostedFileBase bannerImage, string bannerPath, string blogTitle, out string error, out string url)
        {
            if (bannerImage != null)
            {
                HttpPostedFileBase file = bannerImage;
                string extension = Path.GetExtension(file.FileName);
                string fileid = Guid.NewGuid().ToString();
                fileid = Path.ChangeExtension(fileid, extension);

                Image img = Image.FromStream(bannerImage.InputStream, true, true);
                int width = img.Width;
                int height = img.Height;

                var draft = new { location = "" };

                if (file != null && file.ContentLength > 0)
                {
                    const int megabyte = 1024 * 1024;

                    if (!file.ContentType.StartsWith("image/"))
                    {
                        error = "Invalid MIME content type";
                        url = null;
                        return;
                    }

                    string[] extensions = { ".gif", ".jpg", ".png" };
                    if (!extensions.Contains(extension))
                    {
                        error = "Invalid file extension";
                        url = null;
                        return;
                    }

                    if (file.ContentLength > (8 * megabyte))
                    {
                        error = "File size limit exceeded";
                        url = null;
                        return;
                    }

                    if (width != 1440 && height != 700)
                    {
                        error = "Background dimension must be 1440*700";
                        url = null;
                        return;
                    }

                    string savePath = bannerPath + fileid;
                    file.SaveAs(savePath);
                    error = null;
                    url = fileid;
                    return;
                }
                error = null;
                url = null;
                return;
            }
            error = null;
            url = null;
            return;
        }
        private List<TagViewModel> InsertTagsinModel(string tags, long? BlogId)
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
                if (BlogId != null)
                {
                    tvm.BlogPostId = BlogId;
                }
                list.Add(tvm);
            }
            return list;
        }
        private List<MediaFileViewModel> InsertMediaFilesInModel(List<string> imageName, string Description, long? BlogId)
        {
            List<MediaFileViewModel> list = new List<MediaFileViewModel>();
            list.Clear();
            if (imageName != null)
            {
                foreach (var img in imageName)
                {

                    MediaFileViewModel mf = new MediaFileViewModel()
                    {
                        MediaType = MediaTypeEnum.BlogImage.ToString(),
                        Url = img,
                        FileName = img,
                        Description = Description,
                    };
                    if (BlogId != null)
                    {
                        mf.BlogPostId = BlogId;
                    }
                    list.Add(mf);
                }
                return list;
            }
            return list;
        }
        private List<string> getImageNames(List<string> imageSrc)
        {
            List<string> ImageNames = new List<string>();
            ImageNames.Clear();
            if (imageSrc != null)
            {
                ImageNames.Clear();
                foreach (string name in imageSrc)
                {
                    string[] splitArray = name.Split('/');
                    string ImageName = splitArray.Last();
                    ImageNames.Add(ImageName);
                }
                return ImageNames;
            }
            return ImageNames;
        }
        private List<string> getImageSourceList(string html)
        {

            List<string> srcList = new List<string>();
            srcList.Clear();
            srcList.Clear();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(@"" + html);
            var ImageTags = doc.DocumentNode.SelectNodes("//img[@src]");
            if (ImageTags != null)
            {
                foreach (HtmlNode link in ImageTags)
                {
                    string src = link.GetAttributeValue("src", "").ToString();
                    srcList.Add(src);
                }
            }
            return srcList;
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

                string path = ConfigurationManager.AppSettings["blogImagesPath"].ToString();
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
