// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.EpisodewiseLeaderBoardController
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

    public class EpisodewiseLeaderBoardController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_brief)
    {
      EpisodewiseLeaderBoardResponse leaderBoardResponse = new EpisodewiseLeaderBoardResponse();
      List<EpisodewiseLeaderRankList> episodewiseLeaderRankListList = new List<EpisodewiseLeaderRankList>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<tbl_user_quiz_log> tblUserQuizLogList = new List<tbl_user_quiz_log>();
        List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
        leaderBoardResponse.EpisodeId = id_brief;
        List<MasterLeaderBoardData> masterLeaderBoardDataList = new List<MasterLeaderBoardData>();
        Database database = m2ostnextserviceDbContext.Database;
        object[] objArray = new object[2]
        {
          (object) OID,
          (object) id_brief
        };
        foreach (tbl_user_quiz_log tblUserQuizLog in database.SqlQuery<tbl_user_quiz_log>("SELECT * FROM tbl_user_quiz_log  where id_org={0} and id_brief={1} group by id_user", objArray).ToList<tbl_user_quiz_log>())
        {
          EpisodewiseLeaderRankList episodewiseLeaderRankList = new EpisodewiseLeaderRankList();
          episodewiseLeaderRankList.id_user = tblUserQuizLog.id_user;
          tbl_profile tblProfile1 = new tbl_profile();
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUserQuizLog.id_user).FirstOrDefault<tbl_profile>();
          if (tblProfile2 != null)
          {
            episodewiseLeaderRankList.username = tblProfile2.FIRSTNAME;
            episodewiseLeaderRankList.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile2.PROFILE_IMAGE;
          }
          else
          {
            episodewiseLeaderRankList.username = Convert.ToString(tblUserQuizLog.id_user);
            episodewiseLeaderRankList.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
          }
          episodewiseLeaderRankList.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1 and id_brief={1}", (object) tblUserQuizLog.id_user, (object) id_brief).FirstOrDefault<int>();
          if (episodewiseLeaderRankList.total_score > 0)
            episodewiseLeaderRankListList.Add(episodewiseLeaderRankList);
        }
        leaderBoardResponse.RankList = episodewiseLeaderRankListList;
        leaderBoardResponse.RankList = leaderBoardResponse.RankList.OrderByDescending<EpisodewiseLeaderRankList, int>((Func<EpisodewiseLeaderRankList, int>) (x => x.total_score)).ToList<EpisodewiseLeaderRankList>();
        int num = 1;
        foreach (EpisodewiseLeaderRankList rank in leaderBoardResponse.RankList)
        {
          rank.rank = num;
          ++num;
        }
      }
      return namespace2.CreateResponse<EpisodewiseLeaderBoardResponse>(this.Request, HttpStatusCode.OK, leaderBoardResponse);
    }
  }
}
