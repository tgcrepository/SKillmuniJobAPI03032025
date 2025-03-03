// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.VerifyMeController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class VerifyMeController : ApiController
  {
    public HttpResponseMessage Post([FromBody] VerifyMe me)
    {
      Response response = new Response();
      int pendingUserId = new RegistrationModel().GetPendingUserID(me.UserName, me.RoleID);
      if (pendingUserId != 0)
      {
        int authcodeIdOfUser = new RegistrationModel().GetAuthcodeIDOfUser(pendingUserId);
        string authcode = new RegistrationModel().GetAuthcode(authcodeIdOfUser);
        if (me.VerificationCode.Equals(authcode))
        {
          if (new RegistrationModel().UpdateAuthcodeStatus(new Authcode()
          {
            AuthCodeID = authcodeIdOfUser,
            Code = me.VerificationCode,
            Status = "U"
          }) != 0)
          {
            if (new RegistrationModel().UpdateUserStatus(pendingUserId, me.RoleID, "A") != 0)
            {
              response.ResponseCode = "SUCCESS";
              response.ResponseAction = 1;
              response.ResponseMessage = "User account activated.";
            }
            else
            {
              response.ResponseCode = "Failure";
              response.ResponseAction = 0;
              response.ResponseMessage = "Verification Process Failed. Please try again.";
            }
          }
          else
          {
            response.ResponseCode = "Failure";
            response.ResponseAction = 0;
            response.ResponseMessage = "Verification Process Failed. Please try again.";
          }
        }
        else
        {
          response.ResponseCode = "Failure";
          response.ResponseAction = 0;
          response.ResponseMessage = "Invalid authcode. Please try again.";
        }
      }
      else
      {
        response.ResponseCode = "Failure";
        response.ResponseAction = 0;
        response.ResponseMessage = "Could not find user. Please register again.";
      }
      return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
    }
  }
}
