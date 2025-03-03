// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getActiveBriefListController
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

    public class getActiveBriefListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID)
    {
      List<APIBrief> apiBriefList1 = new List<APIBrief>();
      List<APIBrief> apiBriefList2 = new BriefModel().getAPIBriefList("SELECT a.id_organization, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user " + " FROM tbl_brief_master a, tbl_brief_user_assignment b WHERE a.id_brief_master = b.id_brief_master AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) ORDER BY datetimestamp LIMIT 10 ");
      foreach (APIBrief apiBrief in apiBriefList2)
      {
        List<APIBrief> apiBriefList3 = new List<APIBrief>();
        apiBriefList3 = new BriefModel().getAPIBriefList("SELECT a.id_organization, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user " + " FROM tbl_brief_master a, tbl_brief_user_assignment b WHERE   LOWER(brief_code) = '" + apiBrief.brief_code.ToLower().Trim() + "' AND a.id_brief_master = b.id_brief_master AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) LIMIT 10 ");
      }
      return apiBriefList2 != null ? namespace2.CreateResponse<List<APIBrief>>(this.Request, HttpStatusCode.OK, apiBriefList2) : namespace2.CreateResponse<List<APIBrief>>(this.Request, HttpStatusCode.NoContent, apiBriefList2);
    }
  }
}
