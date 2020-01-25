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
            throw new NotImplementedException();
        }

        public int EditBlog(AddBlogViewModel model, long Id)
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

                    BlogPost blogPost = DbContext.BlogPosts.Find(Id);
                    blogPost.MediaFiles = mediaFiles;
                    blogPost.Tags = Tags;
                    blogPost.AutherId = model.AutherId;
                    blogPost.CategoryId = model.CategoryId;
                    blogPost.Active = model.Active;
                    blogPost.ModifyBy = model.ModifyBy;
                    blogPost.ModifyDate = model.ModifyDate;
                    blogPost.Summary = model.Summary;
                    blogPost.Title = model.Title;

                    BlogContent blogContent = DbContext.BlogContents.FirstOrDefault(m => m.BlogPostId == Id);
                    blogContent.BlogPost = blogPost;
                    blogContent.Active = model.Active;
                    blogContent.Content = model.Content;
                    blogContent.ModifyBy = model.ModifyBy;
                    blogContent.ModifyDate = model.ModifyDate;

                    DbContext.Entry(blogPost).State = EntityState.Modified;
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
                    IList<BlogPost> list = DbContext.BlogPosts.Where(m=>m.AutherId==AuthorId).ToList();

                    IList<AddBlogViewModel> blogViewModel = list.Select(bp => new AddBlogViewModel
                    {
                        BlogPostId = bp.Id,
                        Active = bp.Active,
                        CreateBy = bp.CreateBy,
                        CreateDate = bp.CreateDate,
                        Summary = bp.Summary,
                        CategoryId = bp.CategoryId,
                        AutherId = bp.AutherId,
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
                    BlogPost bp = DbContext.BlogPosts.SingleOrDefault(m => m.Id == Id);
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
