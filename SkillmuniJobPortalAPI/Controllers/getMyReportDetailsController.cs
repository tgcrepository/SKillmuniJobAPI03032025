// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getMyReportDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
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

    public class getMyReportDetailsController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int LID, int SID, int UID, int OID)
    {
      AssessmentSheet assessmentSheet = new AssessmentSheet();
      AssessmentResponce assessmentResponce = new AssessmentResponce();
      tbl_assessmnt_log tblAssessmntLog = this.db.tbl_assessmnt_log.Where<tbl_assessmnt_log>((Expression<Func<tbl_assessmnt_log, bool>>) (t => t.id_assessmnt_log == LID && SID == t.id_assessment_sheet)).FirstOrDefault<tbl_assessmnt_log>();
      if (tblAssessmntLog != null)
        assessmentResponce = JsonConvert.DeserializeObject<AssessmentResponce>(tblAssessmntLog.json_response);
      return namespace2.CreateResponse<AssessmentResponce>(this.Request, HttpStatusCode.OK, assessmentResponce);
    }
  }
}
