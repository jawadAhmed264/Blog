using Blog.Data.Models;
using Blog.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.AdminViewModels.BlogPostViewModels;
using ViewModel.Enums;

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
                        MediaTypeId = DbContext.MediaTypes.FirstOrDefault(mt => mt.TypeName == MediaTypeEnum.BlogImage.ToString()).Id,
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
                    if (res > 0)
                    {
                        return res;
                    }
                    else
                    {
                        DbContext.Dispose();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    string exString = "Message: "+ex.Message+" StackTrace: "+ex.StackTrace;
                    LogManagement.LogError(new LogViewModel { Exception = exString,Controller= "BlogService(ServiceLayer)", Action="AddBlog",Active=true,CreatedDate=DateTime.Now});
                    throw ex;
                }
            }
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
                        MediaTypeId = DbContext.MediaTypes.FirstOrDefault(mt => mt.TypeName == MediaTypeEnum.BlogImage.ToString()).Id,
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

                    int res= DbContext.SaveChanges();
                    return 0;
                }
                catch (Exception ex)
                {
                    string exString = "Message: " + ex.Message + " StackTrace: " + ex.StackTrace;
                    LogManagement.LogError(new LogViewModel { Exception = exString, Controller = "BlogService(ServiceLayer)", Action = "EditBlog", Active = true, CreatedDate = DateTime.Now });
                    throw ex;
                }
            }
        }
    }
}
