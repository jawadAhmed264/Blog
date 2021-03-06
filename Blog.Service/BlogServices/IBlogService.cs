﻿using System.Collections.Generic;
using ViewModel.AdminViewModels.BlogPostViewModels;

namespace Blog.Service.BlogServices
{
    public interface IBlogService
    {
        int AddBlog(AddBlogViewModel model);
        int EditBlog(AddBlogViewModel model, long Id);
        int DeleteBlog(long Id);
        int Publish(long Id);
        AddBlogViewModel getBlogById(long? Id);
        IList<IndexBlogViewModel> getAllBlogs();
        IList<AddBlogViewModel> getAllBlogsByAuthor(int? AuthorId);
        IList<AddBlogViewModel> getAllBlogsByCategory(int? CategoryId);

    }
}
