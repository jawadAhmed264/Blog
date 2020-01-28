using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModel.AdminViewModels.CategoryViewModels;
using ViewModel.MediaFileViewModels;
using ViewModel.TagViewModels;

namespace ViewModel.AdminViewModels.BlogPostViewModels
{
    public class AddBlogViewModel
    {
        public long BlogPostId { get; set; }
        [Required(ErrorMessage ="Insert suitable title for blog")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Insert Summary for the blog")]
        public string Summary { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Write Some Blog Content")]
        public String Content { get; set; }
        [Required]
        public string Tags{ get; set; }
        public Nullable<int> AutherId { get; set; }
        [Required(ErrorMessage ="Select Category")]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
        public IEnumerable<MediaFileViewModel> MediaFiles{ get; set; }
        public IEnumerable<TagViewModel> TagList { get; set; }
        public string BannerUrl{ get; set; }
        public string ThumbnailUrl{ get; set; }
        public HttpPostedFileBase BannerImage{ get; set; }
        public IEnumerable<CategoryViewModel> CategoryList { get; set; }
        public string btnSubmit { get; set; }
    }
}
