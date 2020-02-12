using System;

namespace ViewModel.BlogContentViewModels
{
    public class BlogContentViewModel
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public Nullable<long> BlogPostId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
