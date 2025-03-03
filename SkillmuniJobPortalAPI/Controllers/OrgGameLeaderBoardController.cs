// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameLeaderBoardController
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
    public class OrgGameLeaderBoardController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int id_org_game,
      int id_org_game_unit,
      string UserFunction)
    {
      OrgGameLeaderBoardResponse leaderBoardResponse = new OrgGameLeaderBoardResponse();
      List<GameUserLog> source1 = new List<GameUserLog>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT count(id_game_content) FROM tbl_org_game_content inner join tbl_org_game_level_mapping on tbl_org_game_content.id_level=tbl_org_game_level_mapping.id_level where tbl_org_game_level_mapping.id_org_game={0}", (object) id_org_game).FirstOrDefault<int>();
          tbl_org_game_master tblOrgGameMaster1 = new tbl_org_game_master();
          tbl_org_game_master tblOrgGameMaster2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_master>("select * from tbl_org_game_master where id_org_game={0} and  status='A'", (object) id_org_game).FirstOrDefault<tbl_org_game_master>();
          List<tbl_user> tblUserList = new List<tbl_user>();
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[3]
          {
            (object) OID,
            (object) "A",
            (object) UserFunction
          };
          foreach (tbl_user tblUser in database.SqlQuery<tbl_user>("select * from tbl_user where ID_ORGANIZATION={0} and STATUS={1} and user_function={2} ", objArray).ToList<tbl_user>())
          {
            GameUserLog gameUserLog = new GameUserLog()
            {
              total_score_gained = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=1 and tbl_org_game_user_log.id_level={2}", (object) tblUser.ID_USER, (object) id_org_game, (object) 5).FirstOrDefault<int>(),
              total_score_detected = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_org_game_user_log where id_user={0} and id_org_game={1} and score_type=2 and tbl_org_game_user_log.id_level={2}", (object) tblUser.ID_USER, (object) id_org_game, (object) 5).FirstOrDefault<int>(),
              final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(is_correct),0) total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=1 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>(),
              final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count( is_correct) as total from tbl_org_game_user_assessment_log where id_user={0} and id_org_game={1} and is_correct=0 ", (object) tblUser.ID_USER, (object) id_org_game).FirstOrDefault<int>()
            };
            gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
            if (gameUserLog.final_assessmnet_total_count > 0)
              gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
            gameUserLog.id_org_game = tblOrgGameMaster2.id_org_game;
            gameUserLog.game_title = tblOrgGameMaster2.title;
            gameUserLog.current_overallscore = gameUserLog.total_score_gained - gameUserLog.total_score_detected;
            tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            if (tblProfile != null)
            {
              gameUserLog.Name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              gameUserLog.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile.PROFILE_IMAGE;
            }
            source1.Add(gameUserLog);
          }
          List<GameUserLog> list1 = source1.OrderByDescending<GameUserLog, double>((Func<GameUserLog, double>) (x => x.assessment_score)).ToList<GameUserLog>();
          int num2 = 1;
          foreach (GameUserLog gameUserLog in list1)
          {
            gameUserLog.rank = num2;
            ++num2;
          }
          leaderBoardResponse.OverAll = list1;
          List<tbl_org_game_unit_master> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where id_org={0} and status={1}  ", (object) OID, (object) "A").ToList<tbl_org_game_unit_master>();
          if (UserFunction == "CENTRAL")
          {
            List<UnitRankLog> source2 = new List<UnitRankLog>();
            foreach (tbl_org_game_unit_master orgGameUnitMaster in list2)
            {
              UnitRankLog unitRankLog = new UnitRankLog();
              GameUserLog gameUserLog = new GameUserLog()
              {
                final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 1 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function={3} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>(),
                final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 0 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function={3} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>()
              };
              gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
              if (gameUserLog.final_assessmnet_total_count > 0)
                gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
              int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(DISTINCT tbl_org_game_user_assessment_log.id_user) AS usercnt FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER inner join tbl_user on tbl_org_game_user_assessment_log.id_user=tbl_user.ID_USER WHERE tbl_user.STATUS = 'A' AND tbl_profile.id_org_game_unit = {0} AND tbl_user.ID_ORGANIZATION = {1} and tbl_user.user_function={2}", (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>();
              unitRankLog.AverageScore = gameUserLog.assessment_score;
              unitRankLog.AverageScore = Math.Round(unitRankLog.AverageScore, 2);
              int num4 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(tbl_org_game_user_log.id_log) AS logcnt  FROM tbl_org_game_user_log INNER JOIN tbl_user ON tbl_org_game_user_log.id_user = tbl_user.ID_USER INNER JOIN tbl_profile ON tbl_profile.ID_USER = tbl_org_game_user_log.id_user where tbl_org_game_user_log.is_completed=1 and tbl_user.STATUS='A' and tbl_profile.id_org_game_unit={0} and tbl_org_game_user_log.id_org_game={1} and tbl_org_game_user_log.is_completed={2} and tbl_org_game_user_log.id_level={3} and  tbl_user.user_function={4}", (object) id_org_game_unit, (object) id_org_game, (object) 1, (object) 5, (object) "CENTRAL").FirstOrDefault<int>();
              num1 *= num3;
              unitRankLog.CompletionPercentage = num3 <= 0 ? 0.0 : Convert.ToDouble(num4) / Convert.ToDouble(num3) * 100.0;
              unitRankLog.CompletionPercentage = Math.Round(unitRankLog.CompletionPercentage, 2);
              unitRankLog.RankPercentage = (unitRankLog.CompletionPercentage + unitRankLog.AverageScore) / 2.0;
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source2.Add(unitRankLog);
            }
            List<UnitRankLog> list3 = source2.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.RankPercentage)).ToList<UnitRankLog>();
            int num5 = 1;
            foreach (UnitRankLog unitRankLog in list3)
            {
              unitRankLog.Rank = num5;
              ++num5;
            }
            leaderBoardResponse.CENTRALUnits = list3;
          }
          if (UserFunction != "CENTRAL")
          {
            List<UnitRankLog> source3 = new List<UnitRankLog>();
            foreach (tbl_org_game_unit_master orgGameUnitMaster in list2)
            {
              UnitRankLog unitRankLog = new UnitRankLog();
              GameUserLog gameUserLog = new GameUserLog()
              {
                final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 1 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function!={3} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>(),
                final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 0 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function!={3} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>()
              };
              gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
              if (gameUserLog.final_assessmnet_total_count > 0)
                gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
              int num6 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(DISTINCT tbl_org_game_user_assessment_log.id_user) AS usercnt FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER inner join tbl_user on tbl_org_game_user_assessment_log.id_user=tbl_user.ID_USER WHERE tbl_user.STATUS = 'A' AND tbl_profile.id_org_game_unit = {0} AND tbl_user.ID_ORGANIZATION = {1} and tbl_user.user_function!={2}", (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL").FirstOrDefault<int>();
              unitRankLog.AverageScore = gameUserLog.assessment_score;
              unitRankLog.AverageScore = Math.Round(unitRankLog.AverageScore, 2);
              int num7 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(tbl_org_game_user_log.id_log) AS logcnt  FROM tbl_org_game_user_log INNER JOIN tbl_user ON tbl_org_game_user_log.id_user = tbl_user.ID_USER INNER JOIN tbl_profile ON tbl_profile.ID_USER = tbl_org_game_user_log.id_user where tbl_org_game_user_log.is_completed=1 and tbl_user.STATUS='A' and tbl_profile.id_org_game_unit={0} and tbl_org_game_user_log.id_org_game={1} and tbl_org_game_user_log.is_completed={2} and tbl_org_game_user_log.id_level={3} and  tbl_user.user_function!={4}", (object) id_org_game_unit, (object) id_org_game, (object) 1, (object) 5, (object) "CENTRAL").FirstOrDefault<int>();
              num1 *= num6;
              unitRankLog.CompletionPercentage = num6 <= 0 ? 0.0 : Convert.ToDouble(num7) / Convert.ToDouble(num6) * 100.0;
              unitRankLog.CompletionPercentage = Math.Round(unitRankLog.CompletionPercentage, 2);
              unitRankLog.RankPercentage = (unitRankLog.CompletionPercentage + unitRankLog.AverageScore) / 2.0;
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source3.Add(unitRankLog);
            }
            List<UnitRankLog> list4 = source3.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.RankPercentage)).ToList<UnitRankLog>();
            int num8 = 1;
            foreach (UnitRankLog unitRankLog in list4)
            {
              unitRankLog.Rank = num8;
              ++num8;
            }
            leaderBoardResponse.NONCENTRALUnits = list4;
          }
        }
        leaderBoardResponse.STATUS = "SUCCESS";
        leaderBoardResponse.MESSAGE = "Data retrived successfully.";
      }
      catch (Exception ex)
      {
        leaderBoardResponse.STATUS = "FAILED";
        leaderBoardResponse.MESSAGE = "Something went wrong.";
      }
      return namespace2.CreateResponse<OrgGameLeaderBoardResponse>(this.Request, HttpStatusCode.OK, leaderBoardResponse);
    }
  }
}
