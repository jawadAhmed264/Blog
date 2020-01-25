using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.MediaFileViewModels
{
    public class MediaFileViewModel
    {
        public MediaFileViewModel() 
        {
            Url = "/Content/Images/defaultBanner.jpg";
            FileName = "default";  
        }
        public long MediaFileId { get; set; }
        public string FileName { get; set; }
        public string Url { get; set; }
        public Nullable<long> BlogPostId { get; set; }
        public Nullable<int> MediaTypeId { get; set; }
        public string MediaType { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
