// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CeDashboardController
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

    public class CeDashboardController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, string trf)
    {
      CEDashboard ceDashboard = new CEDashboard();
      List<CEAssessment> ceAssessmentList = new List<CEAssessment>();
      int num1 = 0;
      int num2 = 0;
      bool flag1 = false;
      bool flag2 = false;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        string sql1 = "SELECT * FROM tbl_ce_evaluation_tile where id_organization=" + OID.ToString() + " and ce_evaluation_code='" + trf + "'";
        tbl_ce_evaluation_tile ceEvaluationTile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_tile>(sql1).FirstOrDefault<tbl_ce_evaluation_tile>();
        ceDashboard.tile = ceEvaluationTile;
        string sql2 = "select * from tbl_ce_career_evaluation_master where id_ce_evaluation_tile=" + ceEvaluationTile.id_ce_evaluation_tile.ToString() + " AND status='A' order by ordering_sequence_number ";
        List<tbl_ce_career_evaluation_master> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>(sql2).ToList<tbl_ce_career_evaluation_master>();
        string sql3 = "SELECT * FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization =  " + OID.ToString() + " ORDER BY attempt_no DESC LIMIT 1";
        tbl_ce_evaluation_index ceEvaluationIndex = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_index>(sql3).FirstOrDefault<tbl_ce_evaluation_index>();
        if (ceEvaluationIndex != null)
        {
          num1 = ceEvaluationIndex.attempt_no;
          if (num1 > 0)
            num2 = num1 - 1;
        }
        double count1 = (double) list1.Count;
        string sql4 = "SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")";
        double count2 = (double) m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>(sql4).ToList<tbl_ce_career_evaluation_master>().Count;
        string sql5 = "SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num1.ToString() + ")";
        double count3 = (double) m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>(sql5).ToList<tbl_ce_career_evaluation_master>().Count;
        if (count2 > 0.0)
        {
          double num3 = count2 / count1 * 100.0;
          ceDashboard.ceCurrentPercentage = Math.Round(num3, 2);
          if (count1 == count2)
            flag1 = true;
        }
        ceDashboard.ceCurrentStatus = !flag1 ? "Incomplete" : "Completed";
        if (num2 > 0)
        {
          double count4 = (double) list1.Count;
          string sql6 = "SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")";
          double count5 = (double) m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>(sql6).ToList<tbl_ce_career_evaluation_master>().Count;
          string sql7 = "SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + OID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + num2.ToString() + ")";
          double count6 = (double) m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_career_evaluation_master>(sql7).ToList<tbl_ce_career_evaluation_master>().Count;
          if (count5 > 0.0)
          {
            double num4 = count5 / count4 * 100.0;
            ceDashboard.ceCurrentPercentage = Math.Round(num4, 2);
            flag2 = count4 == count5;
          }
        }
        ceDashboard.cePreviousStatus = !flag2 ? "Incomplete" : "Completed";
        string sql8 = "SELECT b.akcode, b.answer_key, SUM(a.job_point) job_point FROM tbl_ce_evaluation_audit a, tbl_ce_evalution_answer_key b WHERE a.attempt_no = " + num1.ToString() + " AND a.id_ce_evalution_answer_key = b.id_ce_evalution_answer_key AND a.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " GROUP BY b.key_code";
        List<CEAnswerKey> list2 = m2ostnextserviceDbContext.Database.SqlQuery<CEAnswerKey>(sql8).ToList<CEAnswerKey>();
        ceDashboard.CareerDriver = list2;
        List<CECategory> ceCategoryList = new List<CECategory>();
        foreach (tbl_ce_career_evaluation_master evaluationMaster in list1)
        {
          CEAssessment ceAssessment = new CEAssessment();
          ceAssessment.career_evaluation_title = evaluationMaster.career_evaluation_title;
          ceAssessment.career_evaluation_code = evaluationMaster.career_evaluation_code;
          ceAssessment.ce_assessment_type = Convert.ToInt32((object) evaluationMaster.ce_assessment_type);
          int? ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num5 = 1;
          if (ceAssessmentType.GetValueOrDefault() == num5 & ceAssessmentType.HasValue)
            ceAssessment.cea_type = "SUL-MCA";
          ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num6 = 2;
          if (ceAssessmentType.GetValueOrDefault() == num6 & ceAssessmentType.HasValue)
            ceAssessment.cea_type = "Psychometric Assessment";
          ceAssessment.job_points_for_ra = Convert.ToInt32((object) evaluationMaster.job_points_for_ra);
          string sql9 = "SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + "  GROUP BY attempt_no ORDER BY attempt_no DESC LIMIT 3";
          List<JobPoint> list3 = m2ostnextserviceDbContext.Database.SqlQuery<JobPoint>(sql9).ToList<JobPoint>();
          List<int> intList = new List<int>();
          for (int index = 0; index < 3; ++index)
          {
            int num7 = 0;
            if (list3.ElementAtOrDefault<JobPoint>(index) != null)
              num7 = list3[index].job_point;
            intList.Add(num7);
          }
          ceAssessmentList.Add(ceAssessment);
        }
        ceDashboard.ceEvaluation = ceAssessmentList;
        ceDashboard.last_attempt_no = num2;
        ceDashboard.latest_attempt_no = num1;
        string sql10 = "SELECT CASE WHEN attempt_no > 0 THEN attempt_no ELSE 0 END attempt_no, CASE WHEN SUM(job_point) > 0 THEN SUM(job_point) ELSE 0 END job_point FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " GROUP BY attempt_no order by attempt_no desc LIMIT 2";
        List<JobPoint> list4 = m2ostnextserviceDbContext.Database.SqlQuery<JobPoint>(sql10).ToList<JobPoint>();
        if (list4.ElementAtOrDefault<JobPoint>(0) != null)
          ceDashboard.ceCurrentScore = list4[0].job_point;
        if (list4.ElementAtOrDefault<JobPoint>(1) != null)
          ceDashboard.cePreviousScore = list4[1].job_point;
      }
      return namespace2.CreateResponse<CEDashboard>(this.Request, HttpStatusCode.NoContent, ceDashboard);
    }
  }
}
