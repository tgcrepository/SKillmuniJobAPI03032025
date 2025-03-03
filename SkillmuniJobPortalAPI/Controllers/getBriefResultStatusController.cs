// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefResultStatusController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class getBriefResultStatusController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID)
    {
      BriefScore briefScore = new BriefScore();
      briefScore.UID = UID;
      briefScore.OID = OID;
      List<tbl_brief_user_assignment> list1 = this.db.tbl_brief_user_assignment.SqlQuery("SELECT * FROM tbl_brief_user_assignment WHERE id_user = " + UID.ToString() + " AND id_brief_master IN (SELECT id_brief_master FROM tbl_brief_master WHERE id_organization = " + OID.ToString() + " AND status = 'A')").ToList<tbl_brief_user_assignment>();
      if (list1.Count > 0)
      {
        briefScore.TOTALCOUNT = list1.Count<tbl_brief_user_assignment>();
        List<tbl_brief_log> list2 = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.id_organization == (int?) OID && t.attempt_no == 1 && t.id_user == UID)).ToList<tbl_brief_log>();
        int num1 = 0;
        double? nullable = new double?(0.0);
        if (list2.Count<tbl_brief_log>() > 0)
        {
          num1 = list2.Count<tbl_brief_log>();
          nullable = list2.Average<tbl_brief_log>((Func<tbl_brief_log, double?>) (t => t.brief_result));
          int num2 = nullable.HasValue ? 1 : 0;
        }
        briefScore.BRIEFTAKEN = num1;
        briefScore.BRIEFSCORE = Convert.ToInt32((object) nullable);
      }
      else
      {
        briefScore.TOTALCOUNT = 0;
        briefScore.BRIEFSCORE = 0;
        briefScore.BRIEFTAKEN = 0;
      }
      return namespace2.CreateResponse<BriefScore>(this.Request, HttpStatusCode.OK, briefScore);
    }
  }
}
