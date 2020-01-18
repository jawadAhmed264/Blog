using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.CategoryViewModels;

namespace ViewModel.AdminViewModels.BlogPostViewModels
{
    public class AddBlogViewModel
    {
        public long BlogPostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Tags{ get; set; }
        public Nullable<int> AutherId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
        public HttpPostedFileBase BannerImage{ get; set; }
        public IEnumerable<CategoryViewModel> CategoryList { get; set; }
        public string btnSumit { get; set; }
    }
}
