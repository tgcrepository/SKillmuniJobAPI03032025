// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getMyReportsController
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

    public class getMyReportsController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, string FLAG, int OID, int MID)
    {
      MyReport myReport = new MyReport();
      List<AssessmentReport> assessmentReportList1 = new List<AssessmentReport>();
      List<AssessmentReport> assessmentReportList2 = new List<AssessmentReport>();
      foreach (tbl_assessment_sheet tblAssessmentSheet1 in this.db.tbl_assessment_sheet.SqlQuery("select a.* from  tbl_assessment_sheet a,tbl_assessment b where a.id_assessment_sheet in (select distinct id_assessment_sheet from  tbl_assessmnt_log where id_user=" + UID.ToString() + ") and a.id_assesment=b.id_assessment order by b.assessment_title ").ToList<tbl_assessment_sheet>())
      {
        tbl_assessment_sheet lItem = tblAssessmentSheet1;
        IQueryable<tbl_assessmnt_log> source = this.db.tbl_assessmnt_log.Where<tbl_assessmnt_log>((Expression<Func<tbl_assessmnt_log, bool>>) (t => t.id_user == UID && t.id_assessment_sheet == lItem.id_assessment_sheet));
        Expression<Func<tbl_assessmnt_log, DateTime?>> keySelector = (Expression<Func<tbl_assessmnt_log, DateTime?>>) (t => t.updated_date_time);
        foreach (tbl_assessmnt_log tblAssessmntLog in source.OrderByDescending<tbl_assessmnt_log, DateTime?>(keySelector).ToList<tbl_assessmnt_log>())
        {
          AssessmentReport assessmentReport = new AssessmentReport();
          tbl_assessment_sheet tblAssessmentSheet2 = this.db.tbl_assessment_sheet.Find(new object[1]
          {
            (object) tblAssessmntLog.id_assessment_sheet
          });
          tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
          {
            (object) tblAssessmentSheet2.id_assesment
          });
          assessmentReport.id_assessment_log = tblAssessmntLog.id_assessmnt_log;
          assessmentReport.id_assessment_sheet = tblAssessmentSheet2.id_assessment_sheet;
          assessmentReport.id_assessment = tblAssessment.id_assessment;
          assessmentReport.assessment_name = tblAssessment.assessment_title;
          assessmentReport.assessment_description = tblAssessment.assesment_description;
          assessmentReport.attempt = tblAssessmntLog.attempt_no.ToString();
          assessmentReport.LogDate = tblAssessmntLog.updated_date_time.Value.ToString("dd-MMM-yyyy HH:mm");
          int? assessmentType1 = tblAssessment.assessment_type;
          int num1 = 1;
          if (assessmentType1.GetValueOrDefault() == num1 & assessmentType1.HasValue)
            assessmentReportList1.Add(assessmentReport);
          int? assessmentType2 = tblAssessment.assessment_type;
          int num2 = 2;
          if (assessmentType2.GetValueOrDefault() == num2 & assessmentType2.HasValue)
            assessmentReportList2.Add(assessmentReport);
        }
      }
      myReport.LEARNING = assessmentReportList1;
      myReport.PSYCHOMETRIC = assessmentReportList2;
      return namespace2.CreateResponse<MyReport>(this.Request, HttpStatusCode.OK, myReport);
    }
  }
}
