// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefNativeNotificatonController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getBriefNativeNotificatonController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID)
    {
      List<APIBrief> apiBriefList1 = new List<APIBrief>();
      List<APIBrief> apiBriefList2 = new BriefModel().getAPIBriefList("SELECT a.id_organization, question_count, brief_title, brief_code, 'NA' brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, 'NA' brief_category, 'NA' brief_subcategory, '0' id_brief_category, '0' id_brief_subcategory FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c WHERE a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND b.id_user = " + UID.ToString() + "  AND read_status = 0 AND action_status = 0 AND a.id_organization = " + OID.ToString() + "  AND a.status = 'A' AND c.status = 'A' AND (b.scheduled_status = 'S' OR b.published_status = 'S') AND (b.published_datetime < NOW() OR b.scheduled_datetime < NOW()) ORDER BY a.brief_title ");
      int num = 1;
      foreach (APIBrief apiBrief in apiBriefList2)
      {
        apiBrief.SRNO = num;
        ++num;
        apiBrief.RESULTSTATUS = 0;
        apiBrief.RESULTSCORE = 0.0;
      }
      return apiBriefList2 != null ? namespace2.CreateResponse<List<APIBrief>>(this.Request, HttpStatusCode.OK, apiBriefList2) : namespace2.CreateResponse<List<APIBrief>>(this.Request, HttpStatusCode.NoContent, apiBriefList2);
    }
  }
}
