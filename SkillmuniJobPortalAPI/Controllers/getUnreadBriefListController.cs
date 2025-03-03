// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getUnreadBriefListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
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

    public class getUnreadBriefListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string uids)
    {
      int num = 0;
      uids = new AESAlgorithm().getDecryptedString(uids);
      tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where USERID='" + uids + "' and status='A'").FirstOrDefault<tbl_user>();
      if (tblUser != null)
      {
        List<APIBrief> apiBriefList = new List<APIBrief>();
        string str = "select * from tbl_brief_user_assignment where id_user='" + tblUser.ID_USER.ToString() + "' and assignment_status='S'";
        List<tbl_brief_read_status> list = this.db.tbl_brief_read_status.SqlQuery("SELECT * FROM tbl_brief_read_status where id_user='" + tblUser.ID_USER.ToString() + "' and read_status=0 and action_status=0 and status='A'").ToList<tbl_brief_read_status>();
        if (list.Count > 0)
          num = list.Count;
      }
      return namespace2.CreateResponse<int>(this.Request, HttpStatusCode.OK, num);
    }
  }
}
