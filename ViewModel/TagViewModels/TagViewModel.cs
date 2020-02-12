using System;

namespace ViewModel.TagViewModels
{
    public class TagViewModel
    {
        public long Id { get; set; }
        public string TagName { get; set; }
        public Nullable<long> BlogPostId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
