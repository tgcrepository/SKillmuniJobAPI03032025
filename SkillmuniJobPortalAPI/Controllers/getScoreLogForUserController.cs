// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getScoreLogForUserController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class getScoreLogForUserController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int episodeID)
    {
      List<tbl_question_episode_mapping> questionEpisodeMappingList = new List<tbl_question_episode_mapping>();
      List<QuestionResponse> questionResponseList = new List<QuestionResponse>();
      MydashboardResponse mydashboardResponse = new MydashboardResponse();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<QuestionResponse> list = m2ostnextserviceDbContext.Database.SqlQuery<QuestionResponse>("select id_brief_question,brief_question,id_organization,id_brief_master from tbl_brief_question where id_brief_master={0}", (object) episodeID).ToList<QuestionResponse>();
        foreach (QuestionResponse questionResponse in list)
        {
          questionResponse.is_question_active = 1;
          questionResponse.attempt_log = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_quiz_log>("select * from tbl_user_quiz_log where id_user={0} and id_question={1}", (object) UID, (object) questionResponse.id_brief_question).ToList<tbl_user_quiz_log>();
          questionResponse.answer = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_answer>("select * from tbl_brief_answer where id_brief_question={0}", (object) questionResponse.id_brief_question).ToList<tbl_brief_answer>();
          if (questionResponse.answer.Count == 2)
          {
            if (questionResponse.attempt_log.Count >= 1)
            {
              questionResponse.is_question_active = 0;
              questionResponse.max_score = 0;
            }
            else
              questionResponse.max_score = 10;
          }
          else if (questionResponse.answer.Count == 3)
          {
            if (questionResponse.attempt_log.Count >= 2)
            {
              questionResponse.is_question_active = 0;
              questionResponse.max_score = 0;
            }
            else
              questionResponse.max_score = questionResponse.attempt_log.Count != 1 ? 20 : 10;
          }
          else if (questionResponse.answer.Count == 4)
          {
            if (questionResponse.attempt_log.Count >= 3)
              questionResponse.is_question_active = 0;
            else
              questionResponse.max_score = questionResponse.attempt_log.Count != 2 ? (questionResponse.attempt_log.Count != 1 ? 30 : 20) : 10;
          }
          foreach (tbl_user_quiz_log tblUserQuizLog in questionResponse.attempt_log)
          {
            if (tblUserQuizLog.is_correct == 1)
            {
              questionResponse.is_question_active = 0;
              break;
            }
          }
        }
        mydashboardResponse.TotalScore = m2ostnextserviceDbContext.Database.SqlQuery<int>("select COALESCE(SUM(score),0) total from tbl_user_quiz_log where id_user={0} and is_correct=1", (object) UID).FirstOrDefault<int>();
        tbl_profile tblProfile1 = new tbl_profile();
        tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
        if (tblProfile2 != null)
        {
          mydashboardResponse.Name = tblProfile2.FIRSTNAME;
          mydashboardResponse.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile2.PROFILE_IMAGE;
        }
        else
        {
          mydashboardResponse.Name = Convert.ToString(UID);
          mydashboardResponse.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
        }
        mydashboardResponse.Question = list;
        mydashboardResponse.UID = UID;
        mydashboardResponse.OID = OID;
      }
      return namespace2.CreateResponse<MydashboardResponse>(this.Request, HttpStatusCode.OK, mydashboardResponse);
    }
  }
}
