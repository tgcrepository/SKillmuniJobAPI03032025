// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BIPhoneAuthController
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

    public class B2BIPhoneAuthController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private string responceString = "";

    public HttpResponseMessage Post([FromBody] B2BIphoneAuth user)
    {
      user.PASSWORD = user.PASSWORD.ToMD5Hash();
      tbl_user dbuser = this.db.tbl_user.SqlQuery(" select * from tbl_user where lower(USERID) like lower(\"" + user.USERID + "\") AND PASSWORD like \"" + user.PASSWORD + "\" AND ID_ORGANIZATION=" + user.ORG_ID.ToString() + "  AND ID_ROLE=" + user.ROLE_ID.ToString() + " ").FirstOrDefault<tbl_user>();
      if (dbuser != null)
      {
        tbl_user_device_link tblUserDeviceLink = this.db.tbl_user_device_link.Where<tbl_user_device_link>((Expression<Func<tbl_user_device_link, bool>>) (t => t.ID_USER == dbuser.ID_USER)).FirstOrDefault<tbl_user_device_link>();
        if (tblUserDeviceLink != null)
        {
          DateTime? expiryDate = this.db.tbl_user.Find(new object[1]
          {
            (object) tblUserDeviceLink.ID_USER
          }).EXPIRY_DATE;
          DateTime now = DateTime.Now;
          if ((expiryDate.HasValue ? (expiryDate.GetValueOrDefault() < now ? 1 : 0) : 0) != 0)
          {
            this.responceString = "expired";
          }
          else
          {
            tblUserDeviceLink.UPDATED_DATE_TIME = DateTime.Now;
            if (!string.IsNullOrEmpty(user.DEVID))
              tblUserDeviceLink.DEVICE_ID = user.DEVID;
            this.db.SaveChanges();
            this.responceString = user.USERID;
          }
        }
      }
      else
        this.responceString = "false";
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, this.responceString);
    }
  }
}
