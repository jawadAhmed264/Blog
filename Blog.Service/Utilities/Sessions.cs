using Blog.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Service.Utilities
{
    public class Sessions
    {
        // private constructor
        private Sessions()
        {
           // FillOrder = new List<string>();
        }

        // Gets the current session.
        public static Sessions Current
        {
            get
            {
                var session = (Sessions)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new Sessions();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        // **** add your session properties here, e.g like this:
        public int LoginId { get; set; }
        public User UserInfo{ get; set; }
        public string UserRole { get; set; }
        public string Username { get; set; }
        public string UserId{ get; set; }

    }
}
