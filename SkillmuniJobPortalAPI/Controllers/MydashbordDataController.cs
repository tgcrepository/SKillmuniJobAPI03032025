// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.MydashbordDataController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class MydashbordDataController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      MydashboardDataResponse mydashboardDataResponse = new MydashboardDataResponse();
      List<MasterLeaderBoardData> source1 = new List<MasterLeaderBoardData>();
      List<MydashoardEpisodeData> mydashoardEpisodeDataList = new List<MydashoardEpisodeData>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        mydashboardDataResponse.overall_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1", (object) UID).FirstOrDefault<int>();
        List<tbl_user_quiz_log> tblUserQuizLogList = new List<tbl_user_quiz_log>();
        List<tbl_user_quiz_log> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_quiz_log>("SELECT * FROM tbl_user_quiz_log  where id_org={0} group by id_user", (object) OID).ToList<tbl_user_quiz_log>();
        foreach (tbl_user_quiz_log tblUserQuizLog in list1)
        {
          MasterLeaderBoardData masterLeaderBoardData = new MasterLeaderBoardData();
          tbl_profile tblProfile = new tbl_profile();
          masterLeaderBoardData.id_user = tblUserQuizLog.id_user;
          masterLeaderBoardData.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1", (object) tblUserQuizLog.id_user).FirstOrDefault<int>();
          if (masterLeaderBoardData.total_score > 0)
            source1.Add(masterLeaderBoardData);
        }
        List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
        Database database = m2ostnextserviceDbContext.Database;
        object[] objArray = new object[1]{ (object) OID };
        foreach (tbl_brief_master tblBriefMaster in database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where id_organization={0}", objArray).ToList<tbl_brief_master>())
        {
          List<MasterLeaderBoardData> source2 = new List<MasterLeaderBoardData>();
          foreach (tbl_user_quiz_log tblUserQuizLog in list1)
          {
            MasterLeaderBoardData masterLeaderBoardData = new MasterLeaderBoardData();
            masterLeaderBoardData.id_brief_master = tblBriefMaster.id_brief_master;
            masterLeaderBoardData.id_user = tblUserQuizLog.id_user;
            masterLeaderBoardData.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1 and id_brief={1}", (object) tblUserQuizLog.id_user, (object) tblBriefMaster.id_brief_master).FirstOrDefault<int>();
            if (masterLeaderBoardData.total_score > 0)
              source2.Add(masterLeaderBoardData);
          }
          if (source2 != null)
          {
            List<MasterLeaderBoardData> list2 = source2.OrderByDescending<MasterLeaderBoardData, int>((Func<MasterLeaderBoardData, int>) (x => x.total_score)).ToList<MasterLeaderBoardData>();
            int num = 1;
            foreach (MasterLeaderBoardData masterLeaderBoardData in list2)
            {
              if (masterLeaderBoardData.id_user == UID)
                mydashoardEpisodeDataList.Add(new MydashoardEpisodeData()
                {
                  Episode_rank = num,
                  Episod_score = masterLeaderBoardData.total_score,
                  id_brief_master = masterLeaderBoardData.id_brief_master,
                  episode_sequence = tblBriefMaster.episode_sequence
                });
              ++num;
            }
          }
        }
        mydashboardDataResponse.Epi = mydashoardEpisodeDataList;
        if (mydashboardDataResponse.Epi != null)
        {
          foreach (MydashoardEpisodeData mydashoardEpisodeData in mydashboardDataResponse.Epi)
          {
            List<tbl_brief_question> tblBriefQuestionList = new List<tbl_brief_question>();
            List<tbl_brief_question> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question>("select * from tbl_brief_question where id_brief_master={0} ", (object) mydashoardEpisodeData.id_brief_master).ToList<tbl_brief_question>();
            List<MydashoardQuestionLog> mydashoardQuestionLogList = new List<MydashoardQuestionLog>();
            foreach (tbl_brief_question tblBriefQuestion in list3)
            {
              MydashoardQuestionLog mydashoardQuestionLog = new MydashoardQuestionLog();
              mydashoardQuestionLog.id_question = tblBriefQuestion.id_brief_question;
              List<tbl_user_quiz_log> list4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_quiz_log>("select * from tbl_user_quiz_log where id_question={0} and id_user={1}", (object) tblBriefQuestion.id_brief_question, (object) UID).ToList<tbl_user_quiz_log>();
              mydashoardQuestionLog.attempts_count = list4.Count;
              mydashoardQuestionLog.question_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1 and id_question={1}", (object) UID, (object) tblBriefQuestion.id_brief_question).FirstOrDefault<int>();
              mydashoardQuestionLogList.Add(mydashoardQuestionLog);
            }
            mydashoardEpisodeData.question = mydashoardQuestionLogList;
          }
        }
      }
      if (source1 != null)
      {
        List<MasterLeaderBoardData> list = source1.OrderByDescending<MasterLeaderBoardData, int>((Func<MasterLeaderBoardData, int>) (x => x.total_score)).ToList<MasterLeaderBoardData>();
        int num = 1;
        foreach (MasterLeaderBoardData masterLeaderBoardData in list)
        {
          if (masterLeaderBoardData.id_user == UID)
            mydashboardDataResponse.overall_rank = num;
          ++num;
        }
      }
      return namespace2.CreateResponse<MydashboardDataResponse>(this.Request, HttpStatusCode.OK, mydashboardDataResponse);
    }
  }
}
