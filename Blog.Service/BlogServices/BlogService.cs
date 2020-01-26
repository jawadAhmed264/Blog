using Blog.Data.Models;
using Blog.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.AdminViewModels.BlogPostViewModels;
using ViewModel.Enums;
using ViewModel.TagViewModels;

namespace Blog.Service.BlogServices
{
    public class BlogService : IBlogService
    {
        private BlogEntities DbContext;

        public BlogService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }

        public int AddBlog(AddBlogViewModel model)
        {
            using (DbContext)
            {
                try
                {
                    ICollection<MediaFile> mediaFiles = model.MediaFiles.Select(mf => new MediaFile
                    {
                        FileName = mf.FileName,
                        MediaTypeId = DbContext.MediaTypes.FirstOrDefault(mt => mt.TypeName == mf.MediaType).Id,
                        Url = mf.Url,
                        Active = model.Active,
                        Description = mf.Description,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate
                    }).ToList();

                    ICollection<Tag> Tags = model.TagList.Select(t => new Tag
                    {
                        Active = t.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        TagName = t.TagName,
                    }).ToList();

                    BlogPost blogPost = new BlogPost
                    {
                        MediaFiles = mediaFiles,
                        AutherId = model.AutherId,
                        CategoryId = model.CategoryId,
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        Summary = model.Summary,
                        Title = model.Title,
                        Tags = Tags
                    };

                    BlogContent blogContent = new BlogContent
                    {
                        BlogPost = blogPost,
                        Active = model.Active,
                        Content = model.Content,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                    };

                    DbContext.BlogPosts.Add(blogPost);
                    DbContext.BlogContents.Add(blogContent);

                    int res = DbContext.SaveChanges();
                    return res;

                }
                catch (Exception ex)
                {
                    string exString = "Message: " + ex.Message + " StackTrace: " + ex.StackTrace;
                    LogManagement.LogError(new LogViewModel { Exception = exString, Controller = "BlogService(ServiceLayer)", Action = "AddBlog", Active = true, CreatedDate = DateTime.Now });
                    throw ex;
                }
            }
        }

        public int DeleteBlog(long Id)
        {
            using (DbContext) {
                var blog = DbContext.BlogPosts.Where(m => m.Id == Id).Include(m => m.MediaFiles).Include(m => m.Tags).Include(m => m.Category).Include(m => m.BlogContents).SingleOrDefault();

                DbContext.MediaFiles.RemoveRange(blog.MediaFiles);
                DbContext.Tags.RemoveRange(blog.Tags);
                DbContext.BlogContents.RemoveRange(blog.BlogContents);
                DbContext.BlogPosts.Remove(blog);
                int res = DbContext.SaveChanges();
                return res;
            }
        }

        public int EditBlog(AddBlogViewModel model, long Id)
        {
            using (DbContext)
            {
                try
                {
                    var blog = DbContext.BlogPosts.Where(m => m.Id == Id).Include(m => m.MediaFiles).Include(m => m.Tags).Include(m => m.Category).Include(m => m.BlogContents).SingleOrDefault();

                    //Delete Existing Data
                    foreach (var mediafile in blog.MediaFiles.ToList()) {
                        if (model.MediaFiles.Any(m => m.BlogPostId == mediafile.BlogPostId)) {
                            DbContext.MediaFiles.Remove(mediafile);
                        }
                    }
                    foreach (var Tag in blog.Tags.ToList())
                    {
                        if (model.TagList.Any(m => m.BlogPostId == Tag.BlogPostId))
                        {
                            DbContext.Tags.Remove(Tag);
                        }
                    }

                    //Adding or Updating Fields
                    foreach (var mediafile in model.MediaFiles.ToList())
                    {
                        MediaFile mediaFile = new MediaFile()
                        {
                            FileName = mediafile.FileName,
                            MediaTypeId = DbContext.MediaTypes.FirstOrDefault(mt => mt.TypeName == mediafile.MediaType).Id,
                            Url = mediafile.Url,
                            Active = model.Active,
                            Description = mediafile.Description,
                            CreateBy = model.CreateBy,
                            CreateDate = model.CreateDate,
                            BlogPostId=model.BlogPostId
                        };
                        blog.MediaFiles.Add(mediaFile);
                    }
                    foreach (var t in model.TagList.ToList())
                    {
                        Tag tag = new Tag()
                        {
                            Active = t.Active,
                            CreateBy = model.CreateBy,
                            CreateDate = model.CreateDate,
                            TagName = t.TagName,
                            BlogPostId=model.BlogPostId
                        };
                        blog.Tags.Add(tag);
                    }
    
                    blog.CategoryId = model.CategoryId;
                    blog.Active = model.Active;
                    blog.ModifyBy = model.CreateBy;
                    blog.ModifyDate = model.CreateDate;
                    blog.Summary = model.Summary;
                    blog.Title = model.Title;

                    DbContext.Entry(blog).State = EntityState.Modified;

                    BlogContent blogContent = DbContext.BlogContents.FirstOrDefault(m => m.BlogPostId == Id);

                    blogContent.BlogPost = blog;
                    blogContent.Active = model.Active;
                    blogContent.Content = model.Content;
                    blogContent.CreateBy = model.CreateBy;
                    blogContent.CreateDate = model.CreateDate;
                   

                    DbContext.Entry(blogContent).State = EntityState.Modified;

                    int res = DbContext.SaveChanges();
                    return res;
                }
                catch (Exception ex)
                {
                    string exString = "Message: " + ex.Message + " StackTrace: " + ex.StackTrace;
                    LogManagement.LogError(new LogViewModel { Exception = exString, Controller = "BlogService(ServiceLayer)", Action = "EditBlog", Active = true, CreatedDate = DateTime.Now });
                    throw ex;
                }
            }
        }

        public IList<AddBlogViewModel> getAllBlogs()
        {
            try
            {
                using (DbContext)
                {
                    IList<BlogPost> list = DbContext.BlogPosts.ToList();

                    IList<AddBlogViewModel> blogViewModel = list.Select(bp => new AddBlogViewModel
                    {
                        BlogPostId = bp.Id,
                        Active = bp.Active,
                        CreateBy = bp.CreateBy,
                        CreateDate = bp.CreateDate,
                        Summary = bp.Summary,
                        CategoryId = bp.CategoryId,
                        AutherId = bp.AutherId,
                        Title = bp.Title
                    }).ToList();

                    return blogViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<AddBlogViewModel> getAllBlogsByAuthor(int? AuthorId)
        {
            try
            {
                using (DbContext)
                {
                    IList<BlogPost> list = DbContext.BlogPosts.Where(m => m.AutherId == AuthorId).ToList();

                    IList<AddBlogViewModel> blogViewModel = list.Select(bp => new AddBlogViewModel
                    {
                        BlogPostId = bp.Id,
                        Active = bp.Active,
                        CreateBy = bp.CreateBy,
                        CreateDate = bp.CreateDate,
                        Summary = bp.Summary,
                        CategoryId = bp.CategoryId,
                        AutherId = bp.AutherId,
                        Title = bp.Title
                    }).ToList();

                    return blogViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<AddBlogViewModel> getAllBlogsByCategory(int? CategoryId)
        {
            try
            {
                using (DbContext)
                {
                    IList<BlogPost> list = DbContext.BlogPosts.Where(m => m.CategoryId == CategoryId).ToList();

                    IList<AddBlogViewModel> blogViewModel = list.Select(bp => new AddBlogViewModel
                    {
                        BlogPostId = bp.Id,
                        Active = bp.Active,
                        CreateBy = bp.CreateBy,
                        CreateDate = bp.CreateDate,
                        Summary = bp.Summary,
                        CategoryId = bp.CategoryId,
                        AutherId = bp.AutherId,
                        Title = bp.Title
                    }).ToList();

                    return blogViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AddBlogViewModel getBlogById(long? Id)
        {
            try
            {
                using (DbContext)
                {
                    BlogPost bp = DbContext.BlogPosts.Find(Id);
                    AddBlogViewModel blogViewModel = new AddBlogViewModel
                    {
                        BlogPostId = bp.Id,
                        Active = bp.Active,
                        CreateBy = bp.CreateBy,
                        CreateDate = bp.CreateDate,
                        ModifyBy = bp.ModifyBy,
                        ModifyDate = bp.ModifyDate,
                        Summary = bp.Summary,
                        CategoryId = bp.CategoryId,
                        AutherId = bp.AutherId,
                        Title = bp.Title
                    };

                    return blogViewModel;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
