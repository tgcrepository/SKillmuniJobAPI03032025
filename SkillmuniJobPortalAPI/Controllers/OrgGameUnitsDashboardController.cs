// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameUnitsDashboardController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class OrgGameUnitsDashboardController : ApiController
  {
    public HttpResponseMessage Get(
      int UID,
      int OID,
      int id_org_game,
      int id_org_game_unit,
      string UserFunction)
    {
      OrgGameLeaderBoardResponse leaderBoardResponse = new OrgGameLeaderBoardResponse();
      List<GameUserLog> gameUserLogList = new List<GameUserLog>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT count(id_game_content) FROM tbl_org_game_content inner join tbl_org_game_level_mapping on tbl_org_game_content.id_level=tbl_org_game_level_mapping.id_level where tbl_org_game_level_mapping.id_org_game={0}", (object) id_org_game).FirstOrDefault<int>();
          tbl_org_game_master tblOrgGameMaster = new tbl_org_game_master();
          tblOrgGameMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_master>("select * from tbl_org_game_master where id_org_game={0} and  status='A'", (object) id_org_game).FirstOrDefault<tbl_org_game_master>();
          if (UserFunction == "CENTRAL")
          {
            List<tbl_org_game_unit_master> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where id_org={0} and status={1} and unit_type={2} ", (object) OID, (object) "A", (object) 1).ToList<tbl_org_game_unit_master>();
            List<UnitRankLog> source = new List<UnitRankLog>();
            foreach (tbl_org_game_unit_master orgGameUnitMaster in list1)
            {
              UnitRankLog unitRankLog = new UnitRankLog();
              GameUserLog gameUserLog = new GameUserLog()
              {
                final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 1 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function={3} and tbl_org_game_user_assessment_log.attempt_no={4} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL", (object) 1).FirstOrDefault<int>(),
                final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 0 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function={3} and tbl_org_game_user_assessment_log.attempt_no={4} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL", (object) 1).FirstOrDefault<int>()
              };
              gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
              if (gameUserLog.final_assessmnet_total_count > 0)
                gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
              int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT( tbl_user.ID_USER) AS usercnt FROM tbl_user JOIN tbl_profile ON tbl_user.ID_USER = tbl_profile.ID_USER WHERE tbl_user.ID_ORGANIZATION = {0} AND tbl_user.user_function = {1} AND tbl_profile.id_org_game_unit = {2} and tbl_user.STATUS='A'", (object) OID, (object) "CENTRAL", (object) orgGameUnitMaster.id_org_game_unit).FirstOrDefault<int>();
              unitRankLog.AverageScore = gameUserLog.assessment_score;
              unitRankLog.AverageScore = Math.Round(unitRankLog.AverageScore, 2);
              int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(tbl_org_game_user_log.id_log) AS logcnt  FROM tbl_org_game_user_log INNER JOIN tbl_user ON tbl_org_game_user_log.id_user = tbl_user.ID_USER INNER JOIN tbl_profile ON tbl_profile.ID_USER = tbl_org_game_user_log.id_user where tbl_org_game_user_log.is_completed=1 and tbl_user.STATUS='A' and tbl_profile.id_org_game_unit={0} and tbl_org_game_user_log.id_org_game={1} and tbl_org_game_user_log.is_completed={2} and tbl_org_game_user_log.id_level={3} and  tbl_user.user_function={4}", (object) orgGameUnitMaster.id_org_game_unit, (object) id_org_game, (object) 1, (object) 5, (object) "CENTRAL").FirstOrDefault<int>();
              num1 *= num2;
              unitRankLog.CompletionPercentage = num2 <= 0 ? 0.0 : Convert.ToDouble(num3) / Convert.ToDouble(num2) * 100.0;
              unitRankLog.CompletionPercentage = Math.Round(unitRankLog.CompletionPercentage, 2);
              unitRankLog.RankPercentage = (unitRankLog.CompletionPercentage + unitRankLog.AverageScore) / 2.0;
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source.Add(unitRankLog);
            }
            List<UnitRankLog> list2 = source.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.RankPercentage)).ToList<UnitRankLog>();
            int num4 = 1;
            foreach (UnitRankLog unitRankLog in list2)
            {
              unitRankLog.Rank = num4;
              ++num4;
            }
            leaderBoardResponse.CENTRALUnits = list2;
          }
          if (UserFunction != "CENTRAL")
          {
            List<tbl_org_game_unit_master> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where id_org={0} and status={1} and unit_type={2} ", (object) OID, (object) "A", (object) 2).ToList<tbl_org_game_unit_master>();
            List<UnitRankLog> source = new List<UnitRankLog>();
            foreach (tbl_org_game_unit_master orgGameUnitMaster in list3)
            {
              UnitRankLog unitRankLog = new UnitRankLog();
              GameUserLog gameUserLog = new GameUserLog()
              {
                final_assessmnet_right_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 1 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function!={3} and tbl_org_game_user_assessment_log.attempt_no={4} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL", (object) 1).FirstOrDefault<int>(),
                final_assessmnet_wrong_count = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(is_correct) total FROM tbl_org_game_user_assessment_log INNER JOIN tbl_profile ON tbl_org_game_user_assessment_log.id_user = tbl_profile.ID_USER INNER JOIN tbl_user ON tbl_user.ID_USER = tbl_org_game_user_assessment_log.id_user WHERE tbl_org_game_user_assessment_log.id_org_game = {0} AND tbl_org_game_user_assessment_log.is_correct = 0 AND tbl_profile.id_org_game_unit = {1} AND tbl_user.ID_ORGANIZATION = {2} and tbl_user.user_function!={3} and tbl_org_game_user_assessment_log.attempt_no={4} ", (object) id_org_game, (object) orgGameUnitMaster.id_org_game_unit, (object) OID, (object) "CENTRAL", (object) 1).FirstOrDefault<int>()
              };
              gameUserLog.final_assessmnet_total_count = gameUserLog.final_assessmnet_right_count + gameUserLog.final_assessmnet_wrong_count;
              if (gameUserLog.final_assessmnet_total_count > 0)
                gameUserLog.assessment_score = Convert.ToDouble(gameUserLog.final_assessmnet_right_count) / Convert.ToDouble(gameUserLog.final_assessmnet_total_count) * 100.0;
              int num5 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT( tbl_user.ID_USER) AS usercnt FROM tbl_user JOIN tbl_profile ON tbl_user.ID_USER = tbl_profile.ID_USER WHERE tbl_user.ID_ORGANIZATION = {0} AND tbl_user.user_function != {1} AND tbl_profile.id_org_game_unit = {2} and tbl_user.STATUS='A'", (object) OID, (object) "CENTRAL", (object) orgGameUnitMaster.id_org_game_unit).FirstOrDefault<int>();
              unitRankLog.AverageScore = gameUserLog.assessment_score;
              unitRankLog.AverageScore = Math.Round(unitRankLog.AverageScore, 2);
              int num6 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COUNT(tbl_org_game_user_log.id_log) AS logcnt  FROM tbl_org_game_user_log INNER JOIN tbl_user ON tbl_org_game_user_log.id_user = tbl_user.ID_USER INNER JOIN tbl_profile ON tbl_profile.ID_USER = tbl_org_game_user_log.id_user where tbl_org_game_user_log.is_completed=1 and tbl_user.STATUS='A' and tbl_profile.id_org_game_unit={0} and tbl_org_game_user_log.id_org_game={1} and tbl_org_game_user_log.is_completed={2} and tbl_org_game_user_log.id_level={3} and  tbl_user.user_function!={4}", (object) orgGameUnitMaster.id_org_game_unit, (object) id_org_game, (object) 1, (object) 5, (object) "CENTRAL").FirstOrDefault<int>();
              num1 *= num5;
              unitRankLog.CompletionPercentage = num5 <= 0 ? 0.0 : Convert.ToDouble(num6) / Convert.ToDouble(num5) * 100.0;
              unitRankLog.CompletionPercentage = Math.Round(unitRankLog.CompletionPercentage, 2);
              unitRankLog.RankPercentage = (unitRankLog.CompletionPercentage + unitRankLog.AverageScore) / 2.0;
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source.Add(unitRankLog);
            }
            List<UnitRankLog> list4 = source.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.RankPercentage)).ToList<UnitRankLog>();
            int num7 = 1;
            foreach (UnitRankLog unitRankLog in list4)
            {
              unitRankLog.Rank = num7;
              ++num7;
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
