// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostHigherEducationRegFeedbackController
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

    public class PostHigherEducationRegFeedbackController : ApiController
  {
    public HttpResponseMessage Post([FromBody] HigherEduFeedback High)
    {
      HigherEduResponse higherEduResponse = new HigherEduResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_sul_higher_education_user_registration set ratings={0} ,  feedback={1} where id_register={2}", (object) High.ratings, (object) High.feedback, (object) High.id_register);
          higherEduResponse.Message = "Feedback updated successfully.";
          higherEduResponse.Status = "SUCCESS";
        }
      }
      catch (Exception ex)
      {
        higherEduResponse.Message = "Something went wrong.";
        higherEduResponse.Status = "FAILED";
        return namespace2.CreateResponse<HigherEduResponse>(this.Request, HttpStatusCode.OK, higherEduResponse);
      }
      return namespace2.CreateResponse<HigherEduResponse>(this.Request, HttpStatusCode.OK, higherEduResponse);
    }
  }
}
