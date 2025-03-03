// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostAssessmentLogController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class PostAssessmentLogController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post(tbl_user_level_log level)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      string str2 = "";
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_level_log (id_user,level,attempt_no,score,bonus,total_score,updated_date_time,is_qualified,status,userid) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9})", (object) level.id_user, (object) level.level, (object) level.attempt_no, (object) level.score, (object) level.bonus, (object) level.total_score, (object) DateTime.Now, (object) level.is_qualified, (object) "A", (object) level.userid);
          foreach (tbl_user_assessment_log userAssessmentLog in level.assessment)
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_user_assessment_log (id_user,level,attempt_no,id_question,id_answer,id_user_answer,status,updated_date_time,is_right) values({0},{1},{2},{3},{4},{5},{6},{7},{8})", (object) level.id_user, (object) level.level, (object) level.attempt_no, (object) userAssessmentLog.id_question, (object) userAssessmentLog.id_answer, (object) userAssessmentLog.id_user_answer, (object) "A", (object) DateTime.Now, (object) userAssessmentLog.is_right);
        }
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
        str2 = "Failed";
      }
      finally
      {
        str2 = "Success";
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str2);
    }
  }
}
