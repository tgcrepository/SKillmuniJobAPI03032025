// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EvaluateCEvaluationController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    [EnableCors("*", "*", "*")]
  public class EvaluateCEvaluationController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] CERequest data)
    {
      data.CRF = new Utility().mysqlTrim(data.CRF.ToString());
      List<CEUserInput> ceUserInputList = new List<CEUserInput>();
      CEReturnResponse ceReturnResponse = new CEReturnResponse();
      List<ComplexityResult> complexityResultList = new List<ComplexityResult>();
      List<tbl_brief_question_complexity> list1 = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.status == "A")).ToList<tbl_brief_question_complexity>();
      int num1 = 0;
      int num2 = 0;
      tbl_ce_career_evaluation_master evaluationMaster = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where lower(career_evaluation_code)=lower('" + data.CRF + "') and id_organization=" + data.OID.ToString() + " and status='A' limit 1").FirstOrDefault<tbl_ce_career_evaluation_master>();
      int num3 = 0;
      int cindex = 0;
      bool flag1 = false;
      string str1 = "00:00";
      if (evaluationMaster == null)
        return namespace2.CreateResponse<CEReturnResponse>(this.Request, HttpStatusCode.NoContent, ceReturnResponse);
      int count = data.ASRQ.Count;
      if (count > 0)
      {
        tbl_ce_evaluation_index ceEvaluationIndex1 = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index where id_user=" + data.UID.ToString() + " and id_ce_career_evaluation_master=" + evaluationMaster.id_ce_career_evaluation_master.ToString() + " order by id_ce_evaluation_index desc limit 1").FirstOrDefault<tbl_ce_evaluation_index>();
        if (ceEvaluationIndex1 != null)
        {
          num3 = ceEvaluationIndex1.attempt_no;
          cindex = num3;
        }
        flag1 = this.checkAttemptCompilation(evaluationMaster.id_ce_evaluation_tile, data.UID, data.OID, cindex);
        int num4 = 0;
        tbl_ce_evaluation_index ceEvaluationIndex2 = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user = " + data.UID.ToString() + " AND id_organization =  " + data.OID.ToString() + " ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
        if (ceEvaluationIndex2 != null)
          num4 = ceEvaluationIndex2.attempt_no;
        if (num4 == num3)
          ++num3;
        if (num4 > num3)
          num3 = num4;
        if (data.CETime != null)
          str1 = data.CETime;
        this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_index (ce_evaluation_token,id_ce_career_evaluation_master, id_user, id_organization, attempt_no, dated_time_stamp,cetimespan, status, updated_date_time) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object) data.TOKEN, (object) evaluationMaster.id_ce_career_evaluation_master, (object) data.UID, (object) data.OID, (object) num3, (object) DateTime.Now, (object) str1, (object) 'A', (object) DateTime.Now);
        foreach (CEASRQ ceasrq in data.ASRQ)
        {
          tbl_brief_question tblBriefQuestion = this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_question=" + ceasrq.QID.ToString() + " and status='A' ").FirstOrDefault<tbl_brief_question>();
          bool flag2 = false;
          int? nullable = evaluationMaster.ce_assessment_type;
          int num5 = 1;
          if (nullable.GetValueOrDefault() == num5 & nullable.HasValue)
          {
            tbl_brief_answer tblBriefAnswer = this.db.Database.SqlQuery<tbl_brief_answer>("select * from tbl_brief_answer where id_brief_answer=" + ceasrq.ANS.ToString() + " and status='A' ").FirstOrDefault<tbl_brief_answer>();
            int num6 = 0;
            int num7;
            if (tblBriefAnswer != null)
            {
              nullable = tblBriefAnswer.is_correct_answer;
              int num8 = 1;
              if (nullable.GetValueOrDefault() == num8 & nullable.HasValue)
              {
                flag2 = true;
                num7 = 1;
                num6 = Convert.ToInt32((object) evaluationMaster.job_points_for_ra);
              }
              else
                num7 = 0;
            }
            else
              num7 = 0;
            this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_audit (id_ce_career_evaluation_master,ce_evaluation_token, id_organization, id_user, id_brief_question, question_complexity, id_brief_answer, value_sent, attempt_no, recorded_timestamp, audit_result,job_point,id_ce_evalution_answer_key, status, updated_date_time) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14})", (object) evaluationMaster.id_ce_career_evaluation_master, (object) data.TOKEN, (object) data.OID, (object) data.UID, (object) tblBriefQuestion.id_brief_question, (object) tblBriefQuestion.question_complexity, (object) ceasrq.ANS, (object) ceasrq.VAL, (object) num3, (object) DateTime.Now, (object) num7, (object) num6, (object) 0, (object) 'A', (object) DateTime.Now);
            if (this.db.Database.SqlQuery<int>("select id_job_log from tbl_job_user_log where id_user={0} and id_job={1}", (object) data.UID, (object) data.ID_JOB).FirstOrDefault<int>() > 0)
              this.db.Database.ExecuteSqlCommand("update  tbl_job_user_log set  status ='A' where id_user={0} and id_job={1}", (object) data.UID, (object) data.ID_JOB);
            else
              this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_job_user_log (id_user,id_job, status, updated_date_time, id_org) VALUES ({0},{1},{2},{3},{4})", (object) data.UID, (object) data.ID_JOB, (object) "A", (object) DateTime.Now, (object) data.OID);
          }
          nullable = evaluationMaster.ce_assessment_type;
          int num9 = 2;
          if (nullable.GetValueOrDefault() == num9 & nullable.HasValue)
          {
            List<tbl_brief_answer> list2 = this.db.Database.SqlQuery<tbl_brief_answer>("select * from tbl_brief_answer where id_brief_question=" + ceasrq.QID.ToString() + " and status='A' ").ToList<tbl_brief_answer>();
            List<TEMPANS> source = new List<TEMPANS>();
            foreach (tbl_brief_answer tblBriefAnswer in list2)
            {
              TEMPANS tempans = new TEMPANS();
              tempans.AID = tblBriefAnswer.id_brief_answer;
              if (ceasrq.ANS == tblBriefAnswer.id_brief_answer)
                tempans.SVAL = Convert.ToInt32(ceasrq.KVAL);
              string[] strArray = new string[7];
              strArray[0] = "SELECT * FROM tbl_ce_evalution_psychometric_setup where id_brief_question= ";
              nullable = tblBriefAnswer.id_brief_question;
              strArray[1] = nullable.ToString();
              strArray[2] = " and id_brief_answer=";
              strArray[3] = tblBriefAnswer.id_brief_answer.ToString();
              strArray[4] = " and id_ce_career_evaluation_master=";
              strArray[5] = evaluationMaster.id_ce_career_evaluation_master.ToString();
              strArray[6] = " ";
              tbl_ce_evalution_psychometric_setup psychometricSetup = this.db.Database.SqlQuery<tbl_ce_evalution_psychometric_setup>(string.Concat(strArray)).FirstOrDefault<tbl_ce_evalution_psychometric_setup>();
              if (psychometricSetup != null)
                tempans.AKVAL = psychometricSetup.id_ce_evalution_answer_key;
              source.Add(tempans);
            }
            List<TEMPANS> list3 = source.OrderByDescending<TEMPANS, int>((Func<TEMPANS, int>) (t => t.SVAL)).ToList<TEMPANS>();
            int num10 = 3;
            int num11 = 1;
            foreach (TEMPANS tempans in list3)
            {
              int num12;
              if (num11 == 1)
              {
                ++num11;
                num12 = tempans.SVAL;
                num10 -= tempans.SVAL;
              }
              else
                num12 = num10;
              this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_audit (id_ce_career_evaluation_master,ce_evaluation_token, id_organization, id_user, id_brief_question, question_complexity, id_brief_answer, value_sent, attempt_no, recorded_timestamp, audit_result,job_point,id_ce_evalution_answer_key, status, updated_date_time,id_job) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})", (object) evaluationMaster.id_ce_career_evaluation_master, (object) data.TOKEN, (object) data.OID, (object) data.UID, (object) tblBriefQuestion.id_brief_question, (object) tblBriefQuestion.question_complexity, (object) tempans.AID, (object) ceasrq.VAL, (object) num3, (object) DateTime.Now, (object) num12, (object) num12, (object) tempans.AKVAL, (object) 'A', (object) DateTime.Now, (object) data.ID_JOB);
            }
          }
        }
        string str2 = ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString();
        int num13 = data.OID;
        string str3 = num13.ToString();
        string str4 = str2 + "briefQuesion/" + str3 + "/question/";
        string str5 = ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString();
        num13 = data.OID;
        string str6 = num13.ToString();
        string str7 = str5 + "briefQuesion/" + str6 + "/choice/";
        int num14 = 1;
        foreach (CEASRQ ceasrq in data.ASRQ)
        {
          CEASRQ row = ceasrq;
          CEUserInput ceUserInput = new CEUserInput();
          tbl_brief_question qtn = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>) (t => t.id_brief_question == row.QID)).FirstOrDefault<tbl_brief_question>();
          if (qtn != null)
          {
            qtn.question_image = string.IsNullOrEmpty(qtn.question_image) ? "" : str4 + qtn.question_image;
            tbl_brief_answer tblBriefAnswer1 = new tbl_brief_answer();
            tbl_brief_answer tblBriefAnswer2 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>) (t => t.id_brief_answer == row.ANS)).FirstOrDefault<tbl_brief_answer>();
            List<CEAnswerBody> ceAnswerBodyList = new List<CEAnswerBody>();
            int? nullable = evaluationMaster.ce_assessment_type;
            num13 = 1;
            if (nullable.GetValueOrDefault() == num13 & nullable.HasValue)
            {
              nullable = tblBriefAnswer2.is_correct_answer;
              num13 = 1;
              if (nullable.GetValueOrDefault() == num13 & nullable.HasValue)
              {
                ++num1;
                ceUserInput.is_right = 1;
                ceUserInput.WANS = tblBriefAnswer2.brief_answer;
              }
              else
              {
                tbl_brief_answer tblBriefAnswer3 = this.db.tbl_brief_answer.Where<tbl_brief_answer>((Expression<Func<tbl_brief_answer, bool>>) (t => t.id_brief_question == (int?) row.QID && t.is_correct_answer == (int?) 1)).FirstOrDefault<tbl_brief_answer>();
                ceUserInput.is_right = 0;
                ceUserInput.WANS = tblBriefAnswer3.brief_answer;
              }
              string[] strArray = new string[9];
              strArray[0] = "SELECT CASE WHEN (a.choice_image IS NOT NULL) THEN CONCAT('";
              strArray[1] = str7;
              strArray[2] = "', a.choice_image) ELSE NULL END choice_image, CASE  WHEN (a.id_brief_answer = ";
              num13 = tblBriefAnswer2.id_brief_answer;
              strArray[3] = num13.ToString();
              strArray[4] = ") THEN 'UC' WHEN (a.is_correct_answer = 1) THEN 'RC' ELSE 'NA' END answer_role, a.* FROM tbl_brief_answer a WHERE id_organization = ";
              num13 = data.OID;
              strArray[5] = num13.ToString();
              strArray[6] = " AND id_brief_question = ";
              num13 = qtn.id_brief_question;
              strArray[7] = num13.ToString();
              strArray[8] = " AND status = 'A'";
              List<CEAnswerBody> list4 = this.db.Database.SqlQuery<CEAnswerBody>(string.Concat(strArray)).ToList<CEAnswerBody>();
              ceUserInput.answerBody = list4;
            }
            nullable = evaluationMaster.ce_assessment_type;
            num13 = 2;
            if (nullable.GetValueOrDefault() == num13 & nullable.HasValue)
            {
              ceUserInput.WANS = "NA";
              ceUserInput.is_right = 0;
              string[] strArray = new string[7];
              strArray[0] = "SELECT * FROM tbl_ce_evaluation_audit WHERE id_ce_career_evaluation_master = ";
              num13 = evaluationMaster.id_ce_career_evaluation_master;
              strArray[1] = num13.ToString();
              strArray[2] = " AND id_brief_question = ";
              num13 = qtn.id_brief_question;
              strArray[3] = num13.ToString();
              strArray[4] = " AND id_brief_answer = ";
              num13 = tblBriefAnswer2.id_brief_answer;
              strArray[5] = num13.ToString();
              strArray[6] = " ";
              tbl_ce_evaluation_audit ceEvaluationAudit = this.db.Database.SqlQuery<tbl_ce_evaluation_audit>(string.Concat(strArray)).FirstOrDefault<tbl_ce_evaluation_audit>();
              if (ceEvaluationAudit != null)
                ceUserInput.jpscore = Convert.ToInt32((object) ceEvaluationAudit.job_point);
            }
            ceUserInput.questionBody = qtn;
            ceUserInput.Question = "Q. " + qtn.brief_question;
            ceUserInput.Answer = tblBriefAnswer2.brief_answer;
            tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == qtn.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
            if (questionComplexity != null)
            {
              ceUserInput.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
              ceUserInput.question_complexity_label = questionComplexity.question_complexity_label;
            }
            ceUserInput.srno = num14++;
            ceUserInputList.Add(ceUserInput);
          }
        }
        foreach (tbl_brief_question_complexity questionComplexity in list1)
        {
          string[] strArray = new string[9];
          strArray[0] = "SELECT CASE WHEN COUNT(*) > 0 THEN COUNT(*) ELSE 0 END totalcount, CASE WHEN COUNT(audit_result) > 0 THEN COUNT(audit_result) ELSE 0 END rightcount, CASE WHEN sum(job_point) > 0 THEN sum(job_point) ELSE 0 END jobpoint FROM tbl_ce_evaluation_audit  WHERE id_user = ";
          int num15 = data.UID;
          strArray[1] = num15.ToString();
          strArray[2] = " AND id_ce_career_evaluation_master = ";
          num15 = evaluationMaster.id_ce_career_evaluation_master;
          strArray[3] = num15.ToString();
          strArray[4] = " AND attempt_no = ";
          strArray[5] = num3.ToString();
          strArray[6] = " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE question_complexity = ";
          strArray[7] = questionComplexity.question_complexity.ToString();
          strArray[8] = ")";
          ComplexityResult complexityResult = new BriefModel().getComplexityResult(string.Concat(strArray));
          if (complexityResult.TOTALCOUNT > 0)
          {
            complexityResult.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
            complexityResult.question_complexity_label = questionComplexity.question_complexity_label;
            double num16 = Math.Round((double) (complexityResult.RIGHTCOUNT * 100 / complexityResult.TOTALCOUNT), 2);
            complexityResult.RESULT = num16;
            complexityResultList.Add(complexityResult);
          }
        }
        int? ceAssessmentType1 = evaluationMaster.ce_assessment_type;
        int num17 = 1;
        if (ceAssessmentType1.GetValueOrDefault() == num17 & ceAssessmentType1.HasValue)
        {
          ceReturnResponse.returnStat = "You have answered " + num1.ToString() + " out of " + count.ToString() + " question right ";
          ceReturnResponse.rightCount = num1;
          JobPoint jobPoint = this.db.Database.SqlQuery<JobPoint>("SELECT  attempt_no, SUM(job_point) job_point FROM tbl_ce_evaluation_audit WHERE id_user = " + data.UID.ToString() + " AND id_organization = " + data.OID.ToString() + " AND id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + " AND attempt_no = " + num3.ToString() + " GROUP BY attempt_no limit 1").FirstOrDefault<JobPoint>();
          if (jobPoint != null)
            num2 = jobPoint.job_point;
        }
        else
        {
          ceReturnResponse.returnStat = "NA";
          ceReturnResponse.rightCount = 0;
        }
        List<AnswerKeyBlock> answerKeyBlockList = new List<AnswerKeyBlock>();
        int? ceAssessmentType2 = evaluationMaster.ce_assessment_type;
        int num18 = 2;
        if (ceAssessmentType2.GetValueOrDefault() == num18 & ceAssessmentType2.HasValue)
          answerKeyBlockList = this.db.Database.SqlQuery<AnswerKeyBlock>("SELECT b.id_ce_evalution_answer_key, key_code, concat('" + (ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString() + "ANSWERKEY/") + "',key_code,'.png') aklogo, answer_key, SUM(b.job_point) job_point FROM tbl_ce_evalution_answer_key a, tbl_ce_evaluation_audit b WHERE a.id_ce_career_evaluation_master = b.id_ce_career_evaluation_master AND a.id_ce_evalution_answer_key = b.id_ce_evalution_answer_key AND b.id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + " AND b.id_user = " + data.UID.ToString() + " AND b.attempt_no = " + num3.ToString() + " GROUP BY b.id_ce_evalution_answer_key ORDER BY job_point DESC").ToList<AnswerKeyBlock>();
        ceReturnResponse.answerKeyBlock = answerKeyBlockList;
        ceReturnResponse.complexity = complexityResultList;
        ceReturnResponse.ceReturn = ceUserInputList;
        ceReturnResponse.totalCount = count;
        ceReturnResponse.attemptno = num3;
        string str8 = JsonConvert.SerializeObject((object) ceReturnResponse);
        this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_log(id_user,id_organization,id_ce_career_evaluation_master,json_response, attempt_no, ce_evaluation_result,cetimespan, status, updated_date_time,id_job) VALUES ({0},{1}, {2}, {3}, {4}, {5}, {6}, {7}, {8},{9})", (object) data.UID, (object) data.OID, (object) evaluationMaster.id_ce_career_evaluation_master, (object) str8, (object) num3, (object) num2, (object) str1, (object) 'A', (object) DateTime.Now, (object) data.ID_JOB);
      }
      if (!flag1)
        this.checkAttemptCompilation(evaluationMaster.id_ce_evaluation_tile, data.UID, data.OID, cindex);
      return namespace2.CreateResponse<CEReturnResponse>(this.Request, HttpStatusCode.OK, ceReturnResponse);
    }

    private bool checkAttemptCompilation(int ctid, int UID, int OID, int cindex) => (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master  WHERE  ce_assessment_type=1 AND  id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count <= 0.0;

    private void generateCompletionReport(int ctid, int UID, int OID, int cindex)
    {
    }
  }
}
