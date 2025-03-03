// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.API1Controller
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class API1Controller : ApiController
    {
        public HttpResponseMessage Post([FromBody] API1Input inp)
        {
            API1Response apI1Response = new API1Response();
            try
            {
                using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
                {
                    apI1Response.SurveryDetails = m2ostnextserviceDbContext.Database.SqlQuery<SurveryDetails>("select ClaimNumber as claimNo  from  SurveyFeedback where EmployeeId={0} and FeedbackStatus={1} and FeedbackExpiresOn<{2}", (object)inp.employeeID, (object)"Open", (object)DateTime.Now).ToList<SurveryDetails>();
                    foreach (SurveryDetails surveryDetail in apI1Response.SurveryDetails)
                    {
                        surveryDetail.caseTypeId = "1";
                        surveryDetail.caseType = "Offshore Cash";
                        surveryDetail.expenseType = "Conveyance";
                        surveryDetail.claimApprovedAmount = "1200.00";
                        surveryDetail.claimAmountPaidOn = "12 March, 2020";
                        surveryDetail.feedbackReasonOptions = m2ostnextserviceDbContext.Database.SqlQuery<feedbackreasonmaster>("select ReasonCode as code,ReasonDescription as description from SurveyFeedbackReasonMaster where CaseTypeId={0} ORDER BY SeqNo ASC", (object)1).ToList<feedbackreasonmaster>();
                    }
                    apI1Response.ret_code = "200";
                    apI1Response.ret_message = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                apI1Response.ret_code = "500";
                apI1Response.ret_message = "Sorry we could not update the feedback status in CMS. Please try again after sometime.";
                apI1Response.SurveryDetails = (List<SurveryDetails>)null;
                return namespace2.CreateResponse<API1Response>(this.Request, HttpStatusCode.OK, apI1Response);
            }
            return namespace2.CreateResponse<API1Response>(this.Request, HttpStatusCode.OK, apI1Response);
        }
    }
}
