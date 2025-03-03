// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BAuthenticationPostController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;


    public class B2BAuthenticationPostController : ApiController
  {
    public db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string UID, string password)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
      try
      {
        password = HttpUtility.UrlDecode(password);
        tbl_user tblUser = new tbl_user();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblUser = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user where USERID={0} and PASSWORD={1}", (object) UID, (object) password).FirstOrDefault<tbl_user>();
        if (tblUser != null)
        {
          if (tblUser.STATUS == "A")
          {
            loginResponseAuth.ResponseCode = "SUCCESS";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = "User successfully registered";
            loginResponseAuth.UserID = Convert.ToInt32(tblUser.ID_USER);
            loginResponseAuth.UserName = tblUser.USERID;
            loginResponseAuth.ROLEID = "1";
            loginResponseAuth.ORGID = Convert.ToString((object) tblUser.ID_ORGANIZATION);
            loginResponseAuth.LogoPath = "";
            loginResponseAuth.BannerPath = "";
            loginResponseAuth.ORGEMAIL = "";
            loginResponseAuth.log_flag = 0;
            tbl_profile tblProfile = new tbl_profile();
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            if (tblProfile != null)
            {
              loginResponseAuth.fullname = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              loginResponseAuth.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
            }
            else
            {
              loginResponseAuth.fullname = "NA";
              loginResponseAuth.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
            }
          }
          else
          {
            string str2 = "User account is deactivated. Please contact your administrator.";
            loginResponseAuth.ResponseCode = "FAILURE";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = str2;
            loginResponseAuth.UserID = 0;
            loginResponseAuth.UserName = "";
            int num = 0;
            loginResponseAuth.ROLEID = "";
            loginResponseAuth.ORGID = num.ToString();
            loginResponseAuth.LogoPath = "";
            loginResponseAuth.BannerPath = "";
            loginResponseAuth.ORGEMAIL = "";
          }
        }
        else
        {
          loginResponseAuth.ResponseCode = "FAILURE";
          loginResponseAuth.ResponseAction = 0;
          loginResponseAuth.ResponseMessage = "User credentials re wrong.";
          loginResponseAuth.UserID = 0;
          loginResponseAuth.UserName = "";
          int num = 0;
          loginResponseAuth.ROLEID = "";
          loginResponseAuth.ORGID = num.ToString();
          loginResponseAuth.LogoPath = "";
          loginResponseAuth.BannerPath = "";
          loginResponseAuth.ORGEMAIL = "";
        }
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
      }
      return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
    }
  }
}
