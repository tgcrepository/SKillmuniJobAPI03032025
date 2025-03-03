// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.API2Controller
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

    public class API2Controller : ApiController
  {
    public HttpResponseMessage Post([FromBody] API2Input inp)
    {
      API2Response apI2Response = new API2Response();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into SurveyFeedbackSubmitJson (JsonString,SubmittedOn,SubmittedStatus) values({0},{1},{2})", (object) inp, (object) DateTime.Now, (object) "SUCCESS");
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update SurveyFeedback set FeedbackStatus={0},Ratings={1},FeedbackCapturedOn={2} where employeeID={3} and claimNo={4}", (object) "CLOSED", (object) inp.rating, (object) DateTime.Now, (object) inp.employeeID, (object) inp.claimNo);
          foreach (feedbackReasonSelected feedbackReasonSelected in inp.feedbackReasonSelected)
          {
            int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select ID from SurveyFeedback where ClaimNumber={0} and EmployeeId={1}", (object) inp.claimNo, (object) inp.employeeID).FirstOrDefault<int>();
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into SurveyFeedbackReasonOptions (SurveyFeedbackID,ReasonCode) values({0},{1})", (object) num, (object) feedbackReasonSelected.code);
          }
          apI2Response.ret_code = "200";
          apI2Response.ret_message = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
        apI2Response.ret_code = "500";
        apI2Response.ret_message = "Sorry we could not update the feedback status in CMS. Please try again after sometime.";
        return namespace2.CreateResponse<API2Response>(this.Request, HttpStatusCode.OK, apI2Response);
      }
      return namespace2.CreateResponse<API2Response>(this.Request, HttpStatusCode.OK, apI2Response);
    }
  }
}
