using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.MediaFileViewModels;

namespace Blog.Service.MediaFileService
{
    public interface IMediaFileService
    {
        MediaFileViewModel GetMediaFileById(int? Id);
        IList<MediaFileViewModel> GetAllMediaFiles();
        IList<MediaFileViewModel> GetAllMediaFilesByBlogId(long BlogId);
        int AddMediaFile(MediaFileViewModel model);
        int EditMediaFile(MediaFileViewModel model, int Id);
        int DeleteMediaFile(int Id);
        int DeleteMediaFilesByBlogId(long BlogId);

    }
}
