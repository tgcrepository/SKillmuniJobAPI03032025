// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EvaluateAssessmentController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
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

    public class EvaluateAssessmentController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] AssessmentRequest request)
    {
      tbl_assessment_sheet tblAssessmentSheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == request.ASID && t.status == "A")).FirstOrDefault<tbl_assessment_sheet>();
      AssessmentResponce assessmentResponce = new AssessmentResponce();
      tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
      {
        (object) tblAssessmentSheet.id_assesment
      });
      Assessment assessment = new Assessment();
      assessment.assessment_title = tblAssessment.assessment_title;
      assessment.assesment_description = tblAssessment.assesment_description;
      assessment.id_assessment = tblAssessment.id_assessment;
      assessment.id_organization = Convert.ToInt32((object) tblAssessment.id_organization);
      assessment.assess_type = tblAssessment.assess_type;
      List<Assessment> assessmentList = new List<Assessment>();
      assessmentList.Add(assessment);
      this.db.tbl_assessment_index.Add(new tbl_assessment_index()
      {
        id_assessment_sheet = tblAssessmentSheet.id_assessment_sheet,
        id_user = request.UID,
        status = "A",
        updated_date_time = new DateTime?(DateTime.Now)
      });
      this.db.SaveChanges();
      List<string> asrq = request.ASRQ;
      int attamptNo = new AssessmentModel().getAttamptNo(tblAssessmentSheet.id_assessment_sheet, request.UID);
      List<UserInput> userInputList = new List<UserInput>();
      int num = 0;
      foreach (string str in asrq)
      {
        ++num;
        UserInput userInput = new UserInput();
        string[] strArray = str.Split('|');
        tbl_assessment_question assessmentQuestion = this.db.tbl_assessment_question.Find(new object[1]
        {
          (object) Convert.ToInt32(strArray[0])
        });
        tbl_assessment_answer assessmentAnswer = this.db.tbl_assessment_answer.Find(new object[1]
        {
          (object) Convert.ToInt32(strArray[1])
        });
        userInput.Question = assessmentQuestion.assessment_question;
        userInput.Answer = assessmentAnswer.answer_description;
        userInputList.Add(userInput);
        this.db.tbl_assessment_audit.Add(new tbl_assessment_audit()
        {
          id_assessment = new int?(tblAssessmentSheet.id_assesment),
          id_assessment_question = new int?(Convert.ToInt32(strArray[0])),
          id_assessment_answer = new int?(Convert.ToInt32(strArray[1])),
          recorded_timestamp = new DateTime?(DateTime.Now),
          updated_date_time = new DateTime?(DateTime.Now),
          attempt_no = new int?(attamptNo),
          status = "A",
          id_user = new int?(request.UID)
        });
        this.db.SaveChanges();
      }
      string str1 = "";
      assessmentResponce.QuestionAnswer = userInputList;
      assessmentResponce.Message = " No Of Questions : " + num.ToString() + "|" + str1;
      assessmentResponce.Assessment = assessmentList;
      assessmentResponce.Attempt = attamptNo.ToString();
      string str2 = JsonConvert.SerializeObject((object) assessmentResponce);
      tbl_assessmnt_log tblAssessmntLog = new tbl_assessmnt_log();
      tblAssessmntLog.attempt_no = attamptNo;
      tblAssessmntLog.id_assessment_sheet = tblAssessmentSheet.id_assessment_sheet;
      tblAssessmntLog.id_user = request.UID;
      tblAssessmntLog.json_response = str2;
      tblAssessmntLog.status = "A";
      tblAssessmntLog.updated_date_time = new DateTime?(DateTime.Now);
      this.db.tbl_assessmnt_log.Add(tblAssessmntLog);
      this.db.SaveChanges();
      this.scoreAssessment(tblAssessmntLog);
      return namespace2.CreateResponse<AssessmentResponce>(this.Request, HttpStatusCode.OK, assessmentResponce);
    }

    public HttpResponseMessage GET(int UID, int ASID, string ASRQ)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      tbl_assessment_sheet tblAssessmentSheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assesment == ASID)).FirstOrDefault<tbl_assessment_sheet>();
      AssessmentResponce assessmentResponce = new AssessmentResponce();
      tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
      {
        (object) tblAssessmentSheet.id_assesment
      });
      bool flag = false;
      int? assessGroup1 = tblAssessment.assess_group;
      int num1 = 1;
      if (assessGroup1.GetValueOrDefault() == num1 & assessGroup1.HasValue)
      {
        int? ansRequiered = tblAssessment.ans_requiered;
        int num2 = 1;
        if (ansRequiered.GetValueOrDefault() == num2 & ansRequiered.HasValue)
          flag = true;
      }
      Assessment assess = new Assessment();
      assess.assessment_title = tblAssessment.assessment_title;
      assess.assesment_description = tblAssessment.answer_description;
      assess.id_assessment = tblAssessment.id_assessment;
      assess.id_organization = Convert.ToInt32((object) tblAssessment.id_organization);
      assess.assess_type = tblAssessment.assess_type;
      List<Assessment> assessmentList = new List<Assessment>();
      assessmentList.Add(assess);
      List<tbl_assessment_question> list1 = this.db.tbl_assessment_question.Where<tbl_assessment_question>((Expression<Func<tbl_assessment_question, bool>>) (t => t.status == "A" && t.id_assessment == (int?) assess.id_assessment)).ToList<tbl_assessment_question>();
      this.db.tbl_assessment_index.Add(new tbl_assessment_index()
      {
        id_assessment_sheet = tblAssessmentSheet.id_assessment_sheet,
        id_user = UID,
        status = "A",
        updated_date_time = new DateTime?(DateTime.Now)
      });
      this.db.SaveChanges();
      string[] strArray1 = ASRQ.Split(';');
      int attamptNo = new AssessmentModel().getAttamptNo(tblAssessmentSheet.id_assessment_sheet, UID);
      List<UserInput> userInputList = new List<UserInput>();
      int num3 = 0;
      List<DataCube> source = new List<DataCube>();
      int? assessGroup2 = tblAssessment.assess_group;
      int num4 = 4;
      if (assessGroup2.GetValueOrDefault() == num4 & assessGroup2.HasValue)
      {
        foreach (string str in strArray1)
        {
          DataCube dataCube = new DataCube();
          string[] strArray2 = str.Split('|');
          dataCube.QID = strArray2[0];
          dataCube.AID = strArray2[1];
          dataCube.VAL = strArray2[2];
          source.Add(dataCube);
          this.db.tbl_assessment_audit.Add(new tbl_assessment_audit()
          {
            id_assessment = new int?(tblAssessmentSheet.id_assesment),
            id_assessment_question = new int?(Convert.ToInt32(strArray2[0])),
            id_assessment_answer = new int?(Convert.ToInt32(strArray2[1])),
            value_sent = new int?(Convert.ToInt32(strArray2[2])),
            recorded_timestamp = new DateTime?(DateTime.Now),
            updated_date_time = new DateTime?(DateTime.Now),
            attempt_no = new int?(attamptNo),
            status = "A",
            id_user = new int?(UID)
          });
          this.db.SaveChanges();
        }
        List<List<DataCube>> dataCubeListList = new List<List<DataCube>>();
        foreach (tbl_assessment_question assessmentQuestion in list1)
        {
          tbl_assessment_question item = assessmentQuestion;
          List<DataCube> dataCubeList = new List<DataCube>();
          List<DataCube> list2 = source.Where<DataCube>((Func<DataCube, bool>) (t => t.QID == item.id_assessment_question.ToString())).ToList<DataCube>();
          dataCubeListList.Add(list2);
        }
        foreach (List<DataCube> dataCubeList in dataCubeListList)
        {
          ++num3;
          UserInput userInput = new UserInput();
          userInput.WANS = "";
          tbl_assessment_question assessmentQuestion = this.db.tbl_assessment_question.Find(new object[1]
          {
            (object) Convert.ToInt32(dataCubeList[0].QID)
          });
          userInput.Question = "Q . " + assessmentQuestion.assessment_question;
          tbl_assessment_answer assessmentAnswer1 = new tbl_assessment_answer();
          List<string> values = new List<string>();
          foreach (DataCube dataCube in dataCubeList)
          {
            tbl_assessment_answer assessmentAnswer2 = this.db.tbl_assessment_answer.Find(new object[1]
            {
              (object) Convert.ToInt32(dataCube.AID)
            });
            values.Add(assessmentAnswer2.answer_description + " [ " + dataCube.VAL + " ]");
          }
          userInput.Answer = string.Join("\n", (IEnumerable<string>) values);
          userInputList.Add(userInput);
        }
      }
      else
      {
        foreach (string str in strArray1)
        {
          ++num3;
          UserInput userInput = new UserInput();
          userInput.WANS = "";
          string[] strArray3 = str.Split('|');
          tbl_assessment_question assessmentQuestion = this.db.tbl_assessment_question.Find(new object[1]
          {
            (object) Convert.ToInt32(strArray3[0])
          });
          userInput.Question = "Q . " + assessmentQuestion.assessment_question;
          tbl_assessment_answer assessmentAnswer3 = new tbl_assessment_answer();
          int int32 = Convert.ToInt32(assessmentQuestion.aq_answer);
          if (flag && !string.IsNullOrEmpty(assessmentQuestion.aq_answer) && int32 > 0)
          {
            tbl_assessment_answer assessmentAnswer4 = this.db.tbl_assessment_answer.Find(new object[1]
            {
              (object) int32
            });
            userInput.WANS = assessmentAnswer4.answer_description;
          }
          if (strArray3[1] == "0")
          {
            userInput.Answer = " Response Value : " + strArray3[2];
          }
          else
          {
            tbl_assessment_answer assessmentAnswer5 = this.db.tbl_assessment_answer.Find(new object[1]
            {
              (object) Convert.ToInt32(strArray3[1])
            });
            userInput.Answer = "-" + assessmentAnswer5.answer_description;
            assessGroup2 = tblAssessment.assess_group;
            int num5 = 4;
            if (assessGroup2.GetValueOrDefault() == num5 & assessGroup2.HasValue)
              userInput.Answer = "-" + assessmentAnswer5.answer_description + " [ " + strArray3[2] + " ]";
          }
          userInputList.Add(userInput);
          this.db.tbl_assessment_audit.Add(new tbl_assessment_audit()
          {
            id_assessment = new int?(tblAssessmentSheet.id_assesment),
            id_assessment_question = new int?(Convert.ToInt32(strArray3[0])),
            id_assessment_answer = new int?(Convert.ToInt32(strArray3[1])),
            value_sent = new int?(Convert.ToInt32(strArray3[2])),
            recorded_timestamp = new DateTime?(DateTime.Now),
            updated_date_time = new DateTime?(DateTime.Now),
            attempt_no = new int?(attamptNo),
            status = "A",
            id_user = new int?(UID)
          });
          this.db.SaveChanges();
        }
      }
      string str1 = new AssessmentModel().EveluateAssessment(ASID, UID, attamptNo);
      assessmentResponce.QuestionAnswer = userInputList;
      assessmentResponce.Message = "Assessment Summary |Number Of questions         : " + num3.ToString() + "|" + str1 + "|Assessment Details";
      assessmentResponce.Assessment = assessmentList;
      assessmentResponce.Attempt = attamptNo.ToString();
      string str2 = JsonConvert.SerializeObject((object) assessmentResponce);
      tbl_assessmnt_log tblAssessmntLog = new tbl_assessmnt_log();
      tblAssessmntLog.attempt_no = attamptNo;
      tblAssessmntLog.id_organization = tblAssessment.id_organization;
      tblAssessmntLog.id_assessment_sheet = tblAssessmentSheet.id_assessment_sheet;
      tblAssessmntLog.id_user = UID;
      tblAssessmntLog.json_response = str2;
      tblAssessmntLog.status = "A";
      tblAssessmntLog.updated_date_time = new DateTime?(DateTime.Now);
      this.db.tbl_assessmnt_log.Add(tblAssessmntLog);
      this.db.SaveChanges();
      this.scoreAssessment(tblAssessmntLog);
      return namespace2.CreateResponse<AssessmentResponce>(this.Request, HttpStatusCode.OK, assessmentResponce);
    }

    private void scoreAssessment(tbl_assessmnt_log log)
    {
      int num1 = 0;
      if (this.db.tbl_rs_type_qna.Where<tbl_rs_type_qna>((Expression<Func<tbl_rs_type_qna, bool>>) (t => t.id_assessment_log == (int?) log.id_assessmnt_log)).FirstOrDefault<tbl_rs_type_qna>() != null)
        return;
      tbl_assessment_sheet sheet = this.db.tbl_assessment_sheet.Where<tbl_assessment_sheet>((Expression<Func<tbl_assessment_sheet, bool>>) (t => t.id_assessment_sheet == log.id_assessment_sheet)).FirstOrDefault<tbl_assessment_sheet>();
      tbl_assessment tblAssessment = this.db.tbl_assessment.Find(new object[1]
      {
        (object) sheet.id_assesment
      });
      int? idAssessmentTheme = sheet.id_assessment_theme;
      int num2 = 1;
      if (idAssessmentTheme.GetValueOrDefault() == num2 & idAssessmentTheme.HasValue)
      {
        int num3 = num1 + 1;
        tbl_rs_type_qna entity = new tbl_rs_type_qna();
        entity.id_user = new int?(log.id_user);
        entity.id_assessment = new int?(tblAssessment.id_assessment);
        entity.id_assessment_log = new int?(log.id_assessmnt_log);
        entity.id_assessment_sheet = new int?(sheet.id_assessment_sheet);
        entity.id_organization = tblAssessment.id_organization;
        entity.attempt_number = new int?(log.attempt_no);
        entity.status = "A";
        entity.updated_date_time = new DateTime?(DateTime.Now);
        List<tbl_assessment_audit> list = this.db.tbl_assessment_audit.Where<tbl_assessment_audit>((Expression<Func<tbl_assessment_audit, bool>>) (t => t.id_assessment == (int?) sheet.id_assesment && t.attempt_no == (int?) log.attempt_no && t.id_user == (int?) log.id_user)).ToList<tbl_assessment_audit>();
        int num4 = 0;
        int num5 = 0;
        double num6 = 0.0;
        foreach (tbl_assessment_audit tblAssessmentAudit in list)
        {
          if (this.db.tbl_assessment_question.Find(new object[1]
          {
            (object) tblAssessmentAudit.id_assessment_question
          }).aq_answer == this.db.tbl_assessment_answer.Find(new object[1]
          {
            (object) tblAssessmentAudit.id_assessment_answer
          }).id_assessment_answer.ToString())
            ++num4;
          ++num5;
        }
        if (num4 != 0)
          num6 = Math.Round((double) num4 / (double) list.Count * 100.0, 2);
        entity.total_question = new int?(num5);
        entity.right_answer_count = new int?(num4);
        entity.wrong_answer_count = new int?(num5 - num4);
        entity.result_in_percentage = new double?(num6);
        this.db.tbl_rs_type_qna.Add(entity);
        this.db.SaveChanges();
      }
      else
      {
        idAssessmentTheme = sheet.id_assessment_theme;
        int num7 = 2;
        if (idAssessmentTheme.GetValueOrDefault() == num7 & idAssessmentTheme.HasValue)
          return;
        idAssessmentTheme = sheet.id_assessment_theme;
        int num8 = 3;
        if (idAssessmentTheme.GetValueOrDefault() == num8 & idAssessmentTheme.HasValue)
          return;
        idAssessmentTheme = sheet.id_assessment_theme;
        int num9 = 4;
        int num10 = idAssessmentTheme.GetValueOrDefault() == num9 & idAssessmentTheme.HasValue ? 1 : 0;
      }
    }

    private void program_scoring(int aid, int attempt, int uid, int oid)
    {
      sc_game_element_weightage elementWeightage = new sc_game_element_weightage();
      foreach (tbl_category tblCategory in this.db.tbl_category.SqlQuery("select * from tbl_category where id_organization=" + oid.ToString() + " and id_category in (select distinct id_category from tbl_assessment_categoty_mapping where id_assessment=" + aid.ToString() + " )").ToList<tbl_category>())
      {
        if (new ProgramScoringModel().checkProgramComplition(tblCategory.ID_CATEGORY, uid, oid) == "0")
        {
          sc_program_content_summary entity = new sc_program_content_summary();
          entity.id_category = new int?(tblCategory.ID_CATEGORY);
          entity.id_organization = new int?(oid);
          entity.id_user = new int?(uid);
          int recordCount1 = new ContentReportModel().getRecordCount("select count(*) count from tbl_content_organization_mapping where id_category=" + tblCategory.ID_CATEGORY.ToString());
          int num1 = 0;
          if (recordCount1 > 0)
          {
            int recordCount2 = new ContentReportModel().getRecordCount("select count(*) count from tbl_content_organization_mapping where id_category=" + tblCategory.ID_CATEGORY.ToString() + " and id_content not in (select distinct id_content from tbl_content_counters where id_user=" + uid.ToString() + ")");
            num1 = recordCount1 - recordCount2;
          }
          entity.totoal_count = new int?(recordCount1);
          entity.completed_count = new int?(num1);
          double num2 = (double) num1 / (double) recordCount1 * 100.0;
          entity.percentage = new double?(Math.Round(num2, 2));
          entity.content_weightage = new double?(new ProgramScoringModel().getContentWeightage(tblCategory.ID_CATEGORY, entity.percentage));
          entity.log_datetime = new DateTime?(DateTime.Now);
          entity.status = "A";
          entity.updated_date_time = new DateTime?(DateTime.Now);
          this.db.sc_program_content_summary.Add(entity);
          this.db.SaveChanges();
          int? assessGroup = this.db.tbl_assessment.Find(new object[1]
          {
            (object) aid
          }).assess_group;
          int num3 = 1;
          if (assessGroup.GetValueOrDefault() == num3 & assessGroup.HasValue)
          {
            tbl_rs_type_qna tblRsTypeQna = this.db.tbl_rs_type_qna.SqlQuery("select * from tbl_rs_type_qna where id_assessment=" + aid.ToString() + " and id_user=" + uid.ToString() + " and id_organization=" + oid.ToString() + " and attempt_number=" + attempt.ToString() + " ").FirstOrDefault<tbl_rs_type_qna>();
            if (tblRsTypeQna != null)
            {
              this.db.sc_program_assessment_scoring.Add(new sc_program_assessment_scoring()
              {
                id_assessment = new int?(aid),
                id_user = new int?(uid),
                id_category = new int?(tblCategory.ID_CATEGORY),
                id_organization = new int?(oid),
                assessment_score = tblRsTypeQna.result_in_percentage,
                assessment_wieghtage = new double?(new ProgramScoringModel().getAssessmentWeightage(aid, tblCategory.ID_CATEGORY, tblRsTypeQna.result_in_percentage)),
                attempt_number = new int?(attempt),
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
  }
}
