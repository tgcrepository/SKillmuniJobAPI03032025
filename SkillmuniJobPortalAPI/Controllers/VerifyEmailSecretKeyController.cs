// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.VerifyEmailSecretKeyController
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
    public class VerifyEmailSecretKeyController : ApiController
  {
    public HttpResponseMessage Post([FromBody] VerifyEmailKey PostData)
    {
      VerifyOTPResponse verifyOtpResponse = new VerifyOTPResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      tbl_sul_fest_master tblSulFestMaster = new tbl_sul_fest_master();
      tbl_sul_fest_event_registration eventRegistration = new tbl_sul_fest_event_registration();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_email_verification_key_log>("select * from tbl_email_verification_key_log where id_user={0} and status='P'", (object) PostData.UID).FirstOrDefault<tbl_email_verification_key_log>().secret_key == PostData.SecretKey)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_email_verification_key_log set status='A' where id_user={0} and status='P' ", (object) PostData.UID);
            verifyOtpResponse.Message = "Email verified successfully.";
            verifyOtpResponse.Status = "SUCCESS";
          }
          else
          {
            verifyOtpResponse.Message = "Entered Key is wrong.";
            verifyOtpResponse.Status = "FAILED";
          }
        }
      }
      catch (Exception ex)
      {
        verifyOtpResponse.Status = "FAILED";
        verifyOtpResponse.Message = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<VerifyOTPResponse>(this.Request, HttpStatusCode.OK, verifyOtpResponse);
      }
      return namespace2.CreateResponse<VerifyOTPResponse>(this.Request, HttpStatusCode.OK, verifyOtpResponse);
    }
  }
}
