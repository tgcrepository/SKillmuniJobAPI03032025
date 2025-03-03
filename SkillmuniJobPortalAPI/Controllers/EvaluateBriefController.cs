// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EvaluateBriefController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

    public class EvaluateBriefController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(
      int OID,
      int UID,
      int BID,
      string BRF,
      string ASRQ,
      int AcademicTileId)
    {
      BRF = new Utility().mysqlTrim(BRF.ToString());
      ASRQ = new Utility().mysqlTrim(ASRQ.ToString());
      List<BriefDataCube> briefDataCubeList = new List<BriefDataCube>();
      List<BriefUserInput> briefUserInputList = new List<BriefUserInput>();
      BriefReturnResponse briefReturnResponse = new BriefReturnResponse();
      List<ComplexityResult> complexityResultList = new List<ComplexityResult>();
      List<tbl_brief_question_complexity> list1 = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.status == "A")).ToList<tbl_brief_question_complexity>();
      int num1 = 0;
      tbl_brief_master brief = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == BID)).FirstOrDefault<tbl_brief_master>();
      if (brief == null)
        return namespace2.CreateResponse<BriefReturnResponse>(this.Request, HttpStatusCode.NoContent, briefReturnResponse);
      List<string> list2 = ((IEnumerable<string>) ASRQ.Split(';')).ToList<string>();
      int num2 = list2.Count<string>();
      if (num2 > 0)
      {
        int num3 = new BriefModel().getAttamptNo("SELECT count(*) subcount FROM tbl_brief_index where id_user=" + UID.ToString() + " and id_brief_master=" + brief.id_brief_master.ToString() + " ") + 1;
        this.db.tbl_brief_index.Add(new tbl_brief_index()
        {
          id_brief_master = brief.id_brief_master,
          id_user = UID,
          status = "A",
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
        foreach (string str1 in list2)
        {
          char[] chArray = new char[1]{ '|' };
          string[] strArray = str1.Split(chArray);
          string str2 = strArray[0];
          if (str2 != null)
          {
            BriefDataCube temp = new BriefDataCube();
            temp.QID = Convert.ToInt32(str2);
            temp.AID = Convert.ToInt32(strArray[1]);
            temp.VAL = "NA";
            briefDataCubeList.Add(temp);
            tbl_brief_question tblBriefQuestion = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>) (t => t.id_brief_question == temp.QID)).FirstOrDefault<tbl_brief_question>();
            bool flag = false;
            tbl_brief_audit entity = new tbl_brief_audit();
            entity.attempt_no = new int?(num3);
            entity.id_brief_master = new int?(brief.id_brief_master);
            entity.id_brief_question = new int?(temp.QID);
            entity.id_brief_answer = new int?(temp.AID);
            entity.id_user = new int?(UID);
            entity.recorded_timestamp = new DateTime?(DateTime.Now);
            entity.status = "A";
            entity.question_complexity = tblBriefQuestion.question_complexity;
            entity.updated_date_time = new DateTime?(DateTime.Now);
            entity.value_sent = new int?(0);
            tbl_brief_answer tblBriefAnswer = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>) (t => t.id_brief_answer == temp.AID)).FirstOrDefault<tbl_brief_answer>();
            if (tblBriefAnswer != null)
            {
              int? isCorrectAnswer = tblBriefAnswer.is_correct_answer;
              int num4 = 1;
              if (isCorrectAnswer.GetValueOrDefault() == num4 & isCorrectAnswer.HasValue)
              {
                flag = true;
                entity.audit_result = new int?(1);
              }
              else
                entity.audit_result = new int?(0);
            }
            else
              entity.audit_result = new int?(0);
            entity.id_organization = new int?(OID);
            this.db.tbl_brief_audit.Add(entity);
            this.db.SaveChanges();
            if (flag && num3 == 1)
            {
              this.db.tbl_brief_b2c_right_audit.Add(new tbl_brief_b2c_right_audit()
              {
                data_index = new int?(0),
                datetime_stamp = new DateTime?(DateTime.Now),
                id_brief_question = new int?(temp.QID),
                id_organization = new int?(OID),
                question_complexity = tblBriefQuestion.question_complexity,
                value_count = new int?(1),
                status = "A",
                updated_date_time = new DateTime?(DateTime.Now),
                id_user = new int?(UID)
              });
              this.db.SaveChanges();
            }
          }
        }
        int num5 = 1;
        foreach (BriefDataCube briefDataCube in briefDataCubeList)
        {
          BriefDataCube item = briefDataCube;
          BriefUserInput briefUserInput = new BriefUserInput();
          tbl_brief_question qtn = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>) (t => t.id_brief_question == item.QID)).FirstOrDefault<tbl_brief_question>();
          if (qtn != null)
          {
            tbl_brief_answer tblBriefAnswer1 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>) (t => t.id_brief_answer == item.AID)).FirstOrDefault<tbl_brief_answer>();
            tbl_brief_answer tblBriefAnswer2 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>) (t => t.id_brief_question == (int?) item.QID && t.is_correct_answer == (int?) 1)).FirstOrDefault<tbl_brief_answer>();
            int? isCorrectAnswer = tblBriefAnswer1.is_correct_answer;
            int num6 = 1;
            if (isCorrectAnswer.GetValueOrDefault() == num6 & isCorrectAnswer.HasValue)
            {
              ++num1;
              briefUserInput.is_right = 1;
            }
            else
              briefUserInput.is_right = 0;
            briefUserInput.Question = "Q. " + qtn.brief_question;
            briefUserInput.Answer = "A. " + tblBriefAnswer1.brief_answer;
            briefUserInput.id_answer = tblBriefAnswer1.id_brief_answer;
            briefUserInput.id_question = tblBriefAnswer1.id_brief_question;
            briefUserInput.WANS = tblBriefAnswer2.brief_answer;
            briefUserInput.id_wans = tblBriefAnswer2.id_brief_answer;
            tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == qtn.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
            if (questionComplexity != null)
            {
              briefUserInput.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
              briefUserInput.question_complexity_label = questionComplexity.question_complexity_label;
            }
            briefUserInput.srno = num5++;
            briefUserInputList.Add(briefUserInput);
          }
        }
        foreach (tbl_brief_question_complexity questionComplexity in list1)
        {
          ComplexityResult complexityResult = new BriefModel().getComplexityResult("SELECT CASE WHEN COUNT(*) > 0 THEN COUNT(*) ELSE 0 END totalcount, CASE WHEN SUM(audit_result) > 0 THEN SUM(audit_result) ELSE 0 END rightcount FROM tbl_brief_audit  WHERE id_user = " + UID.ToString() + " AND id_brief_master = " + brief.id_brief_master.ToString() + " AND attempt_no = 1 AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE question_complexity = " + questionComplexity.question_complexity.ToString() + ")");
          if (complexityResult.TOTALCOUNT > 0)
          {
            complexityResult.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
            complexityResult.question_complexity_label = questionComplexity.question_complexity_label;
            double num7 = Math.Round((double) (complexityResult.RIGHTCOUNT * 100 / complexityResult.TOTALCOUNT), 2);
            complexityResult.RESULT = num7;
            complexityResultList.Add(complexityResult);
          }
        }
        briefReturnResponse.complexity = complexityResultList;
        briefReturnResponse.briefReturn = briefUserInputList;
        briefReturnResponse.returnStat = "You have answered " + num1.ToString() + " out of " + num2.ToString() + " question right ";
        briefReturnResponse.rightCount = num1;
        briefReturnResponse.totalCount = num2;
        briefReturnResponse.attemptno = num3;
        double num8 = 0.0;
        if (num1 > 0 && num2 > 0)
          num8 = Math.Round((double) (num1 * 100 / num2), 2);
        briefReturnResponse.percentage = num8;
        if (num3 == 1)
        {
          this.db.tbl_brief_b2c_score_audit.Add(new tbl_brief_b2c_score_audit()
          {
            data_index = new int?(0),
            datetime_stamp = new DateTime?(DateTime.Now),
            id_brief_master = new int?(brief.id_brief_master),
            id_organization = new int?(OID),
            value_count = new int?(Convert.ToInt32(num8)),
            status = "A",
            updated_date_time = new DateTime?(DateTime.Now),
            id_user = new int?(UID)
          });
          this.db.SaveChanges();
        }
        tbl_brief_log entity1 = new tbl_brief_log();
        entity1.attempt_no = num3;
        entity1.id_brief_master = brief.id_brief_master;
        entity1.id_organization = new int?(OID);
        entity1.id_user = UID;
        entity1.brief_result = new double?(num8);
        entity1.status = "A";
        entity1.updated_date_time = new DateTime?(DateTime.Now);
        string str = JsonConvert.SerializeObject((object) briefReturnResponse);
        entity1.json_response = str;
        this.db.tbl_brief_log.Add(entity1);
        this.db.SaveChanges();
      }
      tbl_brief_read_status tblBriefReadStatus = this.db.tbl_brief_read_status.Where<tbl_brief_read_status>((Expression<Func<tbl_brief_read_status, bool>>) (t => t.id_user == (int?) UID && t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_read_status>();
      if (tblBriefReadStatus != null)
      {
        int? actionStatus = tblBriefReadStatus.action_status;
        int num9 = 0;
        if (actionStatus.GetValueOrDefault() == num9 & actionStatus.HasValue)
        {
          tblBriefReadStatus.read_status = new int?(1);
          tblBriefReadStatus.read_datetime = new DateTime?(DateTime.Now);
          tblBriefReadStatus.action_status = new int?(1);
          tblBriefReadStatus.action_dateime = new DateTime?(DateTime.Now);
          tblBriefReadStatus.updated_date_time = new DateTime?(DateTime.Now);
          this.db.SaveChanges();
        }
      }
      else
      {
        this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
        {
          id_user = new int?(UID),
          id_organization = new int?(OID),
          id_brief_master = new int?(brief.id_brief_master),
          read_status = new int?(1),
          status = "A",
          action_dateime = new DateTime?(DateTime.Now),
          action_status = new int?(1),
          read_datetime = new DateTime?(DateTime.Now),
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
      }
      return namespace2.CreateResponse<BriefReturnResponse>(this.Request, HttpStatusCode.OK, briefReturnResponse);
    }

    public void setRightAnswer()
    {
      DbSet<tbl_brief_audit> tblBriefAudit1 = this.db.tbl_brief_audit;
      Expression<Func<tbl_brief_audit, bool>> predicate1 = (Expression<Func<tbl_brief_audit, bool>>) (t => t.attempt_no == (int?) 1 && t.audit_result == (int?) 1);
      foreach (tbl_brief_audit tblBriefAudit2 in tblBriefAudit1.Where<tbl_brief_audit>(predicate1).ToList<tbl_brief_audit>())
      {
        tbl_brief_b2c_right_audit entity = new tbl_brief_b2c_right_audit()
        {
          data_index = new int?(0),
          datetime_stamp = tblBriefAudit2.recorded_timestamp,
          id_brief_question = tblBriefAudit2.id_brief_question,
          id_organization = tblBriefAudit2.id_organization,
          id_user = tblBriefAudit2.id_user,
          question_complexity = tblBriefAudit2.question_complexity,
          status = "A",
          updated_date_time = tblBriefAudit2.updated_date_time,
          value_count = new int?(1)
        };
        entity.data_index = new int?(0);
        this.db.tbl_brief_b2c_right_audit.Add(entity);
        this.db.SaveChanges();
      }
      DbSet<tbl_brief_log> tblBriefLog1 = this.db.tbl_brief_log;
      Expression<Func<tbl_brief_log, bool>> predicate2 = (Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1);
      foreach (tbl_brief_log tblBriefLog2 in tblBriefLog1.Where<tbl_brief_log>(predicate2).ToList<tbl_brief_log>())
      {
        this.db.tbl_brief_b2c_score_audit.Add(new tbl_brief_b2c_score_audit()
        {
          data_index = new int?(0),
          datetime_stamp = tblBriefLog2.updated_date_time,
          value_count = new int?(Convert.ToInt32((object) tblBriefLog2.brief_result)),
          id_brief_master = new int?(tblBriefLog2.id_brief_master),
          id_organization = tblBriefLog2.id_organization,
          id_user = new int?(tblBriefLog2.id_user),
          status = "A",
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
        this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
        {
          id_brief_master = new int?(tblBriefLog2.id_brief_master),
          id_organization = tblBriefLog2.id_organization,
          id_user = new int?(tblBriefLog2.id_user),
          read_status = new int?(1),
          read_datetime = tblBriefLog2.updated_date_time,
          action_status = new int?(1),
          action_dateime = tblBriefLog2.updated_date_time,
          status = "A",
          updated_date_time = new DateTime?(DateTime.Now)
        });
        this.db.SaveChanges();
      }
    }
  }
}
