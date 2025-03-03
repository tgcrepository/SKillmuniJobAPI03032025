// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BIPhoneAuthenticationController
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

    public class B2BIPhoneAuthenticationController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] B2BUser user)
    {
      user.PASSWORD = user.PASSWORD.ToMD5Hash();
      tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + user.USERID + "' and PASSWORD='" + user.PASSWORD + "'").FirstOrDefault<tbl_user>();
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
        return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
      }
      this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + user.USERID + "' and PASSWORD='" + user.PASSWORD + "' ").FirstOrDefault<tbl_user>();
      string str = tblUser == null ? "Invalid Username and Password..." : "Device not Registered with Cafe Style Culture Club...";
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
