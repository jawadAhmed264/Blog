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

    //public partial class Sessions
    //{
    //    #region ----- Constants -----

    //    /// ----- Application Session Keys -----
    //    private const string _pages = "_pages";

    //    /// ----- User Session Keys -----
    //    private const string _clientID = "_clientID";
    //    private const string _locID = "_locID";
    //    private const string _location = "_location";
    //    private const string _locationList = "_locationList";
    //    private const string _roleID = "_roleID";
    //    private const string _empRoleList = "_empRoleList";
    //    private const string _empRoleID = "_empRoleID";
    //    private const string _Role = "_Role";
    //    private const string _userID = "_userID";
    //    private const string _User = "_User";
    //    private const string _AuthList = "_AuthList";
    //    private const string _companyID = "_companyID";
    //    private const string _templateID = "_templateID";
    //    private const string _resourcesList = "_resourcesList";
    //    private const string _HomeURL = "_HomeURL";
    //    private const string _isPublished = "_isPublished";
    //    private const string _MenueAccessList = "_MenueAccessList";
    //    private const string _company = "_company";
    //    private string _sendSurvey = "SendSurveyModel";
    //    private string _nightMode = "nightMode";


    //    #endregion

    //    #region ----- Helper Methods -----

    //    private T GetSessionValue<T>(string sessionKey)
    //    {
    //        T retValue = default(T);
    //        try { retValue = (T)HttpContext.Current?.Session?[sessionKey]; }
    //        catch (Exception) { }
    //        return retValue;
    //    }

    //    private void SetSessionValue(string sessionKey, object value)
    //    {
    //        try
    //        {
    //            HttpContext.Current.Session[sessionKey] = value;
    //        }
    //        catch (Exception) { }
    //    }

    //    private bool SetupUserSession()
    //    {
    //        if (HttpContext.Current?.User?.Identity?.Name == "") { return false; }

    //        return SetupUserSession(HttpContext.Current?.User?.Identity?.Name);
    //        //return SetupUserSession("JawadAhmed");
    //    }

    //    public bool SetupUserSession(string username)
    //    {
    //        try
    //        {
    //            var userlist = UsersOperation.GetAll(new UserViewModel() { UserName = username }).Where(x => x.Active == true).ToList();

    //            if (userlist.Count == 0) return false;
    //            users = userlist[0];
    //            if (users.Active == false) return false;

    //            roles = UsersOperation.GetUserRolesByUserID(users.UserID).Where(x => x.Active == true).ToList();
    //            if (roles.Count == 0) return false;

    //            defaultRole = roles.Where(x => x.IsDefault == true).FirstOrDefault();

    //            //companies = MallsOperation.GetAll(new MallViewModel() { UserID = users.UserID }).Where(x => x.Active == true).ToList();
    //            //if (companies.Count > 0) { defaultCompany = companies.Where(x => x.CompanyID == defaultRole.CompanyID).FirstOrDefault() ; }

    //            AuthList = PermissionOperation.Get(new PermissionViewModel() { RoleId = defaultRole.RoleID });

    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            Logger.ErrorLog("", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ex);
    //            return false;
    //        }
    //    }

    //    public bool HasRights(EnumController controller, EnumAction action)
    //    {
    //        try
    //        {
    //            var permissions = this.AuthList.Where(x => x.Controller.Trim().ToLower() == controller.ToString().Trim().ToLower() && x.Action.Trim().ToLower() == action.ToString().Trim().ToLower());

    //            if (permissions.Count() > 0) return true;

    //            return false;
    //        }
    //        catch (Exception ex)
    //        {
    //            Logger.ErrorLog("", MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ex);
    //            return false;
    //        }
    //    }



    //    #endregion

    //    #region ----- Application Specific Sessions -----

    //    //public AppsList PagesList
    //    //{
    //    //    get
    //    //    {
    //    //        if (GetSessionValue<AppsList>(_pages) == null) { SetSessionValue(_pages, new AppsList(true)); }
    //    //        return GetSessionValue<AppsList>(_pages);
    //    //    }
    //    //    set { SetSessionValue(_pages, value); }
    //    //}

    //    #endregion

    //    public UserViewModel users
    //    {
    //        get
    //        {
    //            if (GetSessionValue<UserViewModel>(_User) == null) { SetupUserSession(); }
    //            return GetSessionValue<UserViewModel>(_User);
    //        }
    //        set { SetSessionValue(_User, value); }
    //    }

    //    //public MallViewModel defaultCompany
    //    //{
    //    //    get
    //    //    {
    //    //        if (GetSessionValue<MallViewModel>(_locID) == null) { SetupUserSession(); }
    //    //        return GetSessionValue<MallViewModel>(_locID);
    //    //    }
    //    //    set { SetSessionValue(_locID, value); }
    //    //}
    //    //public List<MallViewModel> companies
    //    //{
    //    //    get
    //    //    {
    //    //        if (GetSessionValue<List<MallViewModel>>(_company) == null) { SetupUserSession(); }
    //    //        return GetSessionValue<List<MallViewModel>>(_company);
    //    //    }
    //    //    set
    //    //    {
    //    //        SetSessionValue(_company, value);
    //    //    }
    //    //}        

    //    public UserRoles defaultRole
    //    {
    //        get
    //        {
    //            if (GetSessionValue<UserRoles>(_roleID) == null) { SetupUserSession(); }
    //            return GetSessionValue<UserRoles>(_roleID);
    //        }
    //        set { SetSessionValue(_roleID, value); }
    //    }

    //    public List<UserRoles> roles
    //    {
    //        get
    //        {
    //            if (GetSessionValue<List<UserRoles>>(_Role) == null) { SetupUserSession(); }
    //            return GetSessionValue<List<UserRoles>>(_Role);
    //        }
    //        set { SetSessionValue(_Role, value); }
    //    }

    //    public List<PermissionViewModel> AuthList
    //    {
    //        get
    //        {
    //            if (GetSessionValue<List<PermissionViewModel>>(_AuthList) == null) { SetupUserSession(); }
    //            return GetSessionValue<List<PermissionViewModel>>(_AuthList);
    //        }
    //        set { SetSessionValue(_AuthList, value); }
    //    }

    //    public SendSurveyModel SurveySession
    //    {
    //        get { return GetSessionValue<SendSurveyModel>(_sendSurvey); }
    //        set { SetSessionValue(_sendSurvey, value); }

    //    }
    //    public bool? nightMode
    //    {
    //        get { return GetSessionValue<bool>(_nightMode); }
    //        set { SetSessionValue(_nightMode, value); }

    //    }
    //    //public RoleFeatureList MenueAccessList
    //    //{
    //    //    get
    //    //    {
    //    //        if (GetSessionValue<RoleFeatureList>(_MenueAccessList) == null) { SetupUserSession(); }
    //    //        return GetSessionValue<RoleFeatureList>(_MenueAccessList);
    //    //    }
    //    //    set { SetSessionValue(_MenueAccessList, value); }
    //    //}

    //    //public string HomeURL
    //    //{
    //    //    get
    //    //    {
    //    //        if (GetSessionValue<string>(_HomeURL) == null || GetSessionValue<string>(_HomeURL) == "")
    //    //        { if (AuthList?.Count > 0) { SetSessionValue(_HomeURL, AuthList[0].URL); } else { SetSessionValue(_HomeURL, ""); } }
    //    //        return GetSessionValue<string>(_HomeURL);
    //    //    }
    //    //    set { SetSessionValue(_HomeURL, value); }
    //    //}

    //    public void Abandon()
    //    {
    //        try
    //        {
    //            HttpContext.Current.Session.Abandon();
    //        }
    //        catch (Exception) { }
    //    }

    //}
}
