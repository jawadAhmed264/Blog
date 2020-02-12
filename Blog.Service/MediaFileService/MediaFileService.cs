using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ViewModel.MediaFileViewModels;

namespace Blog.Service.MediaFileService
{
    public class MediaFileService : IMediaFileService
    {
        private BlogEntities DbContext;
        public MediaFileService(BlogEntities _DbContext)
        {
            DbContext = _DbContext;
        }
        public int AddMediaFile(MediaFileViewModel model)
        {
            try
            {
                
                    MediaFile mediaFile = new MediaFile()
                    {
                        Active = model.Active,
                        CreateBy = model.CreateBy,
                        CreateDate = model.CreateDate,
                        BlogPostId = model.BlogPostId,
                        Description = model.Description,
                        FileName = model.FileName,
                        MediaTypeId = model.MediaTypeId,
                        Url = model.Url
                    };
                    DbContext.MediaFiles.Add(mediaFile);
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteMediaFile(int Id)
        {
            try
            {
                
                    MediaFile mediaFile = DbContext.MediaFiles.Find(Id);
                    DbContext.MediaFiles.Remove(mediaFile);
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int DeleteMediaFilesByBlogId(long BlogId)
        {
            try
            {
               
                    var mediaFiles = DbContext.MediaFiles.Where(mf=>mf.BlogPostId==BlogId);
                    DbContext.MediaFiles.RemoveRange(mediaFiles);
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EditMediaFile(MediaFileViewModel model, int Id)
        {
            try
            {
                

                    MediaFile mediaFile = DbContext.MediaFiles.Find(Id);
                    mediaFile.Active = model.Active;
                    mediaFile.ModifyBy = model.ModifyBy;
                    mediaFile.ModifyDate = model.ModifyDate;
                    mediaFile.BlogPostId = model.BlogPostId;
                    mediaFile.Description = model.Description;
                    mediaFile.FileName = model.FileName;
                    mediaFile.MediaTypeId = model.MediaTypeId;
                    mediaFile.Url = model.Url;
                    DbContext.Entry(mediaFile).State = EntityState.Modified;
                    int res = DbContext.SaveChanges();
                    return res;
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<MediaFileViewModel> GetAllMediaFiles()
        {
            try
            {
                
                    IList<MediaFileViewModel> mediaFileList = DbContext.MediaFiles.Select(mf => new MediaFileViewModel
                    {
                        MediaFileId = mf.Id,
                        Active = mf.Active,
                        CreateBy = mf.CreateBy,
                        CreateDate = mf.CreateDate,
                        ModifyBy = mf.CreateBy,
                        ModifyDate = mf.CreateDate,
                        BlogPostId = mf.BlogPostId,
                        Description = mf.Description,
                        FileName = mf.FileName,
                        MediaTypeId = mf.MediaTypeId,
                        MediaType=mf.MediaType.TypeName,
                        Url = mf.Url
                    }).ToList();
                    return mediaFileList;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IList<MediaFileViewModel> GetAllMediaFilesByBlogId(long BlogId)
        {
            try
            {
                
                    IList<MediaFileViewModel> mediaFileList = DbContext.MediaFiles.Where(mf=>mf.BlogPostId==BlogId).Select(mf => new MediaFileViewModel
                    {
                        MediaFileId = mf.Id,
                        Active = mf.Active,
                        CreateBy = mf.CreateBy,
                        CreateDate = mf.CreateDate,
                        ModifyBy = mf.CreateBy,
                        ModifyDate = mf.CreateDate,
                        BlogPostId = mf.BlogPostId,
                        Description = mf.Description,
                        FileName = mf.FileName,
                        MediaTypeId = mf.MediaTypeId,
                        MediaType=mf.MediaType.TypeName,
                        Url = mf.Url
                    }).ToList();
                    return mediaFileList;
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MediaFileViewModel GetMediaFileById(int? Id)
        {
            try
            {
                
                    MediaFile mediaFile = DbContext.MediaFiles.Find(Id);
                    MediaFileViewModel model = new MediaFileViewModel()
                    {
                        MediaFileId = mediaFile.Id,
                        Active = mediaFile.Active,
                        CreateBy = mediaFile.CreateBy,
                        CreateDate = mediaFile.CreateDate,
                        ModifyBy = mediaFile.CreateBy,
                        ModifyDate = mediaFile.CreateDate,
                        BlogPostId = mediaFile.BlogPostId,
                        Description = mediaFile.Description,
                        FileName = mediaFile.FileName,
                        MediaTypeId = mediaFile.MediaTypeId,
                        MediaType = mediaFile.MediaType.TypeName,
                        Url = mediaFile.Url
                    };
                    return model;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
