// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EvaluateBriefAcademyController
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

    public class EvaluateBriefAcademyController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(
      int OID,
      int UID,
      int BID,
      string BRF,
      string ASRQ,
      int AcademicTileId,
      int brief_tile_id)
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
      tbl_restriction_user_log restrictionUserLog = new tbl_restriction_user_log();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_restriction_user_log>("select * from tbl_restriction_user_log where UID={0} and id_brief_master={1}", (object) UID, (object) BID).FirstOrDefault<tbl_restriction_user_log>() == null)
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into  tbl_restriction_user_log (UID,OID,id_brief_master,id_academy,updated_date_time,status,id_brief_tile) values({0},{1},{2},{3},{4},{5},{6})", (object) UID, (object) OID, (object) BID, (object) AcademicTileId, (object) DateTime.Now, (object) "A", (object) brief_tile_id);
      }
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tbl_game_master tblGameMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_game_master>("Select * from tbl_game_master where id_theme={0} AND status={1}", (object) 9, (object) "A").FirstOrDefault<tbl_game_master>();
        if (tblGameMaster != null)
        {
          if (DateTime.Now >= tblGameMaster.start_date)
          {
            if (DateTime.Now <= tblGameMaster.end_date)
            {
              if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_game_academic_mapping>("Select * from tbl_game_academic_mapping where id_academic_tile={0} AND status={1}", (object) AcademicTileId, (object) "A").FirstOrDefault<tbl_game_academic_mapping>() != null)
              {
                List<AssessmentScoreResponse> assessmentScoreResponseList1 = new UniversityScoringlogic().ScoreCal(AcademicTileId, BID, UID, briefReturnResponse.rightCount);
                List<AssessmentScoreResponse> assessmentScoreResponseList2 = new UniversityScoringlogic().ScoreSplCal(AcademicTileId, BID, UID, briefReturnResponse.rightCount);
                foreach (AssessmentScoreResponse assessmentScoreResponse in assessmentScoreResponseList1)
                  m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_game_score_log (id_user,id_game,id_brief,id_org,score,status,id_academic_tile,updated_date_time,id_metric,metric_value) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", (object) UID, (object) assessmentScoreResponse.id_game, (object) assessmentScoreResponse.id_brief, (object) OID, (object) assessmentScoreResponse.metric_score, (object) "A", (object) AcademicTileId, (object) DateTime.Now, (object) assessmentScoreResponse.id_metric, (object) assessmentScoreResponse.metric_name);
                foreach (AssessmentScoreResponse assessmentScoreResponse in assessmentScoreResponseList2)
                  m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_game_special_metric_score_log (id_user,id_game,id_brief,id_org,score,status,id_academic_tile,updated_date_time,id_special_metric,special_metric_value) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", (object) UID, (object) assessmentScoreResponse.id_game, (object) assessmentScoreResponse.id_brief, (object) OID, (object) assessmentScoreResponse.metric_score, (object) "A", (object) AcademicTileId, (object) DateTime.Now, (object) assessmentScoreResponse.id_metric, (object) assessmentScoreResponse.metric_name);
                List<tbl_user_game_score_log> userGameScoreLogList = new List<tbl_user_game_score_log>();
                Database database1 = m2ostnextserviceDbContext.Database;
                object[] objArray1 = new object[4]
                {
                  (object) tblGameMaster.id_game,
                  (object) UID,
                  null,
                  null
                };
                DateTime now1 = DateTime.Now;
                objArray1[2] = (object) now1.Year;
                now1 = DateTime.Now;
                objArray1[3] = (object) now1.Month;
                List<tbl_user_game_score_log> list3 = database1.SqlQuery<tbl_user_game_score_log>("select * from tbl_user_game_score_log where id_game={0} and id_user={1} and  YEAR(updated_date_time) = {2} AND MONTH(updated_date_time) = {3} ", objArray1).ToList<tbl_user_game_score_log>();
                double num10 = 0.0;
                foreach (tbl_user_game_score_log userGameScoreLog in list3)
                  num10 += userGameScoreLog.score;
                int int32 = Convert.ToInt32(num10);
                int num11 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} AND status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
                List<tbl_badge_master> list4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
                List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
                List<tbl_user_gcm_log> list5 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_gcm_log>("select * from tbl_user_gcm_log where id_user={0} and status='A'", (object) UID).ToList<tbl_user_gcm_log>();
                foreach (tbl_badge_master tblBadgeMaster in list4)
                {
                  tblBadgeMaster.eligiblescore = m2ostnextserviceDbContext.Database.SqlQuery<int>("select required_score from tbl_badge_data where id_game={0} and id_badge={1}", (object) num11, (object) tblBadgeMaster.id_badge).FirstOrDefault<int>();
                  if (int32 >= tblBadgeMaster.eligiblescore)
                  {
                    Database database2 = m2ostnextserviceDbContext.Database;
                    object[] objArray2 = new object[5];
                    DateTime now2 = DateTime.Now;
                    objArray2[0] = (object) now2.Year;
                    now2 = DateTime.Now;
                    objArray2[1] = (object) now2.Month;
                    objArray2[2] = (object) UID;
                    objArray2[3] = (object) tblBadgeMaster.id_badge;
                    objArray2[4] = (object) num11;
                    if (database2.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log WHERE YEAR(updated_date_time) = {0} AND MONTH(updated_date_time) = {1} and id_user = {2}  and id_badge = {3} and id_game={4}", objArray2).FirstOrDefault<tbl_user_badge_log>() == null)
                    {
                      m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_badge_log (id_user,id_org,id_badge,updated_date_time,id_game) values({0},{1},{2},{3},{4})", (object) UID, (object) OID, (object) tblBadgeMaster.id_badge, (object) DateTime.Now, (object) num11);
                      int num12 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
                      int num13 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_currency={1}", (object) tblBadgeMaster.id_badge, (object) num12).FirstOrDefault<int>();
                      m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_currency_log (id_user,id_game,currency_value,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) UID, (object) num11, (object) num13, (object) "A", (object) DateTime.Now);
                      string type = "2";
                      string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select badge_logo from tbl_badge_master where id_badge={0}", (object) tblBadgeMaster.id_badge).FirstOrDefault<string>();
                      tbl_badge_won_message tblBadgeWonMessage1 = new tbl_badge_won_message();
                      tbl_badge_won_message tblBadgeWonMessage2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_won_message>("select * from tbl_badge_won_message where id_game={0} and id_badge={1} ", (object) num11, (object) tblBadgeMaster.id_badge).FirstOrDefault<tbl_badge_won_message>();
                      if (list5 != null && tblBadgeWonMessage2 != null)
                      {
                        int currency = m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_game={1}", (object) tblBadgeMaster.id_badge, (object) num11).FirstOrDefault<int>();
                        string title = ConfigurationManager.AppSettings["title_fcm_badge"].ToString();
                        string image = ConfigurationManager.AppSettings["title_fcm_badge"].ToString() + str;
                        foreach (tbl_user_gcm_log tblUserGcmLog in list5)
                          new UniversityScoringlogic().SendNotification(tblUserGcmLog.GCMID, tblBadgeWonMessage2.message, type, image, Convert.ToString(tblBadgeMaster.eligiblescore), currency, tblBadgeMaster.badge_name, title);
                      }
                    }
                  }
                }
                briefReturnResponse.AssessmetGameScore = assessmentScoreResponseList1;
                tbl_user_league_log tblUserLeagueLog1 = new tbl_user_league_log();
                List<tbl_theme_leagues> tblThemeLeaguesList = new List<tbl_theme_leagues>();
                if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_theme_leagues>("select * from tbl_theme_leagues where id_theme={0} ", (object) 9).ToList<tbl_theme_leagues>() != null)
                {
                  List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
                  LeagueInstance leagueInstance = new LeagueInstance();
                  List<tbl_leagues_data> list6 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0} ", (object) num11).ToList<tbl_leagues_data>();
                  int num14 = 1;
                  foreach (tbl_leagues_data tblLeaguesData in list6)
                  {
                    if ((double) int32 <= tblLeaguesData.minscore)
                    {
                      leagueInstance.id_league = tblLeaguesData.id_league;
                      leagueInstance.id_user = UID;
                    }
                    if (num14 == list6.Count && (double) int32 > tblLeaguesData.minscore)
                    {
                      leagueInstance.id_league = tblLeaguesData.id_league;
                      leagueInstance.id_user = UID;
                    }
                    ++num14;
                  }
                  tbl_user_league_log tblUserLeagueLog2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_league_log>("select * from tbl_user_league_log where id_user={0} and id_game={1} and status={2} ", (object) UID, (object) num11, (object) "A").FirstOrDefault<tbl_user_league_log>();
                  if (tblUserLeagueLog2 != null)
                  {
                    if (leagueInstance != null)
                    {
                      if (tblUserLeagueLog2.id_league != leagueInstance.id_league)
                      {
                        string eligiblescore = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) leagueInstance.id_league).FirstOrDefault<string>();
                        m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_user_league_log  set id_league={0} , league={1} where id_user={2} and id_game={3}", (object) leagueInstance.id_league, (object) eligiblescore, (object) UID, (object) num11);
                        tbl_league_message tblLeagueMessage1 = new tbl_league_message();
                        tbl_league_message tblLeagueMessage2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_league_message>("select * from tbl_league_message where id_game={0} and id_league={1} ", (object) num11, (object) leagueInstance.id_league).FirstOrDefault<tbl_league_message>();
                        if (list5 != null)
                        {
                          if (tblLeagueMessage2 != null)
                          {
                            string title = ConfigurationManager.AppSettings["title_fcm_league"].ToString();
                            foreach (tbl_user_gcm_log tblUserGcmLog in list5)
                              new UniversityScoringlogic().SendNotification(tblUserGcmLog.GCMID, tblLeagueMessage2.message, "3", "", eligiblescore, tag: "", title: title);
                          }
                        }
                      }
                    }
                  }
                  else if (leagueInstance != null)
                  {
                    string eligiblescore = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) leagueInstance.id_league).FirstOrDefault<string>();
                    m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_league_log (id_user,id_org,id_game,id_league,league,status,updated_date_time) values({0},{1},{2},{3},{4},{5},{6})", (object) UID, (object) OID, (object) num11, (object) leagueInstance.id_league, (object) eligiblescore, (object) "A", (object) DateTime.Now);
                    tbl_league_message tblLeagueMessage3 = new tbl_league_message();
                    tbl_league_message tblLeagueMessage4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_league_message>("select * from tbl_league_message where id_game={0} and id_league={1} ", (object) num11, (object) leagueInstance.id_league).FirstOrDefault<tbl_league_message>();
                    foreach (tbl_user_gcm_log tblUserGcmLog in list5)
                      new UniversityScoringlogic().SendNotification(tblUserGcmLog.GCMID, tblLeagueMessage4.message, "3", "", eligiblescore);
                  }
                }
              }
            }
          }
        }
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
