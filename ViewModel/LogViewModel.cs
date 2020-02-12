using System;

namespace ViewModel
{
    public class LogViewModel
    {
        public long Id { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Exception { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> CreatedBy { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
