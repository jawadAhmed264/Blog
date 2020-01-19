using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
