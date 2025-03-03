// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BAuthenticationWithMenuController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class B2BAuthenticationWithMenuController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] B2BUser user)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
      try
      {
        user.PASSWORD = user.PASSWORD.ToMD5Hash();
        tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + user.USERID + "' and PASSWORD='" + user.PASSWORD + "' limit 1").FirstOrDefault<tbl_user>();
        if (tblUser != null)
        {
          tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
          {
            (object) tblUser.ID_ORGANIZATION
          });
          if (tblUser.STATUS == "A")
          {
            loginResponseAuth.ResponseCode = "SUCCESS";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = "User successfully registered";
            loginResponseAuth.UserID = Convert.ToInt32(tblUser.ID_USER);
            loginResponseAuth.UserName = tblUser.USERID;
            int idOrganization = tblOrganization.ID_ORGANIZATION;
            loginResponseAuth.ROLEID = "1";
            loginResponseAuth.ORGID = idOrganization.ToString();
            loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
            loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
            loginResponseAuth.ORGEMAIL = tblOrganization.DEFAULT_EMAIL;
            if (string.IsNullOrEmpty(user.IMEI))
            {
              this.db.tbl_report_login_log.Add(new tbl_report_login_log()
              {
                id_user = new int?(tblUser.ID_USER),
                id_organization = tblUser.ID_ORGANIZATION,
                IMEI = "WEBSITE",
                LOG_DATETIME = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
            }
            else
            {
              this.db.tbl_report_login_log.Add(new tbl_report_login_log()
              {
                id_user = new int?(tblUser.ID_USER),
                id_organization = tblUser.ID_ORGANIZATION,
                IMEI = user.IMEI,
                LOG_DATETIME = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
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
          string str3 = this.db.tbl_user.SqlQuery("select * from tbl_user where USERID='" + user.USERID + "' and PASSWORD='" + user.PASSWORD + "' ").FirstOrDefault<tbl_user>() == null ? "Invalid Username and Password..." : "Device not Registered with M2OST.Please contact Administrator..";
          loginResponseAuth.ResponseCode = "FAILURE";
          loginResponseAuth.ResponseAction = 0;
          loginResponseAuth.ResponseMessage = str3;
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

    public HttpResponseMessage Get(
      string IMEI,
      string USERID,
      string PASSWORD,
      string OS,
      string Network,
      string OSVersion,
      string Details)
    {
      PASSWORD = PASSWORD.ToMD5Hash();
      tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + USERID + "' and PASSWORD='" + PASSWORD + "' and ID_USER in (select ID_USER from tbl_user_device_link where DEVICE_ID='" + IMEI + "')").FirstOrDefault<tbl_user>();
      if (tblUser != null)
      {
        tbl_csst_role tblCsstRole = this.db.tbl_csst_role.Find(new object[1]
        {
          (object) tblUser.ID_ROLE
        });
        tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
        {
          (object) tblCsstRole.id_organization
        });
        LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
        loginResponseAuth.ResponseCode = "SUCCESS";
        loginResponseAuth.ResponseAction = 0;
        loginResponseAuth.ResponseMessage = "User successfully registered";
        loginResponseAuth.UserID = Convert.ToInt32(tblUser.ID_USER);
        loginResponseAuth.UserName = tblUser.USERID;
        int idOrganization = tblOrganization.ID_ORGANIZATION;
        loginResponseAuth.ROLEID = tblCsstRole.id_csst_role.ToString();
        loginResponseAuth.ORGID = idOrganization.ToString();
        loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
        loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
        loginResponseAuth.menu_response = new RegistrationModel().get_menu(idOrganization);
        return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
      }
      string str = this.db.tbl_user.SqlQuery("select * from tbl_user where USERID='" + USERID + "' and PASSWORD='" + PASSWORD + "' ").FirstOrDefault<tbl_user>() == null ? "Invalid Username and Password..." : "Device not Registered with M2OST.Please contact Administrator..";
      LoginResponseAuth loginResponseAuth1 = new LoginResponseAuth();
      loginResponseAuth1.ResponseCode = "FAILURE";
      loginResponseAuth1.ResponseAction = 0;
      loginResponseAuth1.ResponseMessage = str;
      loginResponseAuth1.UserID = 0;
      loginResponseAuth1.UserName = "";
      int num = 0;
      loginResponseAuth1.ROLEID = "";
      loginResponseAuth1.ORGID = num.ToString();
      loginResponseAuth1.LogoPath = "";
      loginResponseAuth1.BannerPath = "";
      return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth1);
    }
  }
}
