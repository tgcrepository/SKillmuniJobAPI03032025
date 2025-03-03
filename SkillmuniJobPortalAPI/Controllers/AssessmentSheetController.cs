// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.AssessmentSheetController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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

    public class AssessmentSheetController : ApiController
  {
    private APIRESPONSE responce = new APIRESPONSE();
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int ASID, int UID, int OID)
    {
      DateTime now = DateTime.Now;
      tbl_assessment_sheet sheets = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == ASID && t.status == "A")).FirstOrDefault<tbl_assessment_sheet>();
      if (sheets != null)
      {
        tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => t.id_assessment == sheets.id_assesment && t.status == "A")).FirstOrDefault<tbl_assessment>();
        if (tblAssessment != null)
        {
          DateTime t1_1 = tblAssessment.assess_ended.Value;
          if (t1_1.ToString("HH:mm") == "00:00")
            t1_1 = t1_1.AddDays(1.0);
          bool flag = DateTime.Compare(t1_1, now) > 0;
          if (!flag)
          {
            this.responce.KEY = "FAILURE";
            this.responce.MESSAGE = "The assessment has expired ...";
            return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, this.responce);
          }
          tbl_assessment_user_assignment assessmentUserAssignment = this.db.tbl_assessment_user_assignment.SqlQuery("select distinct * from tbl_assessment_user_assignment where id_organization=" + OID.ToString() + " AND id_user=" + UID.ToString() + " AND id_assessment=" + tblAssessment.id_assessment.ToString()).FirstOrDefault<tbl_assessment_user_assignment>();
          if (assessmentUserAssignment != null)
          {
            DateTime t1_2 = assessmentUserAssignment.expire_date.Value;
            if (t1_2.ToString("HH:mm") == "00:00")
              t1_2 = t1_2.AddDays(1.0);
            flag = DateTime.Compare(t1_2, now) > 0;
          }
          if (flag)
          {
            int attamptNo = new AssessmentModel().getAttamptNo(sheets.id_assessment_sheet, UID);
            int? totalAttempt = tblAssessment.total_attempt;
            int valueOrDefault = totalAttempt.GetValueOrDefault();
            if (attamptNo < valueOrDefault & totalAttempt.HasValue)
            {
              List<QuestionAnswer> questionAnswerList = new List<QuestionAnswer>();
              DbSet<tbl_assessment_question> assessmentQuestion1 = this.db.tbl_assessment_question;
              Expression<Func<tbl_assessment_question, bool>> predicate = (Expression<Func<tbl_assessment_question, bool>>) (t => t.id_assessment == (int?) sheets.id_assesment && t.status == "A");
              foreach (tbl_assessment_question assessmentQuestion2 in assessmentQuestion1.Where<tbl_assessment_question>(predicate).ToList<tbl_assessment_question>())
              {
                tbl_assessment_question item = assessmentQuestion2;
                QuestionAnswer questionAnswer = new QuestionAnswer()
                {
                  AssessmenQuestion = new List<AssessmentQuestion>(),
                  AssessmentOption = new List<AssessmentOption>()
                };
                questionAnswer.AssessmenQuestion.Add(new AssessmentQuestion()
                {
                  id_assessment_question = item.id_assessment_question,
                  id_organization = Convert.ToInt32((object) item.id_organization),
                  assessment_question = item.assessment_question,
                  question_image = string.IsNullOrEmpty(item.question_image) ? "" : ConfigurationManager.AppSettings["ANSIMAGE"].ToString() + "Assessment/" + item.question_image,
                  aq_answer = ""
                });
                List<tbl_assessment_answer> list = this.db.tbl_assessment_answer.Where<tbl_assessment_answer>((Expression<Func<tbl_assessment_answer, bool>>) (t => t.id_assessment_question == (int?) item.id_assessment_question && t.status == "A")).ToList<tbl_assessment_answer>();
                List<AssessmentOption> assessmentOptionList = new List<AssessmentOption>();
                foreach (tbl_assessment_answer assessmentAnswer in list)
                  assessmentOptionList.Add(new AssessmentOption()
                  {
                    id_assessment_answer = assessmentAnswer.id_assessment_answer,
                    id_assessment_question = item.id_assessment_question,
                    answer_description = assessmentAnswer.answer_description
                  });
                questionAnswer.AssessmentOption = assessmentOptionList;
                questionAnswerList.Add(questionAnswer);
              }
              Assessment assessment = new Assessment();
              assessment.assessment_title = tblAssessment.assessment_title;
              assessment.assesment_description = tblAssessment.assesment_description;
              assessment.id_assessment = tblAssessment.id_assessment;
              assessment.id_organization = Convert.ToInt32((object) tblAssessment.id_organization);
              assessment.assess_type = tblAssessment.assess_group.ToString();
              int? assessGroup = tblAssessment.assess_group;
              int num = 3;
              if (assessGroup.GetValueOrDefault() == num & assessGroup.HasValue)
              {
                assessment.low_title = tblAssessment.lower_title;
                assessment.low_value = tblAssessment.lower_value;
                assessment.high_title = tblAssessment.high_title;
                assessment.high_value = tblAssessment.high_value;
              }
              else
              {
                assessment.low_title = "";
                assessment.low_value = "";
                assessment.high_title = "";
                assessment.high_value = "";
              }
              List<Assessment> assessmentList = new List<Assessment>();
              assessmentList.Add(assessment);
              AssessmentSheet assessmentSheet = new AssessmentSheet();
              assessmentSheet.Assessment = assessmentList;
              assessmentSheet.QuestionAnswer = questionAnswerList;
              assessmentSheet.THEME = Convert.ToInt32((object) sheets.id_assessment_theme);
              this.responce.KEY = "SUCCESS";
              this.responce.MESSAGE = JsonConvert.SerializeObject((object) assessmentSheet);
            }
            else
            {
              this.responce.KEY = "FAILURE";
              this.responce.MESSAGE = "No of attempt exceed";
            }
          }
          else
          {
            this.responce.KEY = "FAILURE";
            this.responce.MESSAGE = "The assessment has expired ";
          }
        }
        else
        {
          this.responce.KEY = "FAILURE";
          this.responce.MESSAGE = "Assessment is not found.please contact admin...";
        }
      }
      else
      {
        this.responce.KEY = "FAILURE";
        this.responce.MESSAGE = "Invalid Assessmebnt sheet..";
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, this.responce);
    }
  }
}
