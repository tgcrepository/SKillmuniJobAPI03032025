// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefReadController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

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

    public class getBriefReadController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int BID, int UID, int OID)
    {
      tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == BID && t.status == "A")).FirstOrDefault<tbl_brief_master>();
      if (brief != null)
      {
        tbl_brief_read_status tblBriefReadStatus = this.db.tbl_brief_read_status.Where<tbl_brief_read_status>((Expression<Func<tbl_brief_read_status, bool>>) (t => t.id_user == (int?) UID && t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_read_status>();
        if (tblBriefReadStatus != null)
        {
          int? readStatus = tblBriefReadStatus.read_status;
          int num = 0;
          if (readStatus.GetValueOrDefault() == num & readStatus.HasValue)
          {
            tblBriefReadStatus.id_organization = new int?(OID);
            tblBriefReadStatus.read_status = new int?(1);
            tblBriefReadStatus.read_datetime = new DateTime?(DateTime.Now);
            tblBriefReadStatus.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
        }
        else
        {
          this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
          {
            id_user = new int?(UID),
            id_organization = new int?(OID),
            id_brief_master = new int?(brief.id_brief_master),
            read_status = new int?(1),
            status = "A",
            action_dateime = new DateTime?(),
            action_status = new int?(0),
            read_datetime = new DateTime?(DateTime.Now),
            updated_date_time = new DateTime?(DateTime.Now)
          });
          this.db.SaveChanges();
        }
      }
      return namespace2.CreateResponse<int>(this.Request, HttpStatusCode.OK, OID);
    }
  }
}
