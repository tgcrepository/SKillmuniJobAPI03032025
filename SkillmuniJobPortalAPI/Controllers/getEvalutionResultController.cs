// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getEvalutionResultController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
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

    public class getEvalutionResultController : ApiController
  {
    private m2ostnextserviceDbContext db = new m2ostnextserviceDbContext();

    public HttpResponseMessage Get(string crf, int atm, int UID, int OID, int id_job = 0)
    {
      ResultResponseBody resultResponseBody = new ResultResponseBody();
      CEReturnResponse ceReturnResponse = new CEReturnResponse();
      tbl_ce_career_evaluation_master evaluationMaster = this.db.Database.SqlQuery<tbl_ce_career_evaluation_master>("select * from tbl_ce_career_evaluation_master where lower(career_evaluation_code)=lower('" + crf + "') and id_organization=" + OID.ToString() + " and status='A' limit 1").FirstOrDefault<tbl_ce_career_evaluation_master>();
      string sql = "SELECT * FROM tbl_ce_evaluation_log WHERE id_organization = " + OID.ToString() + " AND id_user = " + UID.ToString() + " AND updated_date_time in (SELECT MAX( updated_date_time ) FROM tbl_ce_evaluation_log WHERE id_job =" + id_job.ToString() + " and id_user =" + UID.ToString() + " and  id_ce_career_evaluation_master = " + evaluationMaster.id_ce_career_evaluation_master.ToString() + " )   AND id_ce_career_evaluation_master =  " + evaluationMaster.id_ce_career_evaluation_master.ToString() + " ";
      if (id_job > 0)
        sql = sql + " and id_job=" + id_job.ToString();
      tbl_ce_evaluation_log tblCeEvaluationLog = this.db.Database.SqlQuery<tbl_ce_evaluation_log>(sql).FirstOrDefault<tbl_ce_evaluation_log>();
      if (tblCeEvaluationLog != null)
      {
        resultResponseBody.status = "success";
        ceReturnResponse = JsonConvert.DeserializeObject<CEReturnResponse>(tblCeEvaluationLog.json_response);
        foreach (AnswerKeyBlock answerKeyBlock in ceReturnResponse.answerKeyBlock)
        {
          string str = ConfigurationManager.AppSettings["BRIEFIMAGE"].ToString() + "ANSWERKEY/";
          if (answerKeyBlock.aklogo == null)
            answerKeyBlock.aklogo = str + answerKeyBlock.key_code + ".png";
          int? ceAssessmentType = evaluationMaster.ce_assessment_type;
          int num = 2;
          if (ceAssessmentType.GetValueOrDefault() == num & ceAssessmentType.HasValue)
            answerKeyBlock.Description = this.db.Database.SqlQuery<string>("SELECT description FROM tbl_ce_evalution_answer_key where key_code={0} and id_organization={1}", (object) answerKeyBlock.key_code, (object) OID).FirstOrDefault<string>();
        }
        ceReturnResponse.CETime = tblCeEvaluationLog.cetimespan;
        resultResponseBody.data = ceReturnResponse;
        if (ceReturnResponse.answerKeyBlock.Count > 0)
          ceReturnResponse.answerKeyBlock = ceReturnResponse.answerKeyBlock.OrderByDescending<AnswerKeyBlock, int>((Func<AnswerKeyBlock, int>) (x => x.job_point)).ToList<AnswerKeyBlock>();
      }
      else
      {
        resultResponseBody.status = "failure";
        resultResponseBody.data = (CEReturnResponse) null;
      }
      return namespace2.CreateResponse<CEReturnResponse>(this.Request, HttpStatusCode.OK, ceReturnResponse);
    }
  }
}
