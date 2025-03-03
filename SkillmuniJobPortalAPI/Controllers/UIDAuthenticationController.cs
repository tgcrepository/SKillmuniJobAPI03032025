// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UIDAuthenticationController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;
    public class UIDAuthenticationController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string USERID)
    {
      tbl_user dbuser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + USERID + "' and status='A'").FirstOrDefault<tbl_user>();
      if (dbuser != null)
      {
        tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == dbuser.ID_USER)).FirstOrDefault<tbl_profile>();
        tbl_csst_role tblCsstRole = this.db.tbl_csst_role.Where<tbl_csst_role>((Expression<Func<tbl_csst_role, bool>>) (t => t.id_csst_role == dbuser.ID_ROLE)).FirstOrDefault<tbl_csst_role>();
        tbl_organization tblOrganization = this.db.tbl_organization.Where<tbl_organization>((Expression<Func<tbl_organization, bool>>) (t => (int?) t.ID_ORGANIZATION == dbuser.ID_ORGANIZATION)).FirstOrDefault<tbl_organization>();
        LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
        loginResponseAuth.ResponseCode = "SUCCESS";
        loginResponseAuth.ResponseAction = 0;
        loginResponseAuth.ResponseMessage = "User successfully registered";
        loginResponseAuth.UserID = Convert.ToInt32(dbuser.ID_USER);
        loginResponseAuth.UserName = dbuser.USERID;
        int idOrganization = tblOrganization.ID_ORGANIZATION;
        loginResponseAuth.ROLEID = tblCsstRole.id_csst_role.ToString();
        loginResponseAuth.ORGID = idOrganization.ToString();
        loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
        loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
        loginResponseAuth.fullname = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
        loginResponseAuth.log_flag = new ChangePasswordLogic().CheckFirstLogin(loginResponseAuth.UserID);
        return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
      }
      string str = this.db.tbl_user.SqlQuery("select * from tbl_user where USERID='" + USERID + "'").FirstOrDefault<tbl_user>() == null ? "Invalid Username and Password..." : "Device not Registered with M2OST.Please contact Administrator..";
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
