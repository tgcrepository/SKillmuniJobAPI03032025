// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BIPhoneController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class B2BIPhoneController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] B2BIphone user)
    {
      user.PASSWORD = user.PASSWORD.ToMD5Hash();
      tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID like \"" + user.USERID + "\" AND PASSWORD like \"" + user.PASSWORD + "\" AND  id_organization=" + user.ORG_ID.ToString() + " AND ID_ROLE=" + user.ROLE_ID.ToString() + " ").FirstOrDefault<tbl_user>();
      return tblUser != null ? namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, tblUser.USERID) : namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "false");
    }
  }
}
