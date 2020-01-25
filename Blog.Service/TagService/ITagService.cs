using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.TagViewModels;

namespace Blog.Service.TagService
{
    public interface ITagService
    {
        TagViewModel GetTagById(int? Id);
        IList<TagViewModel> GetAllTags();
        IList<TagViewModel> GetAllTagsByBlogId(long BlogId);
        int AddTag(TagViewModel model);
        int EditTag(TagViewModel model, int Id);
        int DeleteTag(int Id);
        int DeleteTagByBlogId(long BlogId);

    }
}
