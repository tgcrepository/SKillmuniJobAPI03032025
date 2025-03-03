// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefDetailsController
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

    public class getBriefDetailsController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string brf, int UID, int OID)
    {
      List<APIBrief> apiBriefList1 = new List<APIBrief>();
      BriefBody briefBody = new BriefBody();
      brf = brf.ToLower().Trim();
      ////"SELECT a.id_organization,a.question_count,a.id_brief_category,a.id_brief_sub_category, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user " + " FROM tbl_brief_master a, tbl_brief_user_assignment b WHERE   LOWER(brief_code) = '" + brf + "' AND a.id_brief_master = b.id_brief_master AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) LIMIT 10 ";
      List<APIBrief> apiBriefList2 = new BriefModel().getAPIBriefList("SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory " + " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE a.status='A' and  LOWER(brief_code) = '" + brf + "' AND  a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) ORDER BY datetimestamp DESC LIMIT 20");
      if (apiBriefList2.Count > 0)
      {
        briefBody = new BriefBody();
        APIBrief brief = apiBriefList2[0];
        tbl_brief_master master = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == brief.id_brief_master)).FirstOrDefault<tbl_brief_master>();
        int questionCount = brief.question_count;
        briefBody.BRIEF = brief;
        List<QuestionList> questionListList = new List<QuestionList>();
        List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
        tbl_brief_log tblBriefLog = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1 && t.id_brief_master == brief.id_brief_master && t.id_user == UID)).FirstOrDefault<tbl_brief_log>();
        if (tblBriefLog != null)
        {
          briefBody.RESULTSTATUS = 1;
          briefBody.RESULTSCORE = Convert.ToDouble((object) tblBriefLog.brief_result);
          BriefReturnResponse briefReturnResponse = JsonConvert.DeserializeObject<BriefReturnResponse>(tblBriefLog.json_response);
          briefBody.RESULT = briefReturnResponse;
          briefBody.QTNLIST = (List<QuestionList>) null;
        }
        else
        {
          briefBody.RESULTSTATUS = 0;
          briefBody.RESULTSCORE = 0.0;
          briefBody.RESULT = (BriefReturnResponse) null;
          List<int> intList = new List<int>();
          string[] strArray1 = new string[5]
          {
            "SELECT * FROM tbl_brief_question where id_organization=",
            OID.ToString(),
            " and id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=",
            null,
            null
          };
          int num1 = brief.id_brief_master;
          strArray1[3] = num1.ToString();
          strArray1[4] = " and status='A') and status='A'";
          List<tbl_brief_question> list1 = this.db.tbl_brief_question.SqlQuery(string.Concat(strArray1)).ToList<tbl_brief_question>();
          foreach (tbl_brief_question tblBriefQuestion in list1)
          {
            tbl_brief_question item = tblBriefQuestion;
            QuestionList questionList = new QuestionList();
            tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == item.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
            if (questionComplexity != null)
            {
              questionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
              questionList.question_complexity_label = questionComplexity.question_complexity_label;
            }
            questionList.question = item;
            string[] strArray2 = new string[5]
            {
              "select * from tbl_brief_answer where id_organization=",
              OID.ToString(),
              " and id_brief_question=",
              null,
              null
            };
            num1 = item.id_brief_question;
            strArray2[3] = num1.ToString();
            strArray2[4] = " and status='A'";
            List<tbl_brief_answer> list2 = this.db.tbl_brief_answer.SqlQuery(string.Concat(strArray2)).ToList<tbl_brief_answer>();
            questionList.answers = list2;
            questionListList.Add(questionList);
            intList.Add(item.id_brief_question);
          }
          int num2 = questionCount - list1.Count<tbl_brief_question>();
          if (num2 > 0)
          {
            int int32 = Convert.ToInt32((object) master.brief_type);
            List<tbl_brief_category> source1 = new List<tbl_brief_category>();
            if (int32 == 0)
              source1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category = " + master.id_brief_category.ToString() + " ").ToList<tbl_brief_category>();
            if (int32 == 2)
            {
              List<tbl_brief_category_mapping> briefCategoryMappingList = new List<tbl_brief_category_mapping>();
              if (this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>) (t => t.id_brief_master == (int?) master.id_brief_master)).ToList<tbl_brief_category_mapping>().Count > 0)
                source1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_category_mapping where  id_organization=" + OID.ToString() + " and id_brief_master=" + master.id_brief_master.ToString() + ") limit " + num2.ToString()).ToList<tbl_brief_category>();
            }
            if (int32 == 3)
              source1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num2.ToString()).ToList<tbl_brief_category>();
            if (int32 == 1)
              source1 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num2.ToString()).ToList<tbl_brief_category>();
            int num3 = num2;
            List<tbl_brief_question> source2 = new List<tbl_brief_question>();
            int num4 = source1.Count<tbl_brief_category>();
            int num5 = num4 * 20;
            int num6 = 0;
            do
            {
              int index = num6 % num4;
              tbl_brief_category tblBriefCategory = source1[index];
              tbl_brief_question temp = this.getProgressiveDistributionQuestion(UID, tblBriefCategory.id_brief_category, OID);
              if (temp != null && !intList.Contains(temp.id_brief_question) && source2.Where<tbl_brief_question>((Func<tbl_brief_question, bool>) (t => t.id_brief_question == temp.id_brief_question)).FirstOrDefault<tbl_brief_question>() == null)
              {
                source2.Add(temp);
                --num3;
              }
              if (num6 <= 150)
                ++num6;
              else
                break;
            }
            while (source2.Count != num2);
            foreach (tbl_brief_question tblBriefQuestion in source2)
            {
              tbl_brief_question item = tblBriefQuestion;
              this.db.tbl_brief_progdist_mapping.Add(new tbl_brief_progdist_mapping()
              {
                id_brief_master = new int?(brief.id_brief_master),
                id_brief_question = new int?(item.id_brief_question),
                id_user = new int?(UID),
                date_time_stamp = new DateTime?(DateTime.Now),
                question_link_type = new int?(1),
                status = "A",
                updated_date_time = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
              QuestionList questionList = new QuestionList();
              tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.Where<tbl_brief_question_complexity>((Expression<Func<tbl_brief_question_complexity, bool>>) (t => t.question_complexity == item.question_complexity)).FirstOrDefault<tbl_brief_question_complexity>();
              if (questionComplexity != null)
              {
                questionList.question_complexity = Convert.ToInt32((object) questionComplexity.question_complexity);
                questionList.question_complexity_label = questionComplexity.question_complexity_label;
              }
              questionList.question = item;
              List<tbl_brief_answer> list3 = this.db.tbl_brief_answer.SqlQuery("select * from tbl_brief_answer where id_organization=" + OID.ToString() + " and id_brief_question=" + item.id_brief_question.ToString() + " and status='A'").ToList<tbl_brief_answer>();
              questionList.answers = list3;
              questionListList.Add(questionList);
            }
          }
          briefBody.QTNLIST = questionListList;
        }
        tbl_brief_user_assignment briefUserAssignment = this.db.tbl_brief_user_assignment.Where<tbl_brief_user_assignment>((Expression<Func<tbl_brief_user_assignment, bool>>) (t => t.id_user == (int?) UID && t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_user_assignment>();
        if (briefUserAssignment != null)
        {
          if (briefUserAssignment.scheduled_status == "NA" && briefUserAssignment.published_status == "S")
          {
            briefUserAssignment.published_status = "R";
            briefUserAssignment.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
          if (briefUserAssignment.published_status == "NA" && briefUserAssignment.scheduled_status == "S")
          {
            briefUserAssignment.scheduled_status = "R";
            briefUserAssignment.updated_date_time = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
        }
      }
      return briefBody != null ? namespace2.CreateResponse<BriefBody>(this.Request, HttpStatusCode.OK, briefBody) : namespace2.CreateResponse<BriefBody>(this.Request, HttpStatusCode.NoContent, briefBody);
    }

    public tbl_brief_question getProgressiveDistributionQuestion(int UID, int CID, int OID)
    {
      tbl_brief_audit lstQtn = new tbl_brief_audit();
      lstQtn = this.db.tbl_brief_audit.SqlQuery("SELECT * FROM tbl_brief_audit WHERE  id_user = " + UID.ToString() + " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE  id_organization=" + OID.ToString() + " and id_brief_category = " + CID.ToString() + ") ORDER BY id_brief_audit DESC LIMIT 1").FirstOrDefault<tbl_brief_audit>();
      bool status = false;
      if (lstQtn != null)
      {
        tbl_brief_question tblBriefQuestion = this.db.tbl_brief_question.Where<tbl_brief_question>((Expression<Func<tbl_brief_question, bool>>) (t => (int?) t.id_brief_question == lstQtn.id_brief_question)).FirstOrDefault<tbl_brief_question>();
        int? auditResult = lstQtn.audit_result;
        int num = 1;
        if (auditResult.GetValueOrDefault() == num & auditResult.HasValue)
          status = true;
        int complecityLevel = this.getComplecityLevel(CID, status, tblBriefQuestion.question_complexity);
        tbl_brief_question distributionQuestion = this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and  id_brief_question not in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " ) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>();
        if (distributionQuestion != null)
          return distributionQuestion;
        return this.db.tbl_brief_question.SqlQuery("select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and id_brief_question in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " AND audit_result=0) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY RAND() LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question) null;
      }
      return this.db.tbl_brief_question.SqlQuery("SELECT * FROM tbl_brief_question WHERE id_organization=" + OID.ToString() + " and  id_brief_category =" + CID.ToString() + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity,RAND()  LIMIT 1").FirstOrDefault<tbl_brief_question>() ?? (tbl_brief_question) null;
    }

    public int getComplecityLevel(int CID, bool status, int? level)
    {
      string str = !status ? "  AND question_complexity < " + level.ToString() + " order by question_complexity desc LIMIT 1 " : "  AND question_complexity > " + level.ToString() + " order by question_complexity  LIMIT 1 ";
      tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity WHERE question_complexity IN (SELECT DISTINCT question_complexity FROM tbl_brief_question WHERE id_brief_category = " + CID.ToString() + ") " + str).FirstOrDefault<tbl_brief_question_complexity>();
      return questionComplexity != null ? Convert.ToInt32((object) questionComplexity.question_complexity) : Convert.ToInt32((object) level);
    }
  }
}
