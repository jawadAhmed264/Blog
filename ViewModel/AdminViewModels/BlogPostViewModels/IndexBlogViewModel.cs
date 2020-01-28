using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.AdminViewModels.BlogPostViewModels
{
    public class IndexBlogViewModel
    {
        public long BlogPostId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public String Content { get; set; }
        public string Tags { get; set; }
        public Nullable<int> AutherId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
        public string BannerUrl { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
