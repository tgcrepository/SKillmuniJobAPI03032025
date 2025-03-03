// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.SULFestEventRegistrationController
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

    public class SULFestEventRegistrationController : ApiController
  {
    private static Random random = new Random();

    public HttpResponseMessage Post([FromBody] FestRegistration Fest)
    {
      FestRegResponse festRegResponse = new FestRegResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        tbl_profile tblProfile1 = new tbl_profile();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) Fest.UID).FirstOrDefault<tbl_profile>();
          if (Fest.is_new_college == 1)
            Fest.id_college = m2ostnextserviceDbContext.Database.SqlQuery<int>("insert into tbl_college_list(college_name,clg_state,status,id_city,id_organization,id_user,updated_datetime,clg_city) values({0},{1},{2},{3},{4},{5},{6},{7}) ;select max(id_college) from tbl_college_list", (object) Fest.college_name, (object) Fest.state, (object) "A", (object) Fest.id_city, (object) 130, (object) Fest.UID, (object) DateTime.Now, (object) Fest.city).FirstOrDefault<int>();
          if (Fest.register_status == 2)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from  tbl_sul_fest_event_registration where UID={0} and id_event={1} ", (object) Fest.UID, (object) Fest.id_event);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_event_registration(UID,id_college,id_state,id_city,id_event,status,updated_date_time) values({0},{1},{2},{3},{4},{5},{6}) ", (object) Fest.UID, (object) Fest.id_college, (object) Fest.id_state, (object) Fest.id_city, (object) Fest.id_event, (object) "P", (object) DateTime.Now);
          }
          else
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_event_registration(UID,id_college,id_state,id_city,id_event,status,updated_date_time) values({0},{1},{2},{3},{4},{5},{6}) ", (object) Fest.UID, (object) Fest.id_college, (object) Fest.id_state, (object) Fest.id_city, (object) Fest.id_event, (object) "P", (object) DateTime.Now);
          tblSulFestOtp.OTP = SULFestEventRegistrationController.RandomString(4);
          int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select id_otp from tbl_sul_fest_otp where id_event={0} and UID={1} ", (object) Fest.id_event, (object) Fest.UID).FirstOrDefault<int>();
          if (num > 0)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from  tbl_sul_fest_otp where id_otp={0} ", (object) num);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_otp(UID,id_event,OTP,status,updated_date_time) values({0},{1},{2},{3},{4}) ", (object) Fest.UID, (object) Fest.id_event, (object) tblSulFestOtp.OTP, (object) "A", (object) DateTime.Now);
          }
          else
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_otp(UID,id_event,OTP,status,updated_date_time) values({0},{1},{2},{3},{4}) ", (object) Fest.UID, (object) Fest.id_event, (object) tblSulFestOtp.OTP, (object) "A", (object) DateTime.Now);
          if (tblProfile2.EMAIL != Fest.Email)
          {
            this.SendOTP(Fest.Email, tblSulFestOtp.OTP, Fest.user_name);
            festRegResponse.Message = "OTP sent to your mail ID.";
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_profile set EMAIL={0}, FIRSTNAME={1} where ID_USER={2}", (object) Fest.Email, (object) Fest.user_name, (object) Fest.UID);
            festRegResponse.OTP_Status = "SENT";
          }
          else
          {
            festRegResponse.Message = "Mail ID already present.";
            festRegResponse.OTP_Status = "DECLINED";
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_sul_fest_event_registration set status={0} where UID={1} and id_event={2}", (object) "A", (object) Fest.UID, (object) Fest.id_event);
          }
        }
        festRegResponse.Status = "SUCCESS";
      }
      catch (Exception ex)
      {
        festRegResponse.Status = "FAILED";
        festRegResponse.Message = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<FestRegResponse>(this.Request, HttpStatusCode.OK, festRegResponse);
      }
      return namespace2.CreateResponse<FestRegResponse>(this.Request, HttpStatusCode.OK, festRegResponse);
    }

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[SULFestEventRegistrationController.random.Next(s.Length)])).ToArray<char>());

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
