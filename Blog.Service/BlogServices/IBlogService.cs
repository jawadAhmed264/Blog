﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.AdminViewModels.BlogPostViewModels;

namespace Blog.Service.BlogServices
{
    public interface IBlogService
    {
        int AddBlog(AddBlogViewModel model);
        int EditBlog(AddBlogViewModel model,long Id);

    }
}