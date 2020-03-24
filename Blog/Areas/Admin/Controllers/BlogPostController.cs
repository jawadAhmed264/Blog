using Blog.Common;
using Blog.Service.AuthorService;
using Blog.Service.BlogContentService;
using Blog.Service.BlogServices;
using Blog.Service.CategoryServices;
using Blog.Service.MediaFileService;
using Blog.Service.TagService;
using HtmlAgilityPack;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ViewModel.AdminViewModels.BlogPostViewModels;
using ViewModel.AuthorViewModels;
using ViewModel.Enums;
using ViewModel.MediaFileViewModels;
using ViewModel.TagViewModels;

namespace Blog.Areas.Admin.Controllers
{
    [RoutePrefix("BlogPost")]
    [Authorize(Roles = "Admin,Author")]
    public class BlogPostController : Controller
    {
        private ICategoryService catService;
        private IBlogService blogService;
        private ITagService tagService;
        private IMediaFileService mediaFileService;
        private IBlogContentService blogContentService;
        private IAuthorService authorService;

        public BlogPostController(ICategoryService _catService,
            IBlogService _blogService,
            ITagService _tagService,
            IMediaFileService _mediaFileService,
            IBlogContentService _blogContentService,
            IAuthorService _authorService)
        {
            catService = _catService;
            blogService = _blogService;
            tagService = _tagService;
            mediaFileService = _mediaFileService;
            blogContentService = _blogContentService;
            authorService = _authorService;

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
                    List<string> imageSrc = ImageHandling.getImageSourceList(model.Content);
                    List<string> imageName = ImageHandling.getImageNames(imageSrc);
                    List<MediaFileViewModel> mediafiles = InsertMediaFilesInModel(imageName, "", null);
                    List<TagViewModel> tags = InsertTagsinModel(model.Tags, null);
                    string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
                    string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
                    string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);

                    string Error, bannerUrl;

                    //Save BannerImage
                    FileHandler.SaveImage(model.BannerImage, 4, 700, 1440, bannerPath, out Error, out bannerUrl);

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
                    if (User.IsInRole("Author"))
                    {
                        AuthorViewModel author = authorService.GetAuthorByIdentityId(User.Identity.GetUserId());
                        model.CreateBy = author.Name;
                        model.AutherId = author.Id;
                        model.CreateDate = DateTime.Now;
                    }
                    if (User.IsInRole("Admin"))
                    {
                        model.CreateBy = "Admin";
                        model.CreateDate = DateTime.Now;
                    }
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
                    img.SetAttributeValue("src", "/Content/Images/blogImages/" + mediafiles[count].FileName);
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
                    List<string> imageSrc = ImageHandling.getImageSourceList(model.Content);
                    List<string> imageName = ImageHandling.getImageNames(imageSrc);
                    List<MediaFileViewModel> mediafiles = InsertMediaFilesInModel(imageName, "", BlogId);
                    List<TagViewModel> tags = InsertTagsinModel(model.Tags, BlogId);
                    string blogPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogImagesPath"]);
                    string bannerPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["blogBannerPath"]);
                    string thumbnailPath = Server.MapPath(@"/" + ConfigurationManager.AppSettings["thumbnailsPath"]);


                    string Error, bannerUrl;

                    //Save BannerImage
                    if (model.BannerImage != null)
                    {
                        FileHandler.SaveImage(model.BannerImage, 4, 700, 1440, bannerPath, out Error, out bannerUrl);

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

                    if (User.IsInRole("Author"))
                    {
                        AuthorViewModel author = authorService.GetAuthorByIdentityId(User.Identity.GetUserId());
                        model.ModifyBy = author.Name;
                        model.AutherId = author.Id;
                        model.ModifyDate = DateTime.Now;
                    }
                    if (User.IsInRole("Admin"))
                    {
                        model.ModifyBy = "Admin";
                        model.ModifyDate = DateTime.Now;
                    }

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
        [HttpPost]
        public ActionResult Publish(long Id)
        {
            int res = blogService.Publish(Id);
            IEnumerable<IndexBlogViewModel> model = blogService.getAllBlogs().ToList();
            return PartialView("_partialBlogList", model);
        }
        //Upload for TinyMCE
        public ActionResult Upload()
        {
            var file = Request.Files["file"];

            Image img = Image.FromStream(file.InputStream, true, true);
            int width = img.Width;
            int height = img.Height;

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

                if (file.ContentLength > (4 * megabyte))
                {
                    throw new InvalidOperationException("File size limit exceeded.");
                }

                if (width <= 1200 && height <= 675)
                {
                    throw new InvalidOperationException("Image size must be equal or less then (1200*675)");
                }

                string path = ConfigurationManager.AppSettings["blogImagesPath"].ToString();
                string savePath = Server.MapPath(@"/" + path + fileid);
                file.SaveAs(savePath);

                draft = new { location = Path.Combine("/" + path, fileid).Replace('\\', '/') };
            }


            return Json(draft, JsonRequestBehavior.AllowGet);
        }

        //------------------------------------------------Private Methods-----------------------------------------------------------------

        //Method for populating Tag List
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

        //Method for populating Media Files List
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

        //Method for Deleting all media files related to specific Blog By blogId (Used in Delete Action)
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
    }
}
