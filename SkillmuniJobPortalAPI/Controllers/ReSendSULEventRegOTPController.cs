// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.ReSendSULEventRegOTPController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
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

    public class ReSendSULEventRegOTPController : ApiController
  {
    private static Random random = new Random();

    public HttpResponseMessage Post([FromBody] ResendOTP OTP)
    {
      ResendOTPResponse resendOtpResponse = new ResendOTPResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        tblSulFestOtp.OTP = ReSendSULEventRegOTPController.RandomString(4);
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from  tbl_sul_fest_otp where UID={0} and id_event={1} ", (object) OTP.UID, (object) OTP.id_event);
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_otp(UID,id_event,OTP,status,updated_date_time) values({0},{1},{2},{3},{4}) ", (object) OTP.UID, (object) OTP.id_event, (object) tblSulFestOtp.OTP, (object) "A", (object) DateTime.Now);
          this.SendOTP(OTP.Email, tblSulFestOtp.OTP, OTP.user_name);
          resendOtpResponse.Status = "SUCCESS";
          resendOtpResponse.Message = "OTP sent successfully.";
        }
      }
      catch (Exception ex)
      {
        resendOtpResponse.Status = "FAILED";
        resendOtpResponse.Message = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<ResendOTPResponse>(this.Request, HttpStatusCode.OK, resendOtpResponse);
      }
      return namespace2.CreateResponse<ResendOTPResponse>(this.Request, HttpStatusCode.OK, resendOtpResponse);
    }

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[ReSendSULEventRegOTPController.random.Next(s.Length)])).ToArray<char>());

    public void SendOTP(string Semail, string OTP, string Name)
    {
      try
      {
        string str = "skillmuni@thegamificationcompany.com";
        string to = Semail;
        string empty = string.Empty;
        string subject = "OTP- SUL Event Registration";
        string body = "Hi " + Name + ", <br/><br/>Here is your OTP for the email change request.<br/><br/>" + OTP + "<br/><br/>Please do not share this OTP with anyone.<br/><br/>Thanks and Regards,<br/>Skillmuni Team";
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Credentials = ((ICredentialsByHost) new NetworkCredential(str, "03012019@Skillmuni")),
          Timeout = 30000
        }.Send(new MailMessage(str, to, subject, body)
        {
          IsBodyHtml = true
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
