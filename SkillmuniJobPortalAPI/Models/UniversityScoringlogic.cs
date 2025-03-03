// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.UniversityScoringlogic
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace m2ostnextservice.Models
{
  public class UniversityScoringlogic
  {
    public List<AssessmentScoreResponse> ScoreCal(
      int id_academy,
      int id_brief,
      int id_user,
      int right_count)
    {
      List<AssessmentScoreResponse> assessmentScoreResponseList = new List<AssessmentScoreResponse>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_game_academic_mapping map = m2ostnextserviceDbContext.Database.SqlQuery<tbl_game_academic_mapping>("Select * from tbl_game_academic_mapping where id_academic_tile={0} and status={1}", (object) id_academy, (object) "A").FirstOrDefault<tbl_game_academic_mapping>();
          if (m2ostnextserviceDbContext.Database.SqlQuery<string>("Select assignment_flag from tbl_game_master where id_game={0} ", (object) map.id_game).FirstOrDefault<string>() == "A")
            assessmentScoreResponseList = this.scoringlogic(map, id_brief, right_count);
          else if (m2ostnextserviceDbContext.Database.SqlQuery<int>("Select id_user from tbl_user_game_assignment where id_game={0} and id_user={1}", (object) map.id_game, (object) id_user).FirstOrDefault<int>() > 0)
            assessmentScoreResponseList = this.scoringlogic(map, id_brief, right_count);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return assessmentScoreResponseList;
    }

    public List<AssessmentScoreResponse> ScoreSplCal(
      int id_academy,
      int id_brief,
      int id_user,
      int right_count)
    {
      List<AssessmentScoreResponse> assessmentScoreResponseList = new List<AssessmentScoreResponse>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_game_academic_mapping map = m2ostnextserviceDbContext.Database.SqlQuery<tbl_game_academic_mapping>("Select * from tbl_game_academic_mapping where id_academic_tile={0} and status={1}", (object) id_academy, (object) "A").FirstOrDefault<tbl_game_academic_mapping>();
          if (m2ostnextserviceDbContext.Database.SqlQuery<string>("Select assignment_flag from tbl_game_master where id_game={0} ", (object) map.id_game).FirstOrDefault<string>() == "A")
            assessmentScoreResponseList = this.SPLscoringlogic(map, id_brief, right_count);
          else if (m2ostnextserviceDbContext.Database.SqlQuery<int>("Select id_user from tbl_user_game_assignment where id_game={0} and id_user={1}", (object) map.id_game, (object) id_user).FirstOrDefault<int>() > 0)
            assessmentScoreResponseList = this.SPLscoringlogic(map, id_brief, right_count);
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return assessmentScoreResponseList;
    }

    public List<AssessmentScoreResponse> scoringlogic(
      tbl_game_academic_mapping map,
      int id_brief,
      int rightcount)
    {
      List<AssessmentScoreResponse> assessmentScoreResponseList = new List<AssessmentScoreResponse>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) map.id_game
          };
          foreach (int num1 in database.SqlQuery<int>("SELECT distinct id_metric FROM tbl_university_kpi_grid where id_game={0}", objArray).ToList<int>())
          {
            AssessmentScoreResponse assessmentScoreResponse = new AssessmentScoreResponse();
            assessmentScoreResponse.id_brief = id_brief;
            assessmentScoreResponse.id_game = map.id_game;
            assessmentScoreResponse.id_metric = num1;
            assessmentScoreResponse.metric_name = m2ostnextserviceDbContext.Database.SqlQuery<string>("SELECT  metric_value FROM tbl_theme_metric where id_metric={0} ", (object) num1).FirstOrDefault<string>();
            int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT distinct id_kpi_master FROM tbl_university_kpi_grid where id_game={0} and id_metric={1}", (object) map.id_game, (object) num1).FirstOrDefault<int>();
            switch (m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT kpi_type FROM tbl_university_kpi_master where id_kpi_master={0}", (object) num2).FirstOrDefault<int>())
            {
              case 1:
                double num3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_university_kpi_grid>("SELECT* FROM tbl_university_kpi_grid where id_game = {0} and id_kpi_master = {1}", (object) map.id_game, (object) num2).FirstOrDefault<tbl_university_kpi_grid>().start_range * (double) rightcount;
                assessmentScoreResponse.metric_score = num3;
                break;
              case 2:
                using (List<tbl_university_kpi_grid>.Enumerator enumerator = m2ostnextserviceDbContext.Database.SqlQuery<tbl_university_kpi_grid>("SELECT* FROM tbl_university_kpi_grid where id_game = {0} and id_kpi_master = {1}", (object) map.id_game, (object) num2).ToList<tbl_university_kpi_grid>().GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    tbl_university_kpi_grid current = enumerator.Current;
                    if ((double) rightcount >= current.start_range && (double) rightcount <= current.end_range)
                    {
                      double num4 = (double) (current.kpi_value * rightcount);
                      assessmentScoreResponse.metric_score = num4;
                    }
                  }
                  break;
                }
            }
            assessmentScoreResponseList.Add(assessmentScoreResponse);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return assessmentScoreResponseList;
    }

    public List<AssessmentScoreResponse> SPLscoringlogic(
      tbl_game_academic_mapping map,
      int id_brief,
      int rightcount)
    {
      List<AssessmentScoreResponse> assessmentScoreResponseList = new List<AssessmentScoreResponse>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) map.id_game
          };
          foreach (int num1 in database.SqlQuery<int>("SELECT distinct id_metric FROM tbl_university_special_point_grid where id_game={0}", objArray).ToList<int>())
          {
            AssessmentScoreResponse assessmentScoreResponse = new AssessmentScoreResponse();
            assessmentScoreResponse.id_brief = id_brief;
            assessmentScoreResponse.id_game = map.id_game;
            assessmentScoreResponse.id_metric = num1;
            assessmentScoreResponse.metric_name = m2ostnextserviceDbContext.Database.SqlQuery<string>("SELECT  name FROM tbl_special_metric_master where id_special_metric={0} ", (object) num1).FirstOrDefault<string>();
            int num2 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT distinct id_special_points FROM tbl_university_special_point_grid where id_game={0} and id_metric={1}", (object) map.id_game, (object) num1).FirstOrDefault<int>();
            switch (m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT special_value_type FROM tbl_university_special_points_master where id_special_points={0}", (object) num2).FirstOrDefault<int>())
            {
              case 1:
                double num3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_university_special_point_grid>("SELECT* FROM tbl_university_special_point_grid where id_game = {0} and id_special_points = {1}", (object) map.id_game, (object) num2).FirstOrDefault<tbl_university_special_point_grid>().start_range * (double) rightcount;
                assessmentScoreResponse.metric_score = num3;
                break;
              case 2:
                using (List<tbl_university_special_point_grid>.Enumerator enumerator = m2ostnextserviceDbContext.Database.SqlQuery<tbl_university_special_point_grid>("SELECT* FROM tbl_university_special_point_grid where id_game = {0} and id_special_points = {1}", (object) map.id_game, (object) num2).ToList<tbl_university_special_point_grid>().GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    tbl_university_special_point_grid current = enumerator.Current;
                    if ((double) rightcount >= current.start_range && rightcount <= current.end_range)
                    {
                      double num4 = (double) (current.special_value * rightcount);
                      assessmentScoreResponse.metric_score = num4;
                    }
                  }
                  break;
                }
            }
            assessmentScoreResponseList.Add(assessmentScoreResponse);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return assessmentScoreResponseList;
    }

    public List<UserBadge> getBadgeList(int id_user, int id_game)
    {
      List<UserBadge> badgeList = new List<UserBadge>();
      List<tbl_user_game_score_log> userGameScoreLogList = new List<tbl_user_game_score_log>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          double? nullable1 = new double?(0.0);
          string sql = "select  CASE WHEN (SUM(score) > 0) THEN SUM(score) ELSE 0 END score from tbl_user_game_score_log where id_game=" + id_game.ToString() + " and id_user= " + id_user.ToString() + " ";
          nullable1 = new double?(m2ostnextserviceDbContext.Database.SqlQuery<double>(sql).FirstOrDefault<double>());
          m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_theme from tbl_game_master where id_game={0}", (object) id_game).FirstOrDefault<int>();
          Database database = m2ostnextserviceDbContext.Database;
          object[] objArray = new object[1]
          {
            (object) id_game
          };
          foreach (tbl_badge_data tblBadgeData in database.SqlQuery<tbl_badge_data>("select * from tbl_badge_data where id_game={0}", objArray).ToList<tbl_badge_data>())
          {
            UserBadge userBadge = new UserBadge();
            int num1 = 1;
            double? nullable2 = nullable1;
            double requiredScore = (double) tblBadgeData.required_score;
            if (nullable2.GetValueOrDefault() >= requiredScore & nullable2.HasValue)
            {
              if (num1 <= 3)
              {
                tbl_badge_master tblBadgeMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_badge_master>("select * from tbl_badge_master where id_badge={0}", (object) tblBadgeData.id_badge).FirstOrDefault<tbl_badge_master>();
                userBadge.id_badge = tblBadgeData.id_badge;
                userBadge.badge_name = tblBadgeMaster.badge_name;
                userBadge.eligible_score = tblBadgeData.required_score;
                userBadge.badge_image = ConfigurationManager.AppSettings["BadgeBase"] + tblBadgeMaster.badge_logo;
                badgeList.Add(userBadge);
              }
              int num2 = num1 + 1;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return badgeList;
    }

    public List<LeaderBoardUserList> getUserListLeaderBoard(int id_game, int oid, int id_league)
    {
      List<LeaderBoardUserList> userListLeaderBoard = new List<LeaderBoardUserList>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          string sql = "SELECT a.id_user, (SELECT CASE WHEN (SUM(score) > 0) THEN SUM(score) ELSE 0 END FROM tbl_user_game_score_log WHERE a.id_user = id_user AND id_game = " + id_game.ToString() + ") metric_score, (SELECT CASE WHEN (SUM(score) > 0) THEN SUM(score) ELSE 0 END FROM tbl_user_game_special_metric_score_log WHERE a.id_user = id_user AND id_game =  " + id_game.ToString() + ") special_metric_score FROM tbl_user a  inner join tbl_user_league_log b on a.id_user=b.id_user WHERE a.ID_ORGANIZATION =  " + oid.ToString() + " AND a.status = 'A' and  a.id_user!=2503   AND b.id_league= " + id_league.ToString();
          userListLeaderBoard = m2ostnextserviceDbContext.Database.SqlQuery<LeaderBoardUserList>(sql).ToList<LeaderBoardUserList>();
        }
      }
      catch (Exception ex)
      {
      }
      return userListLeaderBoard;
    }

    public string getApiPost(string uri, NameValueCollection pairs)
    {
      byte[] bytes = (byte[]) null;
      using (WebClient webClient = new WebClient())
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        bytes = webClient.UploadValues(uri, pairs);
      }
      return Encoding.UTF8.GetString(bytes);
    }

    public string getApiResponseString(string api)
    {
      byte[] bytes = (byte[]) null;
      WebClient webClient1 = new WebClient();
      using (WebClient webClient2 = new WebClient())
      {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        bytes = webClient2.DownloadData(api);
      }
      return Encoding.UTF8.GetString(bytes);
    }

    public string SendNotification(
      string deviceRegIds,
      string message,
      string type,
      string image,
      string eligiblescore = "0",
      int currency = 0,
      string tag = "Badge Name",
      string title = "")
    {
      string str1 = "";
      try
      {
        string str2 = "AAAAGrnsAbc:APA91bH3oHyM5R0KrFxEexkW-DmnOr5HD1oyKmsmP_nlUjNEdlmAUu1ZF7YuD3y8NGmMx_760dgynH1hYw603TN7CAnpgD4yp59dUFOMi198H42RweTvKHYEwfVzdusHMMZuKnRvjXUW";
        string str3 = "114788401591";
        WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        webRequest.Method = "post";
        webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str2));
        webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str3));
        webRequest.ContentType = "application/json";
        new NotificationData().Image = image;
        var data = new
        {
          to = deviceRegIds,
          priority = "high",
          content_available = true,
          notification = new
          {
            body = message,
            title = title,
            badge = 1,
            icon = image,
            color = eligiblescore,
            sound = currency,
            tag = tag
          }
        };
        byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) data).ToString());
        webRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = webRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          using (WebResponse response = webRequest.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              if (responseStream != null)
              {
                using (StreamReader streamReader = new StreamReader(responseStream))
                  streamReader.ReadToEnd();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return str1;
    }
  }
}
