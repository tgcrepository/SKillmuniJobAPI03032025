// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefStatusController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class getBriefStatusController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID)
    {
      BriefStatus briefStatus = new BriefStatus();
      briefStatus.UID = UID;
      briefStatus.OID = OID;
      List<tbl_brief_read_status> list = this.db.tbl_brief_read_status.SqlQuery("SELECT * FROM tbl_brief_read_status WHERE id_user =  " + UID.ToString() + "  AND id_organization =  " + OID.ToString() + "  AND status = 'A' AND id_brief_master IN (SELECT id_brief_master FROM tbl_brief_user_assignment WHERE id_user = " + UID.ToString() + " AND status = 'A')").ToList<tbl_brief_read_status>();
      if (list.Count<tbl_brief_read_status>() > 0)
      {
        briefStatus.TOTALCOUNT = list.Count<tbl_brief_read_status>();
        briefStatus.READCOUNT = list.Where<tbl_brief_read_status>((Func<tbl_brief_read_status, bool>) (t =>
        {
          int? readStatus = t.read_status;
          int num = 1;
          return readStatus.GetValueOrDefault() == num & readStatus.HasValue;
        })).Count<tbl_brief_read_status>();
        briefStatus.UNREADCOUNT = list.Where<tbl_brief_read_status>((Func<tbl_brief_read_status, bool>) (t =>
        {
          int? readStatus = t.read_status;
          int num = 0;
          return readStatus.GetValueOrDefault() == num & readStatus.HasValue;
        })).Count<tbl_brief_read_status>();
      }
      else
      {
        briefStatus.TOTALCOUNT = 0;
        briefStatus.READCOUNT = 0;
        briefStatus.UNREADCOUNT = 0;
      }
      return namespace2.CreateResponse<BriefStatus>(this.Request, HttpStatusCode.OK, briefStatus);
    }
  }
}
