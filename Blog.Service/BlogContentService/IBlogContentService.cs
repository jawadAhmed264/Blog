using System.Collections.Generic;
using ViewModel.BlogContentViewModels;

namespace Blog.Service.BlogContentService
{
    public interface IBlogContentService
    {
        IEnumerable<BlogContentViewModel> getAll();
        BlogContentViewModel getBlogContentById(int Id);
        BlogContentViewModel getBlogContentByBlogId(long BlogId);
        int AddBlogContent(BlogContentViewModel model);
        int EditBlogContent(BlogContentViewModel model, int Id);
        int DeleteBlogContent(int Id);
        int DeleteBlogContentByBlogId(long Id);
    }
}
