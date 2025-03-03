// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefResourceDetailWithQuestionThemeController
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

    public class getBriefResourceDetailWithQuestionThemeController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string brf, int UID, int OID)
    {
      List<APIBrief> apiBriefList1 = new List<APIBrief>();
      BriefResource briefResource = new BriefResource();
      brf = brf.ToLower().Trim();
      ////"SELECT a.id_organization,a.question_count,a.id_brief_category,a.id_brief_sub_category, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user " + " FROM tbl_brief_master a, tbl_brief_user_assignment b WHERE   LOWER(brief_code) = '" + brf + "' AND a.id_brief_master = b.id_brief_master AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) LIMIT 10 ";
      List<APIBrief> apiBriefList2 = new BriefModel().getAPIBriefList("SELECT a.id_organization,question_count, brief_title, brief_code, brief_description, CASE WHEN scheduled_status = 'NA' THEN published_datetime WHEN published_status = 'NA' THEN scheduled_datetime ELSE NULL END datetimestamp, CASE WHEN scheduled_status = 'NA' THEN 'P' WHEN published_status = 'NA' THEN 'S' ELSE NULL END scheduled_type, a.override_dnd, a.id_brief_master, b.id_user, a.is_add_question is_question_attached, c.action_status, c.read_status, d.brief_category, e.brief_subcategory, d.id_brief_category, e.id_brief_subcategory " + " FROM tbl_brief_master a, tbl_brief_user_assignment b, tbl_brief_read_status c, tbl_brief_category d, tbl_brief_subcategory e WHERE  LOWER(brief_code) = '" + brf + "' AND a.status='A' AND  a.id_brief_master = b.id_brief_master AND a.id_brief_master = c.id_brief_master AND b.id_user = c.id_user AND a.id_brief_category = d.id_brief_category AND a.id_brief_sub_category = e.id_brief_subcategory AND a.id_brief_sub_category = e.id_brief_subcategory AND b.id_user = " + UID.ToString() + " AND a.id_organization = " + OID.ToString() + " AND (published_datetime < NOW() OR scheduled_datetime < NOW()) ORDER BY datetimestamp DESC LIMIT 50");
      if (apiBriefList2.Count > 0)
      {
        briefResource = new BriefResource();
        APIBrief brief = apiBriefList2[0];
        tbl_brief_master master = this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == brief.id_brief_master)).FirstOrDefault<tbl_brief_master>();
        tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>) (t => t.id_brief_master == (int?) brief.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
        briefResource.brief_template = briefMasterTemplate == null ? "0" : briefMasterTemplate.brief_template;
        int questionCount = brief.question_count;
        briefResource.BRIEF = brief;
        List<tbl_brief_master_body> list = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>) (t => t.id_brief_master == (int?) brief.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>) (t => t.srno)).ToList<tbl_brief_master_body>();
        List<BriefRow> briefRowList = new List<BriefRow>();
        foreach (tbl_brief_master_body tblBriefMasterBody in list)
        {
          BriefRow briefRow = new BriefRow()
          {
            media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type),
            resouce_code = tblBriefMasterBody.resouce_code,
            resource_order = briefMasterTemplate.resource_order,
            brief_destination = tblBriefMasterBody.brief_destination,
            resource_number = tblBriefMasterBody.resource_number,
            srno = Convert.ToInt32((object) tblBriefMasterBody.srno),
            resource_type = Convert.ToInt32((object) tblBriefMasterBody.resource_type),
            resouce_data = tblBriefMasterBody.resouce_data
          };
          briefRow.resouce_code = tblBriefMasterBody.resouce_code;
          briefRow.media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type);
          briefRow.resource_mime = tblBriefMasterBody.resource_mime;
          briefRow.file_extension = tblBriefMasterBody.file_extension;
          briefRow.file_type = tblBriefMasterBody.file_type;
          briefRowList.Add(briefRow);
        }
        briefResource.briefResource = briefRowList;
        List<QuestionList> questionListList = new List<QuestionList>();
        List<tbl_brief_question> source1 = new List<tbl_brief_question>();
        tbl_brief_log tblBriefLog = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1 && t.id_brief_master == brief.id_brief_master && t.id_user == UID)).FirstOrDefault<tbl_brief_log>();
        if (tblBriefLog != null)
        {
          briefResource.RESULTSTATUS = 1;
          briefResource.RESULTSCORE = Convert.ToDouble((object) tblBriefLog.brief_result);
          BriefReturnResponse briefReturnResponse = JsonConvert.DeserializeObject<BriefReturnResponse>(tblBriefLog.json_response);
          briefResource.RESULT = briefReturnResponse;
          briefResource.QTNLIST = (List<QuestionList>) null;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            briefResource.GameScore = 0.0;
            int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
            if (num != 0)
            {
              tbl_user_game_score_log userGameScoreLog = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_game_score_log>("select * from tbl_user_game_score_log where id_user={0} and id_game={1} and id_brief={2} and status={3}", (object) UID, (object) num, (object) brief.id_brief_master, (object) "A").FirstOrDefault<tbl_user_game_score_log>();
              if (userGameScoreLog != null)
                briefResource.GameScore = userGameScoreLog.score;
            }
          }
        }
        else
        {
          briefResource.RESULTSTATUS = 0;
          briefResource.RESULTSCORE = 0.0;
          briefResource.RESULT = (BriefReturnResponse) null;
          List<int> intList = new List<int>();
          string sql1 = "SELECT * FROM tbl_brief_question where id_organization=" + OID.ToString() + " and id_brief_question in (select id_brief_question from  tbl_brief_question_mapping where id_brief_master=" + brief.id_brief_master.ToString() + " and status='A') and status='A'";
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            source1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql1).ToList<tbl_brief_question>();
          foreach (tbl_brief_question tblBriefQuestion in source1)
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
            string sql2 = "select * from tbl_brief_answer where id_organization=" + OID.ToString() + " and id_brief_question=" + item.id_brief_question.ToString() + " and status='A'";
            List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              tblBriefAnswerList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>(sql2).ToList<tbl_brief_answer>();
            questionList.answers = tblBriefAnswerList;
            questionListList.Add(questionList);
            intList.Add(item.id_brief_question);
          }
          int num1 = questionCount - source1.Count<tbl_brief_question>();
          if (num1 > 0)
          {
            int int32 = Convert.ToInt32((object) master.brief_type);
            List<tbl_brief_category> source2 = new List<tbl_brief_category>();
            if (int32 == 0)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category = " + master.id_brief_category.ToString() + " ").ToList<tbl_brief_category>();
            if (int32 == 2)
            {
              List<tbl_brief_category_mapping> briefCategoryMappingList = new List<tbl_brief_category_mapping>();
              if (this.db.tbl_brief_category_mapping.Where<tbl_brief_category_mapping>((Expression<Func<tbl_brief_category_mapping, bool>>) (t => t.id_brief_master == (int?) master.id_brief_master)).ToList<tbl_brief_category_mapping>().Count > 0)
                source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_category_mapping where  id_organization=" + OID.ToString() + " and id_brief_master=" + master.id_brief_master.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            }
            if (int32 == 3)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            if (int32 == 1)
              source2 = this.db.tbl_brief_category.SqlQuery("select * from tbl_brief_category where status='A' and id_organization=" + OID.ToString() + "  and id_brief_category in (SELECT distinct id_brief_category FROM tbl_brief_question where id_organization=" + OID.ToString() + ") limit " + num1.ToString()).ToList<tbl_brief_category>();
            int num2 = num1;
            List<tbl_brief_question> source3 = new List<tbl_brief_question>();
            int num3 = source2.Count<tbl_brief_category>();
            int num4 = num3 * 20;
            int num5 = 0;
            do
            {
              int index = num5 % num3;
              tbl_brief_category tblBriefCategory = source2[index];
              tbl_brief_question temp = this.getProgressiveDistributionQuestion(UID, tblBriefCategory.id_brief_category, OID);
              if (temp != null && !intList.Contains(temp.id_brief_question) && source3.Where<tbl_brief_question>((Func<tbl_brief_question, bool>) (t => t.id_brief_question == temp.id_brief_question)).FirstOrDefault<tbl_brief_question>() == null)
              {
                source3.Add(temp);
                --num2;
              }
              if (num5 <= 150)
                ++num5;
              else
                break;
            }
            while (source3.Count != num1);
            foreach (tbl_brief_question tblBriefQuestion in source3)
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
              string sql3 = "select * from tbl_brief_answer where id_organization=" + OID.ToString() + " and id_brief_question=" + item.id_brief_question.ToString() + " and status='A'";
              List<tbl_brief_answer> tblBriefAnswerList = new List<tbl_brief_answer>();
              using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
                tblBriefAnswerList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>(sql3).ToList<tbl_brief_answer>();
              questionList.answers = tblBriefAnswerList;
              questionListList.Add(questionList);
            }
          }
          briefResource.QTNLIST = questionListList;
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
      return briefResource != null ? namespace2.CreateResponse<BriefResource>(this.Request, HttpStatusCode.OK, briefResource) : namespace2.CreateResponse<BriefResource>(this.Request, HttpStatusCode.NoContent, briefResource);
    }

    public tbl_brief_question getProgressiveDistributionQuestion(int UID, int CID, int OID)
    {
      tbl_brief_audit tblBriefAudit1 = new tbl_brief_audit();
      tbl_brief_audit tblBriefAudit2 = this.db.tbl_brief_audit.SqlQuery("SELECT * FROM tbl_brief_audit WHERE  id_user = " + UID.ToString() + " AND id_brief_question IN (SELECT id_brief_question FROM tbl_brief_question WHERE  id_organization=" + OID.ToString() + " and id_brief_category = " + CID.ToString() + ") ORDER BY id_brief_audit DESC LIMIT 1").FirstOrDefault<tbl_brief_audit>();
      bool status = false;
      if (tblBriefAudit2 != null)
      {
        tbl_brief_question tblBriefQuestion1 = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblBriefQuestion1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_question={0} ", (object) tblBriefAudit2.id_brief_question).FirstOrDefault<tbl_brief_question>();
        int? auditResult = tblBriefAudit2.audit_result;
        int num = 1;
        if (auditResult.GetValueOrDefault() == num & auditResult.HasValue)
          status = true;
        int complecityLevel = this.getComplecityLevel(CID, status, tblBriefQuestion1.question_complexity);
        string sql1 = "select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and  id_brief_question not in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " ) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY  RAND() LIMIT 1";
        tbl_brief_question distributionQuestion = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          distributionQuestion = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql1).FirstOrDefault<tbl_brief_question>();
        if (distributionQuestion != null)
          return distributionQuestion;
        string sql2 = "select * from tbl_brief_question where  id_organization=" + OID.ToString() + " and id_brief_question in (SELECT distinct id_brief_question FROM tbl_brief_audit where id_user =" + UID.ToString() + " AND audit_result=0) and question_complexity=" + complecityLevel.ToString() + " and status='A' and expiry_date>now() ORDER BY RAND() LIMIT 1";
        tbl_brief_question tblBriefQuestion2 = new tbl_brief_question();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblBriefQuestion2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql2).FirstOrDefault<tbl_brief_question>();
        return tblBriefQuestion2 ?? (tbl_brief_question) null;
      }
      string sql = "SELECT * FROM tbl_brief_question WHERE id_organization=" + OID.ToString() + " and  id_brief_category =" + CID.ToString() + " AND status = 'A' AND expiry_date > NOW() ORDER BY question_complexity,RAND()  LIMIT 1";
      tbl_brief_question tblBriefQuestion = new tbl_brief_question();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tblBriefQuestion = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>(sql).FirstOrDefault<tbl_brief_question>();
      return tblBriefQuestion ?? (tbl_brief_question) null;
    }

    public int getComplecityLevel(int CID, bool status, int? level)
    {
      string str = !status ? "  AND question_complexity < " + level.ToString() + " order by question_complexity desc LIMIT 1 " : "  AND question_complexity > " + level.ToString() + " order by question_complexity  LIMIT 1 ";
      tbl_brief_question_complexity questionComplexity = this.db.tbl_brief_question_complexity.SqlQuery("SELECT * FROM tbl_brief_question_complexity WHERE question_complexity IN (SELECT DISTINCT question_complexity FROM tbl_brief_question WHERE id_brief_category = " + CID.ToString() + ") " + str).FirstOrDefault<tbl_brief_question_complexity>();
      return questionComplexity != null ? Convert.ToInt32((object) questionComplexity.question_complexity) : Convert.ToInt32((object) level);
    }
  }
}
