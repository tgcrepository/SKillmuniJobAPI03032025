// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefCompletionListController
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

    public class getBriefCompletionListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID)
    {
      List<BriefCollection> userTestResult = new BriefModel().getUserTestResult("SELECT b.brief_code,a.id_user,a.id_brief_master, b.brief_title, CASE WHEN a.brief_result IS NULL THEN 0 ELSE a.brief_result END brief_result,a.attempt_no, c.FIRSTNAME FROM tbl_brief_log a, tbl_brief_master b, tbl_profile c WHERE a.id_brief_master = b.id_brief_master AND a.id_user = c.ID_USER AND a.id_organization=" + OID.ToString() + " AND a.id_user=" + UID.ToString() + " and b.status='A' order by id_brief_log desc limit 20");
      return userTestResult != null ? namespace2.CreateResponse<List<BriefCollection>>(this.Request, HttpStatusCode.OK, userTestResult) : namespace2.CreateResponse<List<BriefCollection>>(this.Request, HttpStatusCode.NoContent, userTestResult);
    }
  }
}
