// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCEAssessmentDetailController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class getCEAssessmentDetailController : ApiController
  {
    private m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();

    public HttpResponseMessage Get(string crf, int UID, int OID)
    {
      CEBriefBody ceBriefBody = new CEBriefBody();
      List<APIBrief> apiBriefList = new List<APIBrief>();
      crf = crf.ToLower().Trim();
      tbl_ce_career_evaluation_master evaluationMaster1 = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where lower(career_evaluation_code)=lower('" + crf + "') and id_organization=" + OID.ToString() + " and status='A' limit 1").FirstOrDefault<tbl_ce_career_evaluation_master>();
      bool flag = false;
      if (evaluationMaster1 != null)
      {
        int num1 = 0;
        int num2 = 0;
        int int32_1 = Convert.ToInt32((object) evaluationMaster1.ce_assessment_type);
        int int32_2 = Convert.ToInt32(evaluationMaster1.id_ce_evaluation_tile);
        int currentIndex = this.getCurrentIndex(crf, UID, OID);
        int cindex = currentIndex;
        string sql = "SELECT * FROM tbl_ce_evaluation_tile where id_organization=" + OID.ToString() + " and id_ce_evaluation_tile=" + evaluationMaster1.id_ce_evaluation_tile.ToString();
        ceBriefBody.tile = this.db.Database.SqlQuery<tbl_ce_evaluation_tile>(sql).FirstOrDefault<tbl_ce_evaluation_tile>();
        if (this.checkAttemptCompilation(int32_2, UID, OID, currentIndex))
          ++cindex;
        if (int32_1 == 1)
          flag = this.checkCurrentEvaluationStatus(evaluationMaster1.id_ce_career_evaluation_master, UID, OID, cindex);
        if (int32_1 == 2)
        {
          int retIndex;
          num2 = this.checkPsychometricEvaluationStatus(evaluationMaster1.id_ce_career_evaluation_master, UID, OID, out retIndex);
          flag = num2 <= 0;
          num1 = retIndex;
        }
        if (!flag && int32_1 == 2)
        {
          if (num2 == 2)
          {
            ceBriefBody.status = "failed";
            ceBriefBody.message = "You have already completed this Psychometric Assessment. You can re-attempt after some time.";
            return namespace2.CreateResponse<CEBriefBody>(this.Request, HttpStatusCode.OK, ceBriefBody);
          }
          flag = true;
        }
        if (flag)
        {
          string str1 = "CET" + new Utility().uniqueIDS(7);
          int int32_3 = Convert.ToInt32((object) evaluationMaster1.no_of_question);
          tbl_ce_career_evaluation_master evaluationMaster2 = evaluationMaster1;
          ceBriefBody.CEBody = new CECategory()
          {
            id_ce_career_evaluation_master = evaluationMaster2.id_ce_career_evaluation_master,
            career_evaluation_title = evaluationMaster2.career_evaluation_title,
            career_evaluation_code = evaluationMaster2.career_evaluation_code,
            id_ce_evaluation_tile = Convert.ToInt32(evaluationMaster2.id_ce_evaluation_tile),
            ce_description = evaluationMaster2.ce_description,
            validation_period = Convert.ToInt32(evaluationMaster2.validation_period),
            ordering_sequence_number = Convert.ToInt32((object) evaluationMaster2.ordering_sequence_number),
            no_of_question = Convert.ToInt32((object) evaluationMaster2.no_of_question),
            is_time_enforced = Convert.ToInt32((object) evaluationMaster2.is_time_enforced),
            time_enforced = Convert.ToInt32((object) evaluationMaster2.time_enforced),
            ce_assessment_type = Convert.ToInt32((object) evaluationMaster2.ce_assessment_type),
            job_points_for_ra = Convert.ToInt32((object) evaluationMaster2.job_points_for_ra),
            CEToken = str1
          };
          List<CEQuestionList> ceQuestionListList = new List<CEQuestionList>();
          List<tbl_brief_question> tblBriefQuestionList1 = new List<tbl_brief_question>();
          ceBriefBody.RESULTSTATUS = 0;
          ceBriefBody.RESULTSCORE = 0.0;
          ceBriefBody.RESULT = (BriefReturnResponse) null;
          List<int> intList = new List<int>();
          int num3 = int32_3;
          List<tbl_brief_category> tblBriefCategoryList = new List<tbl_brief_category>();
          string str2 = ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString() + "briefQuesion/" + OID.ToString() + "/question/";
          string str3 = ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString() + "briefQuesion/" + OID.ToString() + "/choice/";
          if (int32_1 == 1)
          {
            List<tbl_brief_category> list1 = this.db.Database.SqlQuery<tbl_brief_category>("SELECT * FROM tbl_brief_category WHERE status='A' and  id_brief_category IN (SELECT DISTINCT id_brief_category FROM tbl_ce_category_mapping WHERE  status='A' and  id_ce_career_evaluation_master = " + evaluationMaster1.id_ce_career_evaluation_master.ToString() + ")").ToList<tbl_brief_category>();
            int num4 = num3;
            List<tbl_brief_question> source = new List<tbl_brief_question>();
            int num5 = list1.Count<tbl_brief_category>();
            int num6 = num5 * 20;
            int num7 = 0;
            int totalcount = 0;
            do
            {
              int index = num7 % num5;
              tbl_brief_category tblBriefCategory = list1[index];
              tbl_brief_question temp = this.getCEProgressiveDistributionQuestion(UID, tblBriefCategory.id_brief_category, OID, totalcount);
              if (temp != null)
              {
                ++totalcount;
                if (!intList.Contains(temp.id_brief_question) && source.Where<tbl_brief_question>((Func<tbl_brief_question, bool>) (t => t.id_brief_question == temp.id_brief_question)).FirstOrDefault<tbl_brief_question>() == null)
                {
                  temp.question_image = string.IsNullOrEmpty(temp.question_image) ? "" : str2 + temp.question_image;
                  source.Add(temp);
                  --num4;
                }
              }
              if (num7 <= 150)
                ++num7;
              else
                break;
            }
            while (source.Count != num3);
            foreach (tbl_brief_question tblBriefQuestion in source)
            {
              tbl_ce_evaluation_progdist_mapping evaluationProgdistMapping = new tbl_ce_evaluation_progdist_mapping()
              {
                id_ce_career_evaluation_master = new int?(evaluationMaster1.id_ce_career_evaluation_master),
                ce_evaluation_token = str1,
                id_brief_question = new int?(tblBriefQuestion.id_brief_question),
                id_user = new int?(UID),
                date_time_stamp = new DateTime?(DateTime.Now),
                question_link_type = new int?(0),
                status = "A",
                updated_date_time = new DateTime?(DateTime.Now)
              };
              this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_progdist_mapping (ce_evaluation_token, id_ce_career_evaluation_master, id_brief_question, id_user, question_link_type, date_time_stamp, status, updated_date_time) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", (object) str1, (object) evaluationMaster1.id_ce_career_evaluation_master, (object) tblBriefQuestion.id_brief_question, (object) UID, (object) 0, (object) DateTime.Now, (object) 'A', (object) DateTime.Now);
              CEQuestionList ceQuestionList = new CEQuestionList();
              tbl_brief_question_complexity questionComplexity = this.db.Database.SqlQuery<tbl_brief_question_complexity>("select * from tbl_brief_question_complexity where question_complexity=" + tblBriefQuestion.question_complexity.ToString() + " and status='A'").FirstOrDefault<tbl_brief_question_complexity>();
              if (questionComplexity != null)
              {
                ceQuestionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
                ceQuestionList.question_complexity_label = questionComplexity.question_complexity_label;
              }
              ceQuestionList.CEToken = str1;
              ceQuestionList.question = tblBriefQuestion;
              List<tbl_brief_answer> list2 = this.db.Database.SqlQuery<tbl_brief_answer>("SELECT CASE WHEN (a.choice_image IS NOT NULL) THEN CONCAT('" + str3 + "', a.choice_image) ELSE NULL END choice_image, a.* FROM tbl_brief_answer a WHERE id_organization = " + OID.ToString() + " AND id_brief_question = " + tblBriefQuestion.id_brief_question.ToString() + " AND status = 'A'").ToList<tbl_brief_answer>();
              ceQuestionList.answers = list2;
              ceQuestionListList.Add(ceQuestionList);
            }
          }
          if (int32_1 == 2)
          {
            List<tbl_brief_question> tblBriefQuestionList2 = new List<tbl_brief_question>();
            tbl_brief_category tblBriefCategory = this.db.Database.SqlQuery<tbl_brief_category>("SELECT * FROM tbl_brief_category where  status='A' and   id_brief_category in (SELECT id_brief_category FROM tbl_ce_category_mapping where status='A' and id_ce_career_evaluation_master=" + evaluationMaster1.id_ce_career_evaluation_master.ToString() + ") and status='A' and id_organization=" + OID.ToString() + " ").FirstOrDefault<tbl_brief_category>();
            if (tblBriefCategory != null)
            {
              List<tbl_brief_question> list3 = this.db.Database.SqlQuery<tbl_brief_question>("SELECT * FROM tbl_brief_question WHERE id_organization=" + OID.ToString() + " and  id_brief_category =" + tblBriefCategory.id_brief_category.ToString() + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity ").ToList<tbl_brief_question>();
              List<CEQuestionSorted> source = new List<CEQuestionSorted>();
              int num8 = 1;
              foreach (tbl_brief_question tblBriefQuestion in list3)
              {
                CEQuestionSorted ceQuestionSorted = new CEQuestionSorted();
                ceQuestionSorted.question = tblBriefQuestion;
                int num9 = this.db.Database.SqlQuery<int>("SELECT ordering_sequence FROM tbl_ce_evalution_psychometric_setup where id_ce_career_evaluation_master=" + evaluationMaster1.id_ce_career_evaluation_master.ToString() + " and id_organization= " + OID.ToString() + "   and id_brief_question=" + tblBriefQuestion.id_brief_question.ToString() + " ").FirstOrDefault<int>();
                ceQuestionSorted.ordering_sequence = num9;
                source.Add(ceQuestionSorted);
              }
              foreach (CEQuestionSorted ceQuestionSorted in source.OrderBy<CEQuestionSorted, int>((Func<CEQuestionSorted, int>) (t => t.ordering_sequence)).ToList<CEQuestionSorted>())
              {
                tbl_ce_evaluation_progdist_mapping evaluationProgdistMapping = new tbl_ce_evaluation_progdist_mapping()
                {
                  id_ce_career_evaluation_master = new int?(evaluationMaster1.id_ce_career_evaluation_master),
                  ce_evaluation_token = str1,
                  id_brief_question = new int?(ceQuestionSorted.question.id_brief_question),
                  id_user = new int?(UID),
                  date_time_stamp = new DateTime?(DateTime.Now),
                  question_link_type = new int?(1),
                  status = "A",
                  updated_date_time = new DateTime?(DateTime.Now)
                };
                this.db.Database.ExecuteSqlCommand("INSERT INTO tbl_ce_evaluation_progdist_mapping (ce_evaluation_token, id_ce_career_evaluation_master, id_brief_question, id_user, question_link_type, date_time_stamp, status, updated_date_time) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})", (object) str1, (object) evaluationMaster1.id_ce_career_evaluation_master, (object) ceQuestionSorted.question.id_brief_question, (object) UID, (object) 1, (object) DateTime.Now, (object) 'A', (object) DateTime.Now);
                CEQuestionList ceQuestionList = new CEQuestionList();
                tbl_brief_question_complexity questionComplexity = this.db.Database.SqlQuery<tbl_brief_question_complexity>("select * from tbl_brief_question_complexity where question_complexity=" + ceQuestionSorted.question.question_complexity.ToString() + " and status='A'").FirstOrDefault<tbl_brief_question_complexity>();
                if (questionComplexity != null)
                {
                  ceQuestionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
                  ceQuestionList.question_complexity_label = questionComplexity.question_complexity_label;
                }
                ceQuestionSorted.question.question_image = string.IsNullOrEmpty(ceQuestionSorted.question.question_image) ? "" : str2 + ceQuestionSorted.question.question_image;
                ceQuestionList.qtnnum = num8;
                ceQuestionList.CEToken = str1;
                ceQuestionSorted.question.qtnnum = num8;
                ceQuestionList.question = ceQuestionSorted.question;
                List<tbl_brief_answer> list4 = this.db.Database.SqlQuery<tbl_brief_answer>("SELECT CASE WHEN (a.choice_image IS NOT NULL) THEN CONCAT('" + str3 + "', a.choice_image) ELSE NULL END choice_image, a.* FROM tbl_brief_answer a WHERE id_organization = " + OID.ToString() + " AND id_brief_question = " + ceQuestionSorted.question.id_brief_question.ToString() + " AND status = 'A'").ToList<tbl_brief_answer>();
                ceQuestionList.answers = list4;
                ceQuestionListList.Add(ceQuestionList);
                ++num8;
              }
            }
          }
          ceBriefBody.QTNLIST = ceQuestionListList;
          ceBriefBody.status = "success";
          ceBriefBody.message = "";
          return namespace2.CreateResponse<CEBriefBody>(this.Request, HttpStatusCode.OK, ceBriefBody);
        }
        ceBriefBody.status = "failed";
        ceBriefBody.message = "You need to complete all the other assessments to re-attempt this one.";
        return namespace2.CreateResponse<CEBriefBody>(this.Request, HttpStatusCode.OK, ceBriefBody);
      }
      ceBriefBody.status = "failed";
      ceBriefBody.message = "Invalid Assessment Request.";
      return namespace2.CreateResponse<CEBriefBody>(this.Request, HttpStatusCode.OK, ceBriefBody);
    }

    private tbl_brief_question getCEProgressiveDistributionQuestion(
      int UID,
      int CID,
      int OID,
      int totalcount)
    {
      int num1 = this.db.Database.SqlQuery<int>("select count(*) scount from tbl_brief_question where  id_organization=" + OID.ToString() + "  and  id_brief_category =" + CID.ToString() + " and status='A'").FirstOrDefault<int>();
      bool flag1 = false;
      bool flag2 = false;
      if (num1 <= totalcount)
        flag1 = true;
      if (num1 > 0 && num1 * 2 <= totalcount)
        flag2 = true;
      tbl_ce_evaluation_audit ceEvaluationAudit1 = new tbl_ce_evaluation_audit();
      tbl_ce_evaluation_audit ceEvaluationAudit2 = this.db.Database.SqlQuery<tbl_ce_evaluation_audit>("SELECT * FROM tbl_ce_evaluation_audit WHERE  id_user = " + UID.ToString() + " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE  id_organization=" + OID.ToString() + " and id_brief_category = " + CID.ToString() + ") ORDER BY id_ce_evaluation_audit DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_audit>();
      bool status = false;
      if (ceEvaluationAudit2 != null)
      {
        tbl_brief_question tblBriefQuestion = this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_question= " + ceEvaluationAudit2.id_brief_question.ToString() + " ").FirstOrDefault<tbl_brief_question>();
        int? auditResult = ceEvaluationAudit2.audit_result;
        int num2 = 1;
        if (auditResult.GetValueOrDefault() == num2 & auditResult.HasValue)
          status = true;
        int complecityLevel = this.getComplecityLevel(CID, status, tblBriefQuestion.question_complexity);
        if (flag1)
        {
          tbl_brief_question distributionQuestion = this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where  id_organization=" + OID.ToString() + "  and  id_brief_category =" + CID.ToString() + " and  id_brief_question in (SELECT distinct id_brief_question FROM tbl_ce_evaluation_audit where id_user =" + UID.ToString() + " AND audit_result=0) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>();
          if (distributionQuestion != null)
            return distributionQuestion;
        }
        if (flag2)
        {
          tbl_brief_question distributionQuestion = this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where  id_organization=" + OID.ToString() + "  and  id_brief_category =" + CID.ToString() + " and status='A' and expiry_date>now() ORDER BY question_complexity, RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>();
          if (distributionQuestion != null)
            return distributionQuestion;
        }
        tbl_brief_question distributionQuestion1 = this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where  id_organization=" + OID.ToString() + "  and  id_brief_category =" + CID.ToString() + " and  id_brief_question not in (SELECT distinct id_brief_question FROM tbl_ce_evaluation_audit where id_user =" + UID.ToString() + " ) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>();
        if (distributionQuestion1 != null)
          return distributionQuestion1;
        return this.db.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where   id_organization=" + OID.ToString() + "  and  id_brief_category =" + CID.ToString() + " and id_brief_question in (SELECT distinct id_brief_question FROM tbl_ce_evaluation_audit where id_user =" + UID.ToString() + " AND audit_result=0) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question) null;
      }
      return this.db.Database.SqlQuery<tbl_brief_question>("SELECT * FROM tbl_brief_question WHERE id_organization=" + OID.ToString() + " and  id_brief_category =" + CID.ToString() + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity,RAND()  LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question) null;
    }

    private int getComplecityLevel(int CID, bool status, int? level)
    {
      this.db.Database.SqlQuery<int>("select count(*) scount from tbl_brief_question where  id_brief_category = " + CID.ToString() + " AND question_complexity=" + level.ToString() + " ").FirstOrDefault<int>();
      string str = !status ? "  AND question_complexity < " + level.ToString() + " order by question_complexity desc LIMIT 1 " : "  AND question_complexity > " + level.ToString() + " order by question_complexity  LIMIT 1 ";
      tbl_brief_question_complexity questionComplexity = this.db.Database.SqlQuery<tbl_brief_question_complexity>("SELECT * FROM tbl_brief_question_complexity WHERE question_complexity IN (SELECT DISTINCT question_complexity FROM tbl_brief_question WHERE id_brief_category = " + CID.ToString() + ") " + str).FirstOrDefault<tbl_brief_question_complexity>();
      return questionComplexity != null ? Convert.ToInt32((object) questionComplexity.question_complexity) : Convert.ToInt32((object) level);
    }

    private int getCurrentIndex(string crf, int UID, int OID)
    {
      int currentIndex = 1;
      tbl_ce_evaluation_index ceEvaluationIndex = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization =  " + OID.ToString() + " ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
      if (ceEvaluationIndex != null)
        currentIndex = ceEvaluationIndex.attempt_no;
      return currentIndex;
    }

    private bool checkAttemptCompilation(int ctid, int UID, int OID, int cindex)
    {
      bool flag = false;
      double count1 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master WHERE ce_assessment_type=1 AND id_ce_evaluation_tile=" + ctid.ToString() + " AND status='A' order by ordering_sequence_number ").ToList<tbl_ce_career_evaluation_master>().Count;
      double count2 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE  ce_assessment_type=1 AND  id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master  IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      double count3 = (double) this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master  WHERE  ce_assessment_type=1 AND  id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master NOT IN (SELECT DISTINCT id_ce_career_evaluation_master FROM tbl_ce_evaluation_audit WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND attempt_no = " + cindex.ToString() + ")").ToList<tbl_ce_career_evaluation_master>().Count;
      if (count2 > 0.0)
      {
        if (count1 == count2)
          flag = true;
      }
      else
        flag = true;
      return flag;
    }

    private bool checkCurrentEvaluationStatus(int ceid, int UID, int OID, int cindex)
    {
      bool flag = false;
      if (this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("SELECT * FROM tbl_ce_career_evaluation_master WHERE id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master IN (SELECT id_ce_career_evaluation_master FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master=" + ceid.ToString() + " AND attempt_no = " + cindex.ToString() + ") limit 1").FirstOrDefault<tbl_ce_career_evaluation_master>() == null)
        flag = true;
      return true;
    }

    private int checkPsychometricEvaluationStatus(int ceid, int UID, int OID, out int retIndex)
    {
      int num = 0;
      retIndex = 0;
      tbl_ce_evaluation_index ceEvaluationIndex = this.db.Database.SqlQuery<tbl_ce_evaluation_index>("SELECT * FROM tbl_ce_evaluation_index WHERE id_user = " + UID.ToString() + " AND id_organization = " + OID.ToString() + " AND id_ce_career_evaluation_master = " + ceid.ToString() + " ORDER BY attempt_no DESC LIMIT 1").FirstOrDefault<tbl_ce_evaluation_index>();
      if (ceEvaluationIndex != null)
      {
        num = ceEvaluationIndex.attempt_no;
        tbl_ce_career_evaluation_master evaluationMaster = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where id_ce_career_evaluation_master=" + ceid.ToString() + " ").FirstOrDefault<tbl_ce_career_evaluation_master>();
        if (evaluationMaster != null)
        {
          retIndex = ceEvaluationIndex.attempt_no;
          if (evaluationMaster.validation_period > 0)
          {
            DateTime dateTime = ceEvaluationIndex.dated_time_stamp;
            dateTime = dateTime.AddMonths(Convert.ToInt32(evaluationMaster.validation_period));
            num = !(DateTime.Now > dateTime) ? 2 : 1;
          }
        }
      }
      return num;
    }
  }
}
