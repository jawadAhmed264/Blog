using System.Web;

namespace Blog.Service.Utilities
{
    public class Sessions
    {
        // private constructor
        private Sessions()
        {
            
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
        public string LoginId { get; set; }
        public string LoginName { get; set; }
        public string UserRole { get; set; }

    }
}
