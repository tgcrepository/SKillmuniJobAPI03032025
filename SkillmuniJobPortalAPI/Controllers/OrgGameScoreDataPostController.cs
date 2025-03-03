// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameScoreDataPostController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class OrgGameScoreDataPostController : ApiController
  {
    public HttpResponseMessage Post([FromBody] tbl_org_game_user_log ScoreData)
    {
      ScoreLOgicResponse scoreLogicResponse = new ScoreLOgicResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_org_game_user_log>("select * from tbl_org_game_user_log where id_game_content={0} and id_level={1} and id_org_game={2} and  id_user={3} and attempt_no={4}", (object) ScoreData.id_game_content, (object) ScoreData.id_level, (object) ScoreData.id_org_game, (object) ScoreData.UID, (object) ScoreData.attempt_no).ToList<tbl_org_game_user_log>().Count == 0)
          {
            ScoreData.id_score_unit = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_score_unit from  tbl_org_game_score_unit_mapping where id_org_game={0} ", (object) ScoreData.id_org_game).FirstOrDefault<int>();
            ScoreData.score_unit = m2ostnextserviceDbContext.Database.SqlQuery<string>("select unit from  tbl_org_game_score_unit_master where id_sore_unit={0} ", (object) ScoreData.id_score_unit).FirstOrDefault<string>();
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_org_game_user_log (id_user,id_game_content,score,id_score_unit,score_unit,score_type,status,updated_date_time,id_level,id_org_game,attempt_no,timetaken_to_complete,is_completed) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", (object) ScoreData.UID, (object) ScoreData.id_game_content, (object) ScoreData.score, (object) ScoreData.id_score_unit, (object) ScoreData.score_unit, (object) ScoreData.score_type, (object) "A", (object) DateTime.Now, (object) ScoreData.id_level, (object) ScoreData.id_org_game, (object) ScoreData.attempt_no, (object) ScoreData.timetaken_to_complete, (object) ScoreData.is_completed);
            scoreLogicResponse.STATUS = "SUCCESS";
            scoreLogicResponse.OID = ScoreData.OID;
            scoreLogicResponse.MESSAGE = "Successfully posted.";
            if (ScoreData.is_completed != 1)
              ;
          }
          else
          {
            scoreLogicResponse.STATUS = "FAILED";
            scoreLogicResponse.MESSAGE = "Duplicate entries.";
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
