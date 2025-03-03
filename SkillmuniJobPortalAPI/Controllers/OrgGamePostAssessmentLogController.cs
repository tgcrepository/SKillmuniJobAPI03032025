// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGamePostAssessmentLogController
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

    public class OrgGamePostAssessmentLogController : ApiController
  {
    public HttpResponseMessage Post([FromBody] assessJson inpdata)
    {
      List<tbl_org_game_user_assessment_log> userAssessmentLogList = JsonConvert.DeserializeObject<List<tbl_org_game_user_assessment_log>>(inpdata.log_string);
      ScoreLOgicResponse scoreLogicResponse = new ScoreLOgicResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_assessment_log>("select * from tbl_org_game_user_assessment_log where id_org_game={0} and id_org_game_content={1} and attempt_no={2} and id_user={3} ", (object) userAssessmentLogList[0].id_org_game, (object) userAssessmentLogList[0].id_org_game_content, (object) userAssessmentLogList[0].attempt_no, (object) userAssessmentLogList[0].id_user).ToList<tbl_org_game_user_assessment_log>().Count == 0)
          {
            foreach (tbl_org_game_user_assessment_log userAssessmentLog in userAssessmentLogList)
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_org_game_user_assessment_log (id_org_game,id_org_game_content,attempt_no,id_org_game_level,id_question,id_answer_selected,is_correct,status,updated_date_time,id_user) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", (object) userAssessmentLog.id_org_game, (object) userAssessmentLog.id_org_game_content, (object) userAssessmentLog.attempt_no, (object) userAssessmentLog.id_org_game_level, (object) userAssessmentLog.id_question, (object) userAssessmentLog.id_answer_selected, (object) userAssessmentLog.is_correct, (object) "A", (object) DateTime.Now, (object) userAssessmentLog.id_user);
            scoreLogicResponse.STATUS = "SUCCESS";
            scoreLogicResponse.MESSAGE = "Successfully posted.";
          }
          else
          {
            scoreLogicResponse.STATUS = "FAILED";
            scoreLogicResponse.MESSAGE = "Duplicate Entries.";
          }
        }
      }
      catch (Exception ex)
      {
        scoreLogicResponse.STATUS = "FAILED";
        scoreLogicResponse.MESSAGE = "Something went wrong.";
      }
      return namespace2.CreateResponse<ScoreLOgicResponse>(this.Request, HttpStatusCode.OK, scoreLogicResponse);
    }
  }
}
