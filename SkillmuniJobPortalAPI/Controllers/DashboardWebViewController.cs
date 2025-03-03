// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.DashboardWebViewController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace m2ostnextservice.Controllers
{
  public class DashboardWebViewController : Controller
  {
    private static Random random = new Random();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult FootballLeaderBoard(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        string str = "";
        tbl_profile tblProfile = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
        List<tbl_user_badge_log> tblUserBadgeLogList = new List<tbl_user_badge_log>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_game_master where id_game={0}", (object) id_game).FirstOrDefault<string>();
          tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          tblBadgeMasterList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
          tblUserBadgeLogList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
        }
        List<tbl_leagues_data> list = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
        leaderBoardResponse.Badge = new UniversityScoringlogic().getBadgeList(UID, id_game);
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList = new LeaderBoardUserList();
        leaderBoardResponse.userscore = leaderBoardUserList.metric_score;
        int count = list.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 0;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
          m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
        }
        FootballThemeLeaderBoardHeader leaderBoardHeader = new FootballThemeLeaderBoardHeader();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (tblProfile.social_dp_flag == 0)
            leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          else if (tblProfile.PROFILE_IMAGE == "null")
            leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileDefaultBase"];
          else
            leaderBoardResponse.UserProfileImage = m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardHeader.currency = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_metric from tbl_university_special_point_grid where id_game={0}", (object) id_game).FirstOrDefault<int>();
          leaderBoardHeader.specialmetric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_special_metric_master where id_special_metric={0}", (object) num2).FirstOrDefault<string>();
          leaderBoardHeader.theme_metric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select metric_value from tbl_theme_metric where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardHeader.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          if (leaderBoardResponse.userleague == null)
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
        }
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in tblUserBadgeLogList)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
            }
          }
        }
        this.ViewData["Leader"] = (object) leaderBoardResponse;
        this.ViewData["Header"] = (object) leaderBoardHeader;
        this.ViewData[nameof (UID)] = (object) UID;
        this.ViewData[nameof (OID)] = (object) OID;
        this.ViewData["badgemaster"] = (object) tblBadgeMasterList;
        this.ViewData["gamename"] = (object) str;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult ProfileHome(int UID, int OID)
    {
      tbl_profile tblProfile = new tbl_profile();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
      UserScoreResponse userScoreResponse = JsonConvert.DeserializeObject<UserScoreResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "getUserScore?UID=" + UID.ToString() + "&OID=" + OID.ToString()));
      tblProfile.PROFILE_IMAGE = ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile.PROFILE_IMAGE;
      tblProfile.FIRSTNAME = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
      this.ViewData["profile"] = (object) tblProfile;
      this.ViewData[nameof (UID)] = (object) UID;
      this.ViewData[nameof (OID)] = (object) OID;
      this.ViewData["briefData"] = (object) userScoreResponse;
      return (ActionResult) this.View();
    }

    public ActionResult AssessmentSheet(
      string brfcode,
      int UID,
      int OID,
      int ACID,
      int BriefTileID = 0)
    {
      try
      {
        int OID1 = OID;
        int UID1 = UID;
        string brf = brfcode;
        UserScoreResponse userScoreResponse = new UserScoreResponse();
        BriefResource briefData = new BriefModel().getBriefData(brf, UID1, OID1);
        string[] strArray = new string[5];
        List<BriefChart> briefChartList1 = new List<BriefChart>();
        List<BriefChart> briefChartList2 = new List<BriefChart>();
        if (briefData.RESULTSTATUS == 1)
        {
          userScoreResponse = JsonConvert.DeserializeObject<UserScoreResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "getUserScore?UID=" + UID.ToString() + "&OID=" + OID.ToString()));
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_user_game_special_metric_score_log specialMetricScoreLog = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_game_special_metric_score_log>("select * from tbl_user_game_special_metric_score_log where id_brief={0} and id_user={1}", (object) briefData.BRIEF.id_brief_master, (object) UID).FirstOrDefault<tbl_user_game_special_metric_score_log>();
            if (specialMetricScoreLog != null)
              briefData.SplScore = specialMetricScoreLog.score;
            foreach (BriefUserInput briefUserInput in briefData.RESULT.briefReturn)
            {
              briefUserInput.questiontheme = m2ostnextserviceDbContext.Database.SqlQuery<int>("select question_theme_type from tbl_brief_question where id_brief_question={0}", (object) briefUserInput.id_question).FirstOrDefault<int>();
              if (briefUserInput.questiontheme == 2)
              {
                briefUserInput.questionchoicetype = m2ostnextserviceDbContext.Database.SqlQuery<int>("select question_choice_type from tbl_brief_question where id_brief_question={0}", (object) briefUserInput.id_question).FirstOrDefault<int>();
                briefUserInput.answerchoicetype = m2ostnextserviceDbContext.Database.SqlQuery<int>("select choice_type from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_answer).FirstOrDefault<int>();
                if (briefUserInput.id_wans > 0)
                  briefUserInput.wanschoicetype = m2ostnextserviceDbContext.Database.SqlQuery<int>("select choice_type from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_wans).FirstOrDefault<int>();
                if (briefUserInput.questionchoicetype != 1)
                {
                  if (briefUserInput.questionchoicetype == 2)
                    briefUserInput.questionimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select question_image from tbl_brief_question where id_brief_question={0}", (object) briefUserInput.id_question).FirstOrDefault<string>();
                  else if (briefUserInput.questionchoicetype == 3)
                    briefUserInput.questionimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select question_image from tbl_brief_question where id_brief_question={0}", (object) briefUserInput.id_question).FirstOrDefault<string>();
                }
                if (briefUserInput.answerchoicetype != 1)
                {
                  if (briefUserInput.answerchoicetype == 2)
                    briefUserInput.answerimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select choice_image from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_answer).FirstOrDefault<string>();
                  else if (briefUserInput.answerchoicetype == 3)
                    briefUserInput.answerimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select choice_image from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_answer).FirstOrDefault<string>();
                }
                if (briefUserInput.id_wans > 0)
                {
                  if (briefUserInput.wanschoicetype == 2)
                    briefUserInput.wansimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select choice_image from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_wans).FirstOrDefault<string>();
                  else if (briefUserInput.wanschoicetype == 3)
                    briefUserInput.wansimg = m2ostnextserviceDbContext.Database.SqlQuery<string>("select choice_image from tbl_brief_answer where id_brief_answer={0}", (object) briefUserInput.id_wans).FirstOrDefault<string>();
                }
              }
            }
          }
        }
        this.ViewData["right"] = (object) briefChartList1;
        this.ViewData["wrong"] = (object) briefChartList2;
        this.ViewData["brief"] = (object) briefData;
        this.ViewData[nameof (UID)] = (object) UID;
        this.ViewData[nameof (OID)] = (object) OID;
        this.ViewData[nameof (ACID)] = (object) ACID;
        this.ViewData["BTileId"] = (object) BriefTileID;
        this.ViewData["scoreres"] = (object) userScoreResponse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public string AssessmentResult(int UID, int OID)
    {
      UID = Convert.ToInt32(this.Request.Form[nameof (UID)].ToString());
      OID = Convert.ToInt32(this.Request.Form[nameof (OID)].ToString());
      int int32_1 = Convert.ToInt32(this.Request.Form["ACID"].ToString());
      int num1 = OID;
      int num2 = UID;
      string str1 = this.Request.Form["brf_id"].ToString();
      int int32_2 = Convert.ToInt32(this.Request.Form["qtn_count_" + str1].ToString());
      string input = this.Request.Form["brfcode"].ToString();
      int int32_3 = Convert.ToInt32(this.Request.Form["BTileId"].ToString());
      string str2 = "";
      List<string> stringList = new List<string>();
      int index1 = 0;
      string[] strArray = new string[int32_2];
      for (int index2 = 1; index2 <= int32_2; ++index2)
      {
        stringList.Add(this.Request.Form["qna_" + str1 + index2.ToString()].ToString());
        string str3 = this.Request.Form["qna_" + str1 + index2.ToString()].ToString();
        strArray[index1] = str3;
        ++index1;
      }
      for (int index3 = 1; index3 <= int32_2; ++index3)
        str2 = str2 + this.Request.Form["qna_" + str1 + index3.ToString()] + ";";
      string str4 = str2.TrimEnd(';').Trim();
      Convert.ToString((object) stringList);
      JsonConvert.SerializeObject((object) stringList).ToString();
      NameValueCollection nameValueCollection = new NameValueCollection()
      {
        {
          nameof (OID),
          Convert.ToString(num1)
        },
        {
          nameof (UID),
          Convert.ToString(num2)
        },
        {
          "BID",
          Convert.ToString(str1)
        },
        {
          "BRF",
          input
        },
        {
          "ASRQ",
          str4
        }
      };
      if (!new Regex("[^a-z0-9]").IsMatch(input))
        return input;
      JsonConvert.DeserializeObject<BriefReturnResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "EvaluateBriefAcademy?OID=" + num1.ToString() + "&UID=" + num2.ToString() + "&BID=" + str1 + "&BRF=" + input + "&ASRQ=" + str4 + "&AcademicTileId=" + int32_1.ToString() + "&brief_tile_id=" + int32_3.ToString()));
      return input;
    }

    public ActionResult MyDashboard(int UID, int OID)
    {
      FootballThemeLeaderBoardHeader leaderBoardHeader = new FootballThemeLeaderBoardHeader();
      LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
      List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
      List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
      UserScoreResponse userScoreResponse1 = new UserScoreResponse();
      List<tbl_user_badge_log> tblUserBadgeLogList = new List<tbl_user_badge_log>();
      tbl_profile tblProfile1 = new tbl_profile();
      int num1 = 0;
      int id_game = 0;
      UserScoreResponse userScoreResponse2;
      try
      {
        userScoreResponse2 = JsonConvert.DeserializeObject<UserScoreResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "getUserScore?UID=" + UID.ToString() + "&OID=" + OID.ToString()));
        leaderBoardResponse.userscore = userScoreResponse2.userscore;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tblBadgeMasterList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          tblLeaguesDataList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          leaderBoardHeader.currency = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_metric from tbl_university_special_point_grid where id_game={0}", (object) id_game).FirstOrDefault<int>();
          leaderBoardHeader.specialmetric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_special_metric_master where id_special_metric={0}", (object) num2).FirstOrDefault<string>();
          leaderBoardHeader.theme_metric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select metric_value from tbl_theme_metric where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardHeader.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          if (tblProfile2.social_dp_flag == 0)
            leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          else if (tblProfile2.PROFILE_IMAGE == "null")
            leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileDefaultBase"];
          else
            leaderBoardResponse.UserProfileImage = m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardResponse.UserName = m2ostnextserviceDbContext.Database.SqlQuery<string>(" select  concat (FIRSTNAME ,' ', LASTNAME) as Name from   tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardResponse.Badge = new UniversityScoringlogic().getBadgeList(UID, id_game);
        }
        int count = tblLeaguesDataList.Count;
        int num3 = 0;
        int num4 = 1;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0}", (object) UID).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          foreach (tbl_leagues_data tblLeaguesData in tblLeaguesDataList)
          {
            if (leaderBoardResponse.userscore > tblLeaguesData.minscore)
            {
              using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              {
                if (num4 < count)
                  leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) tblLeaguesDataList[num3 + 1].id_league).FirstOrDefault<string>();
              }
            }
            ++num3;
            ++num4;
          }
        }
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) tblLeaguesDataList[0].id_league).FirstOrDefault<string>();
        }
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          tblUserBadgeLogList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in tblUserBadgeLogList)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
              using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              {
                tblBadgeMaster.money_value = m2ostnextserviceDbContext.Database.SqlQuery<int>("select money_value from tbl_currency_data where id_badge={0} and id_game={1}", (object) tblBadgeMaster.id_badge, (object) id_game).FirstOrDefault<int>();
                num1 += tblBadgeMaster.money_value;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      int num5 = 0;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        num5 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(usedmoney),0) AS amnt FROM tbl_couponsredeemed where id_user={0} and id_game={1} and status={2}", (object) UID, (object) id_game, (object) "A").FirstOrDefault<int>();
      int num6 = num1 - num5;
      this.ViewData["Leader"] = (object) leaderBoardResponse;
      this.ViewData["Header"] = (object) leaderBoardHeader;
      this.ViewData[nameof (UID)] = (object) UID;
      this.ViewData[nameof (OID)] = (object) OID;
      this.ViewData["badgemaster"] = (object) tblBadgeMasterList;
      this.ViewData["ScoreRes"] = (object) userScoreResponse2;
      this.ViewData["totalcurrency"] = (object) num6;
      return (ActionResult) this.View();
    }

    public ActionResult RewardsPage(int UID, int OID)
    {
      FootballThemeLeaderBoardHeader leaderBoardHeader = new FootballThemeLeaderBoardHeader();
      LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
      List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
      List<tbl_leagues_data> tblLeaguesDataList = new List<tbl_leagues_data>();
      UserScoreResponse userScoreResponse1 = new UserScoreResponse();
      List<tbl_user_badge_log> tblUserBadgeLogList = new List<tbl_user_badge_log>();
      int num1 = 0;
      int id_game = 0;
      UserScoreResponse userScoreResponse2;
      try
      {
        userScoreResponse2 = JsonConvert.DeserializeObject<UserScoreResponse>(new UniversityScoringlogic().getApiResponseString(APIString.API + "getUserScore?UID=" + UID.ToString() + "&OID=" + OID.ToString()));
        leaderBoardResponse.userscore = userScoreResponse2.userscore;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tblBadgeMasterList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0} ORDER BY id_badge ASC", (object) 9).ToList<tbl_badge_master>();
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          tblLeaguesDataList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          leaderBoardHeader.currency = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_metric from tbl_university_special_point_grid where id_game={0}", (object) id_game).FirstOrDefault<int>();
          leaderBoardHeader.specialmetric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_special_metric_master where id_special_metric={0}", (object) num2).FirstOrDefault<string>();
          leaderBoardHeader.theme_metric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select metric_value from tbl_theme_metric where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardHeader.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardResponse.UserProfileImage = m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardResponse.UserName = m2ostnextserviceDbContext.Database.SqlQuery<string>(" select  FIRSTNAME from   tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardResponse.Badge = new UniversityScoringlogic().getBadgeList(UID, id_game);
          leaderBoardResponse.MailId = m2ostnextserviceDbContext.Database.SqlQuery<string>(" select  EMAIL from   tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardResponse.id_user = UID;
          tblUserBadgeLogList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
        }
        int count = tblLeaguesDataList.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (leaderBoardResponse.userleague == null)
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) tblLeaguesDataList[0].id_league).FirstOrDefault<string>();
        }
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in tblUserBadgeLogList)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
              using (new m2ostnextserviceDbContext())
                ;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      int num3 = 0;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tbl_user tblUser = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user where ID_USER={0}", (object) UID).FirstOrDefault<tbl_user>();
        num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(currency_value),0) AS amnt FROM tbl_user_currency_log where id_user={0} and status={1}", (object) UID, (object) "A").FirstOrDefault<int>();
        num1 += m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(referral_points),0) AS amnt FROM tbl_referral_code_user_mapping where referral_code={0} and status={1}", (object) tblUser.ref_id, (object) "A").FirstOrDefault<int>();
        num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(usedmoney),0) AS amnt FROM tbl_couponsredeemed where id_user={0} and id_game={1} and status={2}", (object) UID, (object) id_game, (object) "A").FirstOrDefault<int>();
      }
      if (leaderBoardResponse.UserProfileImage == "null" || leaderBoardResponse.UserProfileImage == "noframe.png")
        leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileDefaultBase"];
      int num4 = num1 - num3;
      string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(Convert.ToString(num4)));
      this.ViewData["Leader"] = (object) leaderBoardResponse;
      this.ViewData["Header"] = (object) leaderBoardHeader;
      this.ViewData[nameof (UID)] = (object) UID;
      this.ViewData[nameof (OID)] = (object) OID;
      this.ViewData["badgemaster"] = (object) tblBadgeMasterList;
      this.ViewData["ScoreRes"] = (object) userScoreResponse2;
      this.ViewData["TotalCurrency"] = (object) num4;
      this.ViewData["base64Currency"] = (object) base64String;
      this.ViewData["used_money"] = (object) num3;
      return (ActionResult) this.View();
    }

    public ActionResult RewardsRedirectPage(int UID)
    {
      List<CouponsRedeemed> couponsRedeemedList = new List<CouponsRedeemed>();
      try
      {
        Convert.ToInt32(ConfigurationManager.AppSettings["SOCIALORG"]);
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          couponsRedeemedList = m2ostnextserviceDbContext.Database.SqlQuery<CouponsRedeemed>("select * from tbl_couponsredeemed where id_user={0} and id_game={1} and status='A'", (object) UID, (object) num).ToList<CouponsRedeemed>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      this.ViewData["cpn"] = (object) couponsRedeemedList;
      return (ActionResult) this.View();
    }

    public ActionResult RewardsPost(string Data, string Hash)
    {
      try
      {
        Data = this.Server.UrlDecode(Data);
        Hash = this.Server.UrlDecode(Hash);
        RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(Data);
        rootObject.id_org = Convert.ToInt32(ConfigurationManager.AppSettings["SOCIALORG"]);
        rootObject.id_user = Convert.ToInt32(rootObject.AccountID);
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          rootObject.id_game = num;
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_rewards_redeem_master (AccountID,EmailAddress,PartnerCode,ProviderCode,RedeemType,TotalPoints,TransactionID,UserName,id_user,id_org,id_game) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10})", (object) rootObject.AccountID, (object) rootObject.EmailAddress, (object) rootObject.PartnerCode, (object) rootObject.ProviderCode, (object) rootObject.RedeemType, (object) rootObject.TotalPoints, (object) rootObject.TransactionID, (object) rootObject.UserName, (object) rootObject.id_user, (object) rootObject.id_org, (object) rootObject.id_game);
          foreach (CouponsRedeemed couponsRedeemed in rootObject.MiscellaneousData1.CouponsRedeemed)
          {
            if (!(couponsRedeemed.CouponID == "931") && !(couponsRedeemed.CouponID == "1016") && !(couponsRedeemed.CouponID == "1019"))
            {
              couponsRedeemed.usedmoney = Convert.ToInt32(couponsRedeemed.PointsUsed);
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_couponsredeemed (CouponID,WebsiteName,CouponCode,CouponDescription,Link,PointsUsed,Image,ExpiryDate,id_game,id_user,id_org,usedmoney) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", (object) couponsRedeemed.CouponID, (object) couponsRedeemed.WebsiteName, (object) couponsRedeemed.CouponCode, (object) couponsRedeemed.CouponDescription, (object) couponsRedeemed.Link, (object) couponsRedeemed.PointsUsed, (object) couponsRedeemed.Image, (object) couponsRedeemed.ExpiryDate, (object) num, (object) rootObject.id_user, (object) rootObject.id_org, (object) couponsRedeemed.usedmoney);
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult FormSub() => (ActionResult) this.View();

    public ActionResult Loginaction()
    {
      try
      {
        if (new RoadMapLogic().LoginValidate(this.Request.Form["uid"], this.Request.Form["pswd"]) == "SUCCESS")
          return (ActionResult) this.RedirectToAction("Index");
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.RedirectToAction("FormSub");
    }

    public ActionResult FootballLeaderBoardForCollege(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        string str1 = "";
        int id_league = 0;
        tbl_profile tblProfile1 = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        List<tbl_badge_master> tblBadgeMasterList1 = new List<tbl_badge_master>();
        List<tbl_user_badge_log> tblUserBadgeLogList1 = new List<tbl_user_badge_log>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          str1 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_game_master where id_game={0}", (object) id_game).FirstOrDefault<string>();
          tblProfile1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          tblBadgeMasterList1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
          tblUserBadgeLogList1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
          id_league = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<int>();
        }
        List<tbl_leagues_data> list1 = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME;
        leaderBoardResponse.Badge = new UniversityScoringlogic().getBadgeList(UID, id_game);
        leaderBoardResponse.UserList = new UniversityScoringlogic().getUserListLeaderBoard(id_game, OID, id_league);
        leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderByDescending<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList1 = new LeaderBoardUserList();
        LeaderBoardUserList leaderBoardUserList2 = leaderBoardResponse.UserList.Where<LeaderBoardUserList>((Func<LeaderBoardUserList, bool>) (t => t.id_user == UID)).FirstOrDefault<LeaderBoardUserList>();
        leaderBoardResponse.userscore = leaderBoardUserList2.metric_score;
        int count = list1.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 1;
        int num2 = 1;
        foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) user.id_user).FirstOrDefault<tbl_profile>();
            if (num1 <= Convert.ToInt32(ConfigurationManager.AppSettings["UserListLimit"]))
            {
              if (tblProfile2 != null)
              {
                user.UserProfileImage = !(tblProfile2.PROFILE_IMAGE == "null") ? ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile2.PROFILE_IMAGE : ConfigurationManager.AppSettings["ProfileDefaultBase"];
                user.Username = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME;
                user.city = tblProfile2.CITY;
                List<tbl_badge_master> tblBadgeMasterList2 = new List<tbl_badge_master>();
                List<tbl_badge_master> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
                user.userbadge = new List<tbl_badge_master>();
                user.Badge = new UniversityScoringlogic().getBadgeList(user.id_user, id_game);
                foreach (tbl_badge_master tblBadgeMaster in list2)
                {
                  tblBadgeMaster.WonFlag = 0;
                  foreach (UserBadge userBadge in user.Badge)
                  {
                    if (userBadge.id_badge == tblBadgeMaster.id_badge)
                    {
                      tblBadgeMaster.WonFlag = 1;
                      tblBadgeMaster.eligiblescore = Convert.ToInt32(user.metric_score / (double) userBadge.eligible_score);
                    }
                  }
                  user.userbadge.Add(tblBadgeMaster);
                }
                user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) user.id_user, (object) id_game).FirstOrDefault<string>();
                if (user.userleague == null)
                  user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
                if (leaderBoardResponse.userleague == user.userleague)
                {
                  if (tblProfile1.COLLEGE == tblProfile2.COLLEGE)
                  {
                    if (tblProfile1.COLLEGE != null)
                    {
                      if (tblProfile1.COLLEGE != "")
                      {
                        user.Rank = num2;
                        leaderBoardUserListList.Add(user);
                        ++num2;
                        ++num1;
                      }
                    }
                  }
                }
              }
            }
          }
        }
        leaderBoardResponse.UserList = leaderBoardUserListList;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
          string str2 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
          {
            List<tbl_user_badge_log> tblUserBadgeLogList2 = new List<tbl_user_badge_log>();
            List<tbl_user_badge_log> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) user.id_user, (object) id_game).ToList<tbl_user_badge_log>();
            int num4 = 0;
            foreach (tbl_badge_master tblBadgeMaster in user.userbadge)
            {
              foreach (tbl_user_badge_log tblUserBadgeLog in list3)
              {
                if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
                {
                  tblBadgeMaster.currency_value = m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_currency={1}", (object) tblBadgeMaster.id_badge, (object) num3).FirstOrDefault<int>();
                  tblBadgeMaster.currency_name = str2;
                  num4 += tblBadgeMaster.currency_value;
                }
              }
            }
            user.currencyvalue = num4;
          }
        }
        FootballThemeLeaderBoardHeader leaderBoardHeader = new FootballThemeLeaderBoardHeader();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardHeader.currency = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          int num5 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_metric from tbl_university_special_point_grid where id_game={0}", (object) id_game).FirstOrDefault<int>();
          leaderBoardHeader.specialmetric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_special_metric_master where id_special_metric={0}", (object) num5).FirstOrDefault<string>();
          leaderBoardHeader.theme_metric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select metric_value from tbl_theme_metric where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardHeader.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          if (leaderBoardResponse.userleague == null)
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
        }
        if (leaderBoardResponse.UserProfileImage == "null")
          leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileDefaultBase"];
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList1)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in tblUserBadgeLogList1)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
            }
          }
        }
        this.ViewData["Leader"] = (object) leaderBoardResponse;
        this.ViewData["Header"] = (object) leaderBoardHeader;
        this.ViewData[nameof (UID)] = (object) UID;
        this.ViewData[nameof (OID)] = (object) OID;
        this.ViewData["badgemaster"] = (object) tblBadgeMasterList1;
        this.ViewData["gamename"] = (object) str1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult FootballLeaderBoardForCountry(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        string str1 = "";
        int id_league = 0;
        tbl_profile tblProfile1 = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        List<tbl_badge_master> tblBadgeMasterList1 = new List<tbl_badge_master>();
        List<tbl_user_badge_log> tblUserBadgeLogList1 = new List<tbl_user_badge_log>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          str1 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_game_master where id_game={0}", (object) id_game).FirstOrDefault<string>();
          tblProfile1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          tblBadgeMasterList1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
          tblUserBadgeLogList1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) UID, (object) id_game).ToList<tbl_user_badge_log>();
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
          id_league = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<int>();
        }
        List<tbl_leagues_data> list1 = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME;
        leaderBoardResponse.Badge = new UniversityScoringlogic().getBadgeList(UID, id_game);
        leaderBoardResponse.UserList = new UniversityScoringlogic().getUserListLeaderBoard(id_game, OID, id_league);
        leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderByDescending<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList1 = new LeaderBoardUserList();
        LeaderBoardUserList leaderBoardUserList2 = leaderBoardResponse.UserList.Where<LeaderBoardUserList>((Func<LeaderBoardUserList, bool>) (t => t.id_user == UID)).FirstOrDefault<LeaderBoardUserList>();
        leaderBoardResponse.userscore = leaderBoardUserList2.metric_score;
        int count = list1.Count;
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 1;
        int num2 = 1;
        foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) user.id_user).FirstOrDefault<tbl_profile>();
            if (num1 <= Convert.ToInt32(ConfigurationManager.AppSettings["UserListLimit"]))
            {
              if (tblProfile2 != null)
              {
                user.UserProfileImage = !(tblProfile2.PROFILE_IMAGE == "null") ? ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile2.PROFILE_IMAGE : ConfigurationManager.AppSettings["ProfileDefaultBase"];
                user.Username = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME;
                user.city = tblProfile2.CITY;
                List<tbl_badge_master> tblBadgeMasterList2 = new List<tbl_badge_master>();
                List<tbl_badge_master> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
                user.userbadge = new List<tbl_badge_master>();
                user.Badge = new UniversityScoringlogic().getBadgeList(user.id_user, id_game);
                foreach (tbl_badge_master tblBadgeMaster in list2)
                {
                  tblBadgeMaster.WonFlag = 0;
                  foreach (UserBadge userBadge in user.Badge)
                  {
                    if (userBadge.id_badge == tblBadgeMaster.id_badge)
                    {
                      tblBadgeMaster.WonFlag = 1;
                      tblBadgeMaster.eligiblescore = Convert.ToInt32(user.metric_score / (double) userBadge.eligible_score);
                    }
                  }
                  user.userbadge.Add(tblBadgeMaster);
                }
                user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) user.id_user, (object) id_game).FirstOrDefault<string>();
                if (user.userleague == null)
                  user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
                if (leaderBoardResponse.userleague == user.userleague)
                {
                  if (tblProfile1.COUNTRY == tblProfile2.COUNTRY)
                  {
                    user.Rank = num2;
                    leaderBoardUserListList.Add(user);
                    ++num2;
                    ++num1;
                  }
                }
              }
            }
          }
        }
        leaderBoardResponse.UserList = leaderBoardUserListList;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          int num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
          string str2 = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
          {
            List<tbl_user_badge_log> tblUserBadgeLogList2 = new List<tbl_user_badge_log>();
            List<tbl_user_badge_log> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) user.id_user, (object) id_game).ToList<tbl_user_badge_log>();
            int num4 = 0;
            foreach (tbl_badge_master tblBadgeMaster in user.userbadge)
            {
              foreach (tbl_user_badge_log tblUserBadgeLog in list3)
              {
                if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
                {
                  tblBadgeMaster.currency_value = m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_currency={1}", (object) tblBadgeMaster.id_badge, (object) num3).FirstOrDefault<int>();
                  tblBadgeMaster.currency_name = str2;
                  num4 += tblBadgeMaster.currency_value;
                }
              }
            }
            user.currencyvalue = num4;
          }
        }
        FootballThemeLeaderBoardHeader leaderBoardHeader = new FootballThemeLeaderBoardHeader();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          leaderBoardHeader.currency = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          int num5 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_metric from tbl_university_special_point_grid where id_game={0}", (object) id_game).FirstOrDefault<int>();
          leaderBoardHeader.specialmetric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select name from tbl_special_metric_master where id_special_metric={0}", (object) num5).FirstOrDefault<string>();
          leaderBoardHeader.theme_metric = m2ostnextserviceDbContext.Database.SqlQuery<string>("select metric_value from tbl_theme_metric where id_theme={0}", (object) 9).FirstOrDefault<string>();
          leaderBoardHeader.currency_image = ConfigurationManager.AppSettings["CurrencyImageBase"] + m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_logo from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
          if (leaderBoardResponse.userleague == null)
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list1[0].id_league).FirstOrDefault<string>();
        }
        if (leaderBoardResponse.UserProfileImage == "null")
          leaderBoardResponse.UserProfileImage = ConfigurationManager.AppSettings["ProfileDefaultBase"];
        foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList1)
        {
          tblBadgeMaster.WonFlag = 0;
          foreach (tbl_user_badge_log tblUserBadgeLog in tblUserBadgeLogList1)
          {
            if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
            {
              tblBadgeMaster.WonFlag = 1;
              ++tblBadgeMaster.eligiblescore;
            }
          }
        }
        this.ViewData["Leader"] = (object) leaderBoardResponse;
        this.ViewData["Header"] = (object) leaderBoardHeader;
        this.ViewData[nameof (UID)] = (object) UID;
        this.ViewData[nameof (OID)] = (object) OID;
        this.ViewData["badgemaster"] = (object) tblBadgeMasterList1;
        this.ViewData["gamename"] = (object) str1;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult APIREstrictionPage() => (ActionResult) this.View();

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length).Select<string, char>((Func<string, char>) (s => s[DashboardWebViewController.random.Next(s.Length)])).ToArray<char>());

    public ActionResult FootballLeaderboardPartialView(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        int id_league = 0;
        tbl_profile tblProfile1 = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          tblProfile1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          id_league = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<int>();
        }
        List<tbl_leagues_data> list = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME;
        leaderBoardResponse.UserList = new UniversityScoringlogic().getUserListLeaderBoard(id_game, OID, id_league);
        leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderByDescending<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList1 = new LeaderBoardUserList();
        LeaderBoardUserList leaderBoardUserList2 = leaderBoardResponse.UserList.Where<LeaderBoardUserList>((Func<LeaderBoardUserList, bool>) (t => t.id_user == UID)).FirstOrDefault<LeaderBoardUserList>();
        leaderBoardResponse.userscore = leaderBoardUserList2.metric_score;
        int count = list.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 1;
        int num2 = 1;
        foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) user.id_user).FirstOrDefault<tbl_profile>();
            if (num1 <= Convert.ToInt32(ConfigurationManager.AppSettings["UserListLimit"]))
            {
              if (tblProfile2 != null)
              {
                user.UserProfileImage = tblProfile2.social_dp_flag != 0 ? (!(tblProfile2.PROFILE_IMAGE == "null") ? tblProfile2.PROFILE_IMAGE : ConfigurationManager.AppSettings["ProfileDefaultBase"]) : ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile2.PROFILE_IMAGE;
                user.Username = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME;
                user.city = tblProfile2.CITY;
                List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
                user.userbadge = new List<tbl_badge_master>();
                user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) user.id_user, (object) id_game).FirstOrDefault<string>();
                if (user.userleague == null)
                  user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
                if (user.id_user == 3640)
                  ;
                if (leaderBoardResponse.userleague == user.userleague)
                {
                  user.Rank = num2;
                  leaderBoardUserListList.Add(user);
                  user.currencyvalue = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(currency_value),0) from tbl_user_currency_log where id_user={0}", (object) user.id_user).FirstOrDefault<int>();
                  string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select ref_id from tbl_user where ID_USER={0}", (object) user.id_user).FirstOrDefault<string>();
                  user.currencyvalue += m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(referral_points),0) from tbl_referral_code_user_mapping where referral_code={0}", (object) str).FirstOrDefault<int>();
                  ++num2;
                  ++num1;
                }
              }
            }
          }
        }
        leaderBoardResponse.UserList = leaderBoardUserListList;
        this.ViewData["Leader"] = (object) leaderBoardResponse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult FootBallLeaderBoardCollegePartialView(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        int id_league = 0;
        tbl_profile tblProfile1 = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          tblProfile1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          id_league = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<int>();
        }
        List<tbl_leagues_data> list = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME;
        leaderBoardResponse.UserList = new UniversityScoringlogic().getUserListLeaderBoard(id_game, OID, id_league);
        leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderByDescending<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList = new LeaderBoardUserList();
        leaderBoardResponse.userscore = leaderBoardUserList.metric_score;
        int count = list.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 1;
        int num2 = 1;
        foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) user.id_user).FirstOrDefault<tbl_profile>();
            if (num1 <= Convert.ToInt32(ConfigurationManager.AppSettings["UserListLimit"]))
            {
              if (tblProfile2 != null)
              {
                user.UserProfileImage = !(tblProfile2.PROFILE_IMAGE == "null") ? ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile2.PROFILE_IMAGE : ConfigurationManager.AppSettings["ProfileDefaultBase"];
                user.Username = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME;
                user.city = tblProfile2.CITY;
                List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
                user.userbadge = new List<tbl_badge_master>();
                user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) user.id_user, (object) id_game).FirstOrDefault<string>();
                if (user.userleague == null)
                  user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
                if (leaderBoardResponse.userleague == user.userleague)
                {
                  if (tblProfile1.COLLEGE == tblProfile2.COLLEGE)
                  {
                    if (tblProfile1.COLLEGE != null)
                    {
                      user.Rank = num2;
                      leaderBoardUserListList.Add(user);
                      user.currencyvalue = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(currency_value),0) from tbl_user_currency_log where id_user={0}", (object) user.id_user).FirstOrDefault<int>();
                      string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select ref_id from tbl_user where ID_USER={0}", (object) user.id_user).FirstOrDefault<string>();
                      user.currencyvalue += m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(referral_points),0) from tbl_referral_code_user_mapping where referral_code={0}", (object) str).FirstOrDefault<int>();
                      ++num2;
                      ++num1;
                    }
                  }
                }
              }
            }
          }
        }
        leaderBoardResponse.UserList = leaderBoardUserListList;
        this.ViewData["Leader"] = (object) leaderBoardResponse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public ActionResult FootBallLeaderBoardCountryPartialView(int UID, int OID)
    {
      try
      {
        LeaderBoardResponse leaderBoardResponse = new LeaderBoardResponse();
        int id_game = 0;
        int id_league = 0;
        tbl_profile tblProfile1 = new tbl_profile();
        List<tbl_leagues_data> source = new List<tbl_leagues_data>();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
          tblProfile1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          source = m2ostnextserviceDbContext.Database.SqlQuery<tbl_leagues_data>("select * from tbl_leagues_data where id_game={0}", (object) id_game).ToList<tbl_leagues_data>();
          id_league = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<int>();
        }
        List<tbl_leagues_data> list = source.OrderBy<tbl_leagues_data, double>((Func<tbl_leagues_data, double>) (o => o.minscore)).ToList<tbl_leagues_data>();
        leaderBoardResponse.id_game = id_game;
        leaderBoardResponse.id_user = UID;
        leaderBoardResponse.UserName = tblProfile1.FIRSTNAME + " " + tblProfile1.LASTNAME;
        leaderBoardResponse.UserList = new UniversityScoringlogic().getUserListLeaderBoard(id_game, OID, id_league);
        leaderBoardResponse.UserList = leaderBoardResponse.UserList.OrderByDescending<LeaderBoardUserList, double>((Func<LeaderBoardUserList, double>) (o => o.metric_score)).ToList<LeaderBoardUserList>();
        List<LeaderBoardUserList> leaderBoardUserListList = new List<LeaderBoardUserList>();
        LeaderBoardUserList leaderBoardUserList = new LeaderBoardUserList();
        leaderBoardResponse.userscore = leaderBoardUserList.metric_score;
        int count = list.Count;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) UID, (object) id_game).FirstOrDefault<string>();
        if (leaderBoardResponse.userleague == null)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            leaderBoardResponse.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
        }
        int num1 = 1;
        int num2 = 1;
        foreach (LeaderBoardUserList user in leaderBoardResponse.UserList)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) user.id_user).FirstOrDefault<tbl_profile>();
            if (num1 <= Convert.ToInt32(ConfigurationManager.AppSettings["UserListLimit"]))
            {
              if (tblProfile2 != null)
              {
                user.UserProfileImage = !(tblProfile2.PROFILE_IMAGE == "null") ? ConfigurationManager.AppSettings["ProfileImageBase"] + tblProfile2.PROFILE_IMAGE : ConfigurationManager.AppSettings["ProfileDefaultBase"];
                user.Username = tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME;
                user.city = tblProfile2.CITY;
                List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
                user.userbadge = new List<tbl_badge_master>();
                user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league from tbl_user_league_log where id_user={0} and id_game={1}", (object) user.id_user, (object) id_game).FirstOrDefault<string>();
                if (user.userleague == null)
                  user.userleague = m2ostnextserviceDbContext.Database.SqlQuery<string>("select league_name from tbl_theme_leagues where id_league={0}", (object) list[0].id_league).FirstOrDefault<string>();
                if (leaderBoardResponse.userleague == user.userleague)
                {
                  if (tblProfile1.COUNTRY == tblProfile2.COUNTRY)
                  {
                    user.Rank = num2;
                    leaderBoardUserListList.Add(user);
                    user.currencyvalue = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(currency_value),0) from tbl_user_currency_log where id_user={0}", (object) user.id_user).FirstOrDefault<int>();
                    string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select ref_id from tbl_user where ID_USER={0}", (object) user.id_user).FirstOrDefault<string>();
                    user.currencyvalue += m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(referral_points),0) from tbl_referral_code_user_mapping where referral_code={0}", (object) str).FirstOrDefault<int>();
                    ++num2;
                    ++num1;
                  }
                }
              }
            }
          }
        }
        leaderBoardResponse.UserList = leaderBoardUserListList;
        this.ViewData["Leader"] = (object) leaderBoardResponse;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return (ActionResult) this.View();
    }

    public void currency_value_generator()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_currency from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<int>();
        string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select currency_value from tbl_currency_points where id_theme={0}", (object) 9).FirstOrDefault<string>();
        int id_game = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_game from tbl_game_master where id_theme={0} and status={1}", (object) 9, (object) "A").FirstOrDefault<int>();
        List<tbl_badge_master> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_theme={0}", (object) 9).ToList<tbl_badge_master>();
        string sql = "SELECT a.id_user, (SELECT CASE WHEN (SUM(score) > 0) THEN SUM(score) ELSE 0 END FROM tbl_user_game_score_log WHERE a.id_user = id_user AND id_game = " + id_game.ToString() + ") metric_score, (SELECT CASE WHEN (SUM(score) > 0) THEN SUM(score) ELSE 0 END FROM tbl_user_game_special_metric_score_log WHERE a.id_user = id_user AND id_game =  " + id_game.ToString() + ") special_metric_score FROM tbl_user a WHERE a.ID_ORGANIZATION =  " + 130.ToString() + " AND a.status = 'A' and  a.id_user!=2503 ";
        foreach (LeaderBoardUserList leaderBoardUserList in m2ostnextserviceDbContext.Database.SqlQuery<LeaderBoardUserList>(sql).ToList<LeaderBoardUserList>())
        {
          List<tbl_user_badge_log> tblUserBadgeLogList = new List<tbl_user_badge_log>();
          List<tbl_user_badge_log> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_badge_log>("SELECT * FROM tbl_user_badge_log where id_user={0} and id_game={1} ", (object) leaderBoardUserList.id_user, (object) id_game).ToList<tbl_user_badge_log>();
          List<tbl_badge_master> tblBadgeMasterList = new List<tbl_badge_master>();
          List<UserBadge> badgeList = new UniversityScoringlogic().getBadgeList(leaderBoardUserList.id_user, id_game);
          foreach (tbl_badge_master tblBadgeMaster in list1)
          {
            tblBadgeMaster.WonFlag = 0;
            foreach (UserBadge userBadge in badgeList)
            {
              if (userBadge.id_badge == tblBadgeMaster.id_badge)
              {
                tblBadgeMaster.WonFlag = 1;
                tblBadgeMaster.eligiblescore = Convert.ToInt32(leaderBoardUserList.metric_score / (double) userBadge.eligible_score);
              }
            }
            tblBadgeMasterList.Add(tblBadgeMaster);
          }
          int num2 = 0;
          foreach (tbl_badge_master tblBadgeMaster in tblBadgeMasterList)
          {
            foreach (tbl_user_badge_log tblUserBadgeLog in list2)
            {
              if (tblUserBadgeLog.id_badge == tblBadgeMaster.id_badge)
              {
                tblBadgeMaster.currency_value = m2ostnextserviceDbContext.Database.SqlQuery<int>("select currency_value from tbl_currency_data where id_badge={0} and id_currency={1}", (object) tblBadgeMaster.id_badge, (object) num1).FirstOrDefault<int>();
                tblBadgeMaster.currency_name = str;
                num2 += tblBadgeMaster.currency_value;
              }
            }
          }
          int num3 = num2;
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_currency_log (id_user,id_game,currency_value,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) leaderBoardUserList.id_user, (object) id_game, (object) num3, (object) "A", (object) DateTime.Now);
        }
      }
    }

    public void activity_log()
    {
    }

    public void UnitAverageScoreReport(int id_org_game, string UserFunction, int OID, int UID)
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
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source.Add(unitRankLog);
            }
            List<UnitRankLog> list2 = source.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.AverageScore)).ToList<UnitRankLog>();
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
              unitRankLog.id_org_game = id_org_game;
              unitRankLog.Unit = orgGameUnitMaster.unit;
              unitRankLog.IdUnit = orgGameUnitMaster.id_org_game_unit;
              source.Add(unitRankLog);
            }
            List<UnitRankLog> list4 = source.OrderByDescending<UnitRankLog, double>((Func<UnitRankLog, double>) (x => x.AverageScore)).ToList<UnitRankLog>();
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
    }

    public void earliestFinishReport()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<UnitCompletionReport> completionReportList = new List<UnitCompletionReport>();
        List<tbl_org_game_unit_master> orgGameUnitMasterList = new List<tbl_org_game_unit_master>();
        foreach (tbl_org_game_unit_master orgGameUnitMaster in m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where status='A' and id_org=132 and unit_type={0}", (object) 1).ToList<tbl_org_game_unit_master>())
        {
          List<tbl_org_game_user_log> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("SELECT * FROM tbl_org_game_user_log AS a INNER JOIN tbl_user AS b ON a.id_user = b.ID_USER INNER JOIN tbl_profile AS c ON b.id_user = c.ID_USER WHERE b.STATUS = 'A' AND c.id_org_game_unit = {0} AND a.id_level = 5 AND a.is_completed = 1 AND b.ID_ORGANIZATION = 132", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_org_game_user_log>();
          List<tbl_user> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user as a inner join tbl_profile as b on a.ID_USER=b.ID_USER where a.STATUS='A' and b.id_org_game_unit={0}", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_user>();
          if (list1.Count == list2.Count)
            completionReportList.Add(new UnitCompletionReport()
            {
              UnitName = orgGameUnitMaster.unit,
              LastCompletedDate = list1.Select<tbl_org_game_user_log, DateTime>((Func<tbl_org_game_user_log, DateTime>) (x => x.updated_date_time)).Max<DateTime>()
            });
        }
      }
    }

    public void AverageTimeTakenunit()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<UnitCompletionAverageReport> completionAverageReportList = new List<UnitCompletionAverageReport>();
        List<tbl_org_game_unit_master> orgGameUnitMasterList = new List<tbl_org_game_unit_master>();
        foreach (tbl_org_game_unit_master orgGameUnitMaster in m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where status='A' and id_org=132 and unit_type={0}", (object) 2).ToList<tbl_org_game_unit_master>())
        {
          List<tbl_org_game_user_log> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("SELECT * FROM tbl_org_game_user_log AS a INNER JOIN tbl_user AS b ON a.id_user = b.ID_USER INNER JOIN tbl_profile AS c ON b.id_user = c.ID_USER WHERE b.STATUS = 'A' AND c.id_org_game_unit = {0} AND a.id_level = 5 AND a.is_completed = 1 AND b.ID_ORGANIZATION = 132", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_org_game_user_log>();
          List<tbl_user> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user as a inner join tbl_profile as b on a.ID_USER=b.ID_USER where a.STATUS='A' and b.id_org_game_unit={0}", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_user>();
          if (list1.Count == list2.Count)
          {
            UnitCompletionAverageReport completionAverageReport = new UnitCompletionAverageReport();
            completionAverageReport.TotalTime = TimeSpan.Parse("00:00");
            foreach (tbl_org_game_user_log tblOrgGameUserLog in list1)
            {
              TimeSpan timeSpan = TimeSpan.Parse("00:" + tblOrgGameUserLog.timetaken_to_complete);
              completionAverageReport.TotalTime = timeSpan + completionAverageReport.TotalTime;
            }
            completionAverageReport.averageSecs = Convert.ToInt32(completionAverageReport.TotalTime.TotalSeconds) / list2.Count;
            completionAverageReport.UnitName = orgGameUnitMaster.unit;
            completionAverageReportList.Add(completionAverageReport);
          }
        }
      }
    }

    public void ScoreWiseUserRank()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<UnituserScoreWiseData> unituserScoreWiseDataList = new List<UnituserScoreWiseData>();
        List<tbl_org_game_unit_master> orgGameUnitMasterList = new List<tbl_org_game_unit_master>();
        foreach (tbl_org_game_unit_master orgGameUnitMaster in m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where status='A' and id_org=132 and unit_type={0}", (object) 2).ToList<tbl_org_game_unit_master>())
        {
          List<tbl_org_game_user_log> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("SELECT * FROM tbl_org_game_user_log AS a INNER JOIN tbl_user AS b ON a.id_user = b.ID_USER INNER JOIN tbl_profile AS c ON b.id_user = c.ID_USER WHERE b.STATUS = 'A' AND c.id_org_game_unit = {0} AND a.id_level = 5 AND a.attempt_no = 1 AND b.ID_ORGANIZATION = 132", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_org_game_user_log>();
          m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user as a inner join tbl_profile as b on a.ID_USER=b.ID_USER where a.STATUS='A' and b.id_org_game_unit={0}", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_user>();
          UnituserScoreWiseData unituserScoreWiseData = new UnituserScoreWiseData();
          unituserScoreWiseData.UnitName = orgGameUnitMaster.unit;
          List<OrguserScoreData> orguserScoreDataList = new List<OrguserScoreData>();
          foreach (tbl_org_game_user_log tblOrgGameUserLog in list)
          {
            OrguserScoreData orguserScoreData = new OrguserScoreData();
            orguserScoreData.TimeTakenToComplete = TimeSpan.Parse("00:" + tblOrgGameUserLog.timetaken_to_complete);
            orguserScoreData.Name = m2ostnextserviceDbContext.Database.SqlQuery<string>("select concat(a.FIRSTNAME,' ',a.LASTNAME) as name from tbl_profile as a where a.ID_USER={0}", (object) tblOrgGameUserLog.id_user).FirstOrDefault<string>();
            int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(id_log) from tbl_org_game_user_assessment_log as a where a.id_user={0} and a.is_correct=1 and a.attempt_no=1", (object) tblOrgGameUserLog.id_user).FirstOrDefault<int>();
            int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(id_log) from tbl_org_game_user_assessment_log as a where a.id_user={0} and a.is_correct=0 and a.attempt_no=1", (object) tblOrgGameUserLog.id_user).FirstOrDefault<int>();
            int num3 = num1 + num2;
            orguserScoreData.AssessmnetScore = Convert.ToDouble(Convert.ToDouble(num1) / Convert.ToDouble(num3) * 100.0);
            orguserScoreDataList.Add(orguserScoreData);
          }
          unituserScoreWiseData.UserList = orguserScoreDataList;
          unituserScoreWiseDataList.Add(unituserScoreWiseData);
        }
      }
    }

    public void TimeWiseUserRank()
    {
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<UnituserScoreWiseData> unituserScoreWiseDataList = new List<UnituserScoreWiseData>();
        List<tbl_org_game_unit_master> orgGameUnitMasterList = new List<tbl_org_game_unit_master>();
        foreach (tbl_org_game_unit_master orgGameUnitMaster in m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_unit_master>("select * from tbl_org_game_unit_master where status='A' and id_org=132 and unit_type={0}", (object) 2).ToList<tbl_org_game_unit_master>())
        {
          List<tbl_org_game_user_log> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("SELECT * FROM tbl_org_game_user_log AS a INNER JOIN tbl_user AS b ON a.id_user = b.ID_USER INNER JOIN tbl_profile AS c ON b.id_user = c.ID_USER WHERE b.STATUS = 'A' AND c.id_org_game_unit = {0} AND a.id_level = 5 AND a.is_completed = 1 AND b.ID_ORGANIZATION = 132", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_org_game_user_log>();
          m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user as a inner join tbl_profile as b on a.ID_USER=b.ID_USER where a.STATUS='A' and b.id_org_game_unit={0}", (object) orgGameUnitMaster.id_org_game_unit).ToList<tbl_user>();
          UnituserScoreWiseData unituserScoreWiseData = new UnituserScoreWiseData();
          unituserScoreWiseData.UnitName = orgGameUnitMaster.unit;
          List<OrguserScoreData> orguserScoreDataList = new List<OrguserScoreData>();
          foreach (tbl_org_game_user_log tblOrgGameUserLog in list)
          {
            OrguserScoreData orguserScoreData = new OrguserScoreData();
            orguserScoreData.TimeTakenToComplete = TimeSpan.Parse("00:" + tblOrgGameUserLog.timetaken_to_complete);
            orguserScoreData.Name = m2ostnextserviceDbContext.Database.SqlQuery<string>("select concat(a.FIRSTNAME,' ',a.LASTNAME) as name from tbl_profile as a where a.ID_USER={0}", (object) tblOrgGameUserLog.id_user).FirstOrDefault<string>();
            int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(id_log) from tbl_org_game_user_assessment_log as a where a.id_user={0} and a.is_correct=1 and a.attempt_no=1", (object) tblOrgGameUserLog.id_user).FirstOrDefault<int>();
            int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(id_log) from tbl_org_game_user_assessment_log as a where a.id_user={0} and a.is_correct=0 and a.attempt_no=1", (object) tblOrgGameUserLog.id_user).FirstOrDefault<int>();
            int num3 = num1 + num2;
            orguserScoreData.AssessmnetScore = Convert.ToDouble(Convert.ToDouble(num1) / Convert.ToDouble(num3) * 100.0);
            orguserScoreDataList.Add(orguserScoreData);
          }
          unituserScoreWiseData.UserList = orguserScoreDataList;
          unituserScoreWiseDataList.Add(unituserScoreWiseData);
        }
      }
    }
  }
}
