// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GameScoringController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class GameScoringController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage GET(int OID)
    {
      foreach (tbl_assessmnt_log tblAssessmntLog in this.db.tbl_assessmnt_log.SqlQuery("select * from tbl_assessmnt_log where id_organization =" + OID.ToString() + " ").ToList<tbl_assessmnt_log>())
      {
        tbl_assessment tblAssessment = this.db.tbl_assessment.SqlQuery("select * from tbl_assessment where id_assessment in (select id_assesment from tbl_assessment_sheet where id_assessment_sheet=" + tblAssessmntLog.id_assessment_sheet.ToString() + " )").FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          string[] strArray1 = new string[5]
          {
            "select * from tbl_category where id_organization=",
            OID.ToString(),
            " and id_category in (select distinct id_category from tbl_assessment_categoty_mapping where id_assessment=",
            null,
            null
          };
          int num1 = tblAssessment.id_assessment;
          strArray1[3] = num1.ToString();
          strArray1[4] = " )";
          foreach (tbl_category tblCategory in this.db.tbl_category.SqlQuery(string.Concat(strArray1)).ToList<tbl_category>())
          {
            if (new ProgramScoringModel().checkProgramComplition(tblCategory.ID_CATEGORY, tblAssessmntLog.id_user, OID) == "0")
            {
              sc_program_content_summary entity = new sc_program_content_summary();
              entity.id_category = new int?(tblCategory.ID_CATEGORY);
              entity.id_organization = new int?(OID);
              entity.id_user = new int?(tblAssessmntLog.id_user);
              num1 = tblCategory.ID_CATEGORY;
              int recordCount1 = new ContentReportModel().getRecordCount("select count(*) count from tbl_content_organization_mapping where id_category=" + num1.ToString());
              int num2 = 0;
              if (recordCount1 > 0)
              {
                string[] strArray2 = new string[7];
                strArray2[0] = "select count(*) count from tbl_content_organization_mapping where id_category=";
                num1 = tblCategory.ID_CATEGORY;
                strArray2[1] = num1.ToString();
                strArray2[2] = " and id_content not in (select distinct id_content from tbl_content_counters where id_user=";
                num1 = tblAssessmntLog.id_user;
                strArray2[3] = num1.ToString();
                strArray2[4] = " and  updated_date_time<='";
                strArray2[5] = tblAssessmntLog.updated_date_time.Value.ToString("yyyy-MM-dd HH:mm:00");
                strArray2[6] = "')";
                int recordCount2 = new ContentReportModel().getRecordCount(string.Concat(strArray2));
                num2 = recordCount1 - recordCount2;
              }
              entity.totoal_count = new int?(recordCount1);
              entity.completed_count = new int?(num2);
              double num3 = (double) num2 / (double) recordCount1 * 100.0;
              entity.percentage = new double?(Math.Round(num3, 2));
              entity.content_weightage = new double?(new ProgramScoringModel().getContentWeightage(tblCategory.ID_CATEGORY, entity.percentage));
              entity.log_datetime = new DateTime?(DateTime.Now);
              entity.status = "A";
              entity.updated_date_time = new DateTime?(DateTime.Now);
              this.db.sc_program_content_summary.Add(entity);
              this.db.SaveChanges();
            }
            int? assessGroup = tblAssessment.assess_group;
            num1 = 1;
            if (assessGroup.GetValueOrDefault() == num1 & assessGroup.HasValue)
            {
              string[] strArray3 = new string[9];
              strArray3[0] = "select * from tbl_rs_type_qna where id_assessment=";
              num1 = tblAssessment.id_assessment;
              strArray3[1] = num1.ToString();
              strArray3[2] = " and id_user=";
              num1 = tblAssessmntLog.id_user;
              strArray3[3] = num1.ToString();
              strArray3[4] = " and id_organization=";
              strArray3[5] = OID.ToString();
              strArray3[6] = " and attempt_number=";
              num1 = tblAssessmntLog.attempt_no;
              strArray3[7] = num1.ToString();
              strArray3[8] = " ";
              tbl_rs_type_qna tblRsTypeQna = this.db.tbl_rs_type_qna.SqlQuery(string.Concat(strArray3)).FirstOrDefault<tbl_rs_type_qna>();
              if (tblRsTypeQna != null)
              {
                this.db.sc_program_assessment_scoring.Add(new sc_program_assessment_scoring()
                {
                  id_assessment = new int?(tblAssessment.id_assessment),
                  id_user = new int?(tblAssessmntLog.id_user),
                  id_category = new int?(tblCategory.ID_CATEGORY),
                  id_organization = new int?(OID),
                  assessment_score = tblRsTypeQna.result_in_percentage,
                  assessment_wieghtage = new double?(new ProgramScoringModel().getAssessmentWeightage(tblAssessment.id_assessment, tblCategory.ID_CATEGORY, tblRsTypeQna.result_in_percentage)),
                  attempt_number = new int?(tblAssessmntLog.attempt_no),
                  log_datetime = new DateTime?(DateTime.Now),
                  status = "A",
                  updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
            }
          }
        }
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "");
    }
  }
}
