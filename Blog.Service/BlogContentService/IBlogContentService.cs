using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
