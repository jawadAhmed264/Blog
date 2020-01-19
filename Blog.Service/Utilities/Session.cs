using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Service.Utilities
{
    public class Session
    {
        // private constructor
        private Session()
        {
           // FillOrder = new List<string>();
        }

        // Gets the current session.
        public static Session Current
        {
            get
            {
                var session = (Session)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new Session();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        // **** add your session properties here, e.g like this:
        public int LoginId { get; set; }
    }
}
