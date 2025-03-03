// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameUserDashboardController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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

    public class OrgGameUserDashboardController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int id_org_game,
      string UserFunction,
      int id_org_game_unit)
    {
      List<LevelUserLogResponse> levelUserLogResponseList = new List<LevelUserLogResponse>();
      OrgGameUserDashboardResult userDashboardResult = new OrgGameUserDashboardResult();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          userDashboardResult.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=1", (object) UID, (object) id_org_game).FirstOrDefault<int>();
          userDashboardResult.detucted_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=2", (object) UID, (object) id_org_game).FirstOrDefault<int>();
          userDashboardResult.current_score = userDashboardResult.total_score - userDashboardResult.detucted_score;
          tbl_org_game_master tblOrgGameMaster1 = new tbl_org_game_master();
          tbl_org_game_master tblOrgGameMaster2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_master>("select * from tbl_org_game_master where id_org_game={0} and  status='A'", (object) id_org_game).FirstOrDefault<tbl_org_game_master>();
          List<tbl_org_game_level_mapping> gameLevelMappingList = new List<tbl_org_game_level_mapping>();
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) id_org_game
          };
          foreach (tbl_org_game_level_mapping gameLevelMapping in database.SqlQuery<tbl_org_game_level_mapping>("select * from tbl_org_game_level_mapping where id_org_game={0} and status='A'", objArray).ToList<tbl_org_game_level_mapping>())
          {
            LevelUserLogResponse levelUserLogResponse = new LevelUserLogResponse();
            levelUserLogResponse.content = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_content>("select * from tbl_org_game_content inner join tbl_org_game_level_mapping on tbl_org_game_content.id_level=tbl_org_game_level_mapping.id_level  where tbl_org_game_content.id_level={0} and tbl_org_game_level_mapping.id_org_game={1}", (object) gameLevelMapping.id_level, (object) id_org_game).ToList<tbl_org_game_content>();
            levelUserLogResponse.UID = UID;
            levelUserLogResponse.OID = OID;
            levelUserLogResponse.id_game = id_org_game;
            levelUserLogResponse.id_level = gameLevelMapping.id_level;
            int num = 0;
            foreach (tbl_org_game_content tblOrgGameContent in levelUserLogResponse.content)
            {
              tblOrgGameContent.user_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("select * from tbl_org_game_user_log where id_game_content={0} and id_user={1} and id_org_game={2} and id_level={3}", (object) tblOrgGameContent.id_game_content, (object) UID, (object) id_org_game, (object) gameLevelMapping.id_level).ToList<tbl_org_game_user_log>();
              foreach (tbl_org_game_user_log tblOrgGameUserLog in tblOrgGameContent.user_log)
              {
                if (tblOrgGameUserLog.is_completed == 1)
                  ++num;
              }
              if (tblOrgGameContent.user_log != null)
              {
                tblOrgGameContent.badge_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_badge_master>("select * from tbl_badge_master inner join tbl_org_game_content_badge_mapping on tbl_badge_master.id_badge=tbl_org_game_content_badge_mapping.id_badge where tbl_org_game_content_badge_mapping.id_content={0} and tbl_org_game_content_badge_mapping.id_game={1} and id_level={2}", (object) tblOrgGameContent.id_game_content, (object) id_org_game, (object) gameLevelMapping.id_level).FirstOrDefault<tbl_org_game_badge_master>();
                if (tblOrgGameContent.badge_log != null)
                {
                  tblOrgGameContent.badge_log.badge_count = m2ostnextserviceDbContext.Database.SqlQuery<int>(" select COUNT(id_log) AS total from tbl_org_game_badge_user_log where id_user = {0} and id_content = {1} and id_game = {2} and id_level={3}", (object) UID, (object) tblOrgGameContent.id_game_content, (object) id_org_game, (object) gameLevelMapping.id_level).FirstOrDefault<int>();
                  tblOrgGameContent.badge_log.is_achieved = 0;
                  if (tblOrgGameContent.badge_log.badge_count > 0)
                    tblOrgGameContent.badge_log.is_achieved = 1;
                }
              }
            }
            levelUserLogResponse.level_badge_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_badge_master>("select * from tbl_badge_master inner join tbl_org_game_badge_level_mapping on tbl_badge_master.id_badge=tbl_org_game_badge_level_mapping.id_badge where tbl_org_game_badge_level_mapping.id_level={0} and tbl_org_game_badge_level_mapping.id_org_game={1} ", (object) gameLevelMapping.id_level, (object) id_org_game).FirstOrDefault<tbl_org_game_badge_master>();
            if (levelUserLogResponse.level_badge_log != null)
            {
              levelUserLogResponse.level_badge_log.badge_count = m2ostnextserviceDbContext.Database.SqlQuery<int>(" select COUNT(id_log) AS total from tbl_org_game_badge_user_log where id_user = {0} and id_content = {1} and id_game = {2} and id_level={3}", (object) UID, (object) 0, (object) id_org_game, (object) gameLevelMapping.id_level).FirstOrDefault<int>();
              levelUserLogResponse.level_badge_log.is_achieved = 0;
              if (levelUserLogResponse.level_badge_log.badge_count > 0)
                levelUserLogResponse.level_badge_log.is_achieved = 1;
            }
            if (levelUserLogResponse.content.Count == num)
              levelUserLogResponse.is_level_completed = 1;
            levelUserLogResponseList.Add(levelUserLogResponse);
          }
          List<tbl_user> tblUserList = new List<tbl_user>();
          List<tbl_user> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user as a inner join tbl_org_game_user_log as b on a.ID_USER=b.id_user where a.ID_ORGANIZATION={0} and a.STATUS='A' and a.user_function={1} and b.attempt_no=1 and b.id_level=5", (object) OID, (object) UserFunction).ToList<tbl_user>();
          List<GameUserLog> source = new List<GameUserLog>();
          foreach (tbl_user tblUser in list1)
          {
            GameUserLog gameUserLog = new GameUserLog()
            {
              final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(is_correct),0) total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=1 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>(),
              final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count( is_correct) as total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=0 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>()
            };
            gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
            if (gameUserLog.final_assessmnet_total_count > 0)
              gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
            gameUserLog.id_user = tblUser.ID_USER;
            gameUserLog.id_org_game = tblOrgGameMaster2.id_org_game;
            gameUserLog.game_title = tblOrgGameMaster2.title;
            gameUserLog.current_overallscore = gameUserLog.total_score_gained - gameUserLog.total_score_detected;
            tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            if (tblProfile != null)
            {
              gameUserLog.Name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              gameUserLog.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
            }
            source.Add(gameUserLog);
          }
          List<GameUserLog> list2 = source.OrderByDescending<GameUserLog, double>((Func<GameUserLog, double>) (x => x.assessment_score)).ToList<GameUserLog>();
          int num1 = 1;
          foreach (GameUserLog gameUserLog in list2)
          {
            gameUserLog.rank = num1;
            ++num1;
          }
          GameUserLog gameUserLog1 = list2.Find((Predicate<GameUserLog>) (p => p.id_user == UID));
          if (gameUserLog1 != null)
          {
            userDashboardResult.OverAllRank = gameUserLog1.rank;
            List<tbl_user> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user where ID_ORGANIZATION={0} and STATUS={1} and user_function={2} ", (object) OID, (object) "A", (object) UserFunction).ToList<tbl_user>();
            userDashboardResult.OverAllRankTotal = list3.Count<tbl_user>();
          }
          else
          {
            List<tbl_user> list4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user where ID_ORGANIZATION={0} and STATUS={1} and user_function={2} ", (object) OID, (object) "A", (object) UserFunction).ToList<tbl_user>();
            userDashboardResult.OverAllRankTotal = list4.Count<tbl_user>();
          }
          List<tbl_user> list5 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user inner join tbl_profile on tbl_user.ID_USER=tbl_profile.ID_USER inner join tbl_org_game_user_log  on tbl_user.ID_USER=tbl_org_game_user_log.id_user  where tbl_user.ID_ORGANIZATION={0} and tbl_user.STATUS={1} and tbl_user.user_function={2} and tbl_profile.id_org_game_unit={3} and  tbl_org_game_user_log.attempt_no=1 and tbl_org_game_user_log.id_level=5 ", (object) OID, (object) "A", (object) UserFunction, (object) id_org_game_unit).ToList<tbl_user>();
          List<GameUserLog> gameUserLogList = new List<GameUserLog>();
          foreach (tbl_user tblUser in list5)
          {
            GameUserLog gameUserLog2 = new GameUserLog()
            {
              final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(is_correct),0) total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=1 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>(),
              final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count( is_correct) as total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=0 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>()
            };
            gameUserLog2.final_assessmnet_total_count = gameUserLog2.final_assessmnet_right_count + gameUserLog2.final_assessmnet_wrong_count;
            if (gameUserLog2.final_assessmnet_total_count > 0)
              gameUserLog2.assessment_score = Convert.ToDouble(gameUserLog2.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog2.final_assessmnet_total_count) * 100.0;
            gameUserLog2.id_user = tblUser.ID_USER;
            gameUserLog2.id_org_game = tblOrgGameMaster2.id_org_game;
            gameUserLog2.game_title = tblOrgGameMaster2.title;
            gameUserLog2.current_overallscore = gameUserLog2.total_score_gained - gameUserLog2.total_score_detected;
            tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            if (tblProfile != null)
            {
              gameUserLog2.Name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              gameUserLog2.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
            }
            gameUserLogList.Add(gameUserLog2);
          }
          list2.OrderByDescending<GameUserLog, double>((Func<GameUserLog, double>) (x => x.assessment_score)).ToList<GameUserLog>();
          int num2 = 1;
          foreach (GameUserLog gameUserLog3 in gameUserLogList)
          {
            gameUserLog3.rank = num2;
            ++num2;
          }
          if (gameUserLogList.Find((Predicate<GameUserLog>) (p => p.id_user == UID)) != null)
          {
            userDashboardResult.UnitRank = gameUserLog1.rank;
            List<tbl_user> list6 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user inner join tbl_profile on tbl_user.ID_USER=tbl_profile.ID_USER   where tbl_user.ID_ORGANIZATION={0} and tbl_user.STATUS={1} and tbl_user.user_function={2} and tbl_profile.id_org_game_unit={3} ", (object) OID, (object) "A", (object) UserFunction, (object) id_org_game_unit).ToList<tbl_user>();
            userDashboardResult.UnitRankTotal = list6.Count<tbl_user>();
          }
          else
          {
            List<tbl_user> list7 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user inner join tbl_profile on tbl_user.ID_USER=tbl_profile.ID_USER   where tbl_user.ID_ORGANIZATION={0} and tbl_user.STATUS={1} and tbl_user.user_function={2} and tbl_profile.id_org_game_unit={3} ", (object) OID, (object) "A", (object) UserFunction, (object) id_org_game_unit).ToList<tbl_user>();
            userDashboardResult.UnitRankTotal = list7.Count<tbl_user>();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      userDashboardResult.LevelUserLog = levelUserLogResponseList;
      return namespace2.CreateResponse<OrgGameUserDashboardResult>(this.Request, HttpStatusCode.OK, userDashboardResult);
    }
  }
}
