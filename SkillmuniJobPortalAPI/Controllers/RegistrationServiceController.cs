// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.RegistrationServiceController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class RegistrationServiceController : ApiController
  {
    public HttpResponseMessage Post([FromBody] Registration registration)
    {
      Response response = new Response();
      string str = "";
      registration.Password = registration.Password.ToMD5Hash();
      if (new RegistrationModel().CheckUserExist(registration.UserName, registration.Role))
      {
        response.ResponseCode = "FAILURE";
        response.ResponseAction = 0;
        response.ResponseMessage = "Given user name already present in the system. Please try with another user name.";
      }
      else
      {
        Authcode authcode = new Authcode();
        Authcode activeAuthcode = new RegistrationModel().GetActiveAuthcode();
        if (activeAuthcode != null)
        {
          str = activeAuthcode.Code.ToString();
          int user = new RegistrationModel().CreateUser(registration, activeAuthcode, "P");
          if (user > 0)
          {
            activeAuthcode.Status = "R";
            if (new RegistrationModel().UpdateAuthcodeStatus(activeAuthcode) == 1)
            {
              if (new RegistrationModel().CreateProfile(user, registration) == 1)
              {
                int deviceTypeId = new RegistrationModel().GetDeviceTypeID(registration.DeviceType);
                if (deviceTypeId != 0)
                {
                  int actionId = new RegistrationModel().GetActionID("Registration");
                  if (actionId != 0)
                  {
                    if (new RegistrationModel().UpdateUserLog(user, deviceTypeId, registration.DeviceID, actionId) != 0)
                    {
                      response.ResponseCode = "SUCCESS";
                      response.ResponseAction = 1;
                      response.ResponseMessage = "User successfully registered with pending status. Please activate the account.";
                    }
                    else
                    {
                      response.ResponseCode = "SUCCESS";
                      response.ResponseAction = 2;
                      response.ResponseMessage = "User successfully registered with pending status, but could not log details. Please activate the account.";
                    }
                  }
                  else
                  {
                    response.ResponseCode = "SUCCESS";
                    response.ResponseAction = 2;
                    response.ResponseMessage = "User successfully registered with pending status, but could not log details. Please activate the account.";
                  }
                }
                else
                {
                  response.ResponseCode = "SUCCESS";
                  response.ResponseAction = 2;
                  response.ResponseMessage = "User successfully registered with pending status, but could not capture device deails. Please activate the account and update device details.";
                }
              }
              else
              {
                response.ResponseCode = "SUCCESS";
                response.ResponseAction = 2;
                response.ResponseMessage = "User successfully registered with pending status, but profile could not be updated. Please activate the account and update profile.";
              }
            }
            else if (new RegistrationModel().DeleteUserRollback(user) == 1)
            {
              response.ResponseCode = "FAILURE";
              response.ResponseAction = 0;
              response.ResponseMessage = "Registration Failed. Could not generate auth code. Please try again later.";
            }
            else
            {
              response.ResponseCode = "FAILURE";
              response.ResponseAction = 0;
              response.ResponseMessage = "Registration Failed. Could not delete user while roll back. Please contact administrator.";
            }
          }
          else
          {
            response.ResponseCode = "FAILURE";
            response.ResponseAction = 0;
            response.ResponseMessage = "Registration Failed. Could not create user. Please try again later.";
          }
        }
        else
        {
          response.ResponseCode = "FAILURE";
          response.ResponseAction = 0;
          response.ResponseMessage = "Registration Failed. Could not generate auth code. Please try again later.";
        }
      }
      if (response.ResponseCode.Equals("SUCCESS"))
        new SmtpClient()
        {
          Host = ConfigurationManager.AppSettings["MailSMTP"].ToString(),
          Port = 587,
          UseDefaultCredentials = false,
          Credentials = ((ICredentialsByHost) new NetworkCredential(ConfigurationManager.AppSettings["MailUserName"].ToString(), ConfigurationManager.AppSettings["MailPassword"].ToString())),
          EnableSsl = true
        }.Send(new MailMessage()
        {
          To = {
            registration.UserName
          },
          From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString()),
          Subject = ConfigurationManager.AppSettings["MailSendAuthCodeSubject"].ToString(),
          Body = "<p>Your Verification Code for Skillmuni access is:<h5> UserName: " + str + "</h5></p>",
          IsBodyHtml = true
        });
      return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
    }
  }
}
