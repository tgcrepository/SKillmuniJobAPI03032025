// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getGameContentAssessmentController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
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

    public class getGameContentAssessmentController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_brief_master)
    {
      BriefResource briefResource = new BriefResource();
      BriefResource briefData;
      try
      {
        string str = "";
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select brief_code from tbl_brief_master where id_brief_master ={0}", (object) id_brief_master).ToString();
        int OID1 = OID;
        int UID1 = UID;
        string brf = str;
        UserScoreResponse userScoreResponse = new UserScoreResponse();
        briefData = new BriefModel().getBriefData(brf, UID1, OID1);
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
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<BriefResource>(this.Request, HttpStatusCode.OK, briefData);
    }
  }
}
