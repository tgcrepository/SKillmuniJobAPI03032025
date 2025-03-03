// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.SulHigherEducationRegistrationController
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

    public class SulHigherEducationRegistrationController : ApiController
  {
    public HttpResponseMessage Post([FromBody] HigherEduReg High)
    {
      HigherEduResponse higherEduResponse = new HigherEduResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          higherEduResponse.id_register = m2ostnextserviceDbContext.Database.SqlQuery<int>("insert into tbl_sul_higher_education_user_registration(id_higher_education,id_user,status,update_date_time,slot) values({0},{1},{2},{3},{4});select max(id_register) from tbl_sul_higher_education_user_registration", (object) High.id_higher_education, (object) High.id_user, (object) "A", (object) DateTime.Now, (object) High.slot).FirstOrDefault<int>();
          if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where id_event={0} and UID={1}", (object) High.id_event, (object) High.id_user).FirstOrDefault<tbl_sul_fest_event_registration>() == null)
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_sul_fest_event_registration(UID,id_college,id_state,id_city,id_event,status,updated_date_time) values({0},{1},{2},{3},{4},{5},{6}) ", (object) High.id_user, (object) 0, (object) 0, (object) 0, (object) High.id_event, (object) "A", (object) DateTime.Now);
          tbl_sul_higher_education_user_registration reg = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_higher_education_user_registration>("select * from tbl_sul_higher_education_user_registration where id_higher_education={0} and id_user={1}", (object) High.id_higher_education, (object) High.id_user).FirstOrDefault<tbl_sul_higher_education_user_registration>();
          tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) High.id_user).FirstOrDefault<tbl_profile>();
          tbl_sul_higher_education_master sem = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_higher_education_master>("select * from  tbl_sul_higher_education_master where id_higher_education={0} ", (object) High.id_higher_education).FirstOrDefault<tbl_sul_higher_education_master>();
          tbl_sul_fest_master mas = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from  tbl_sul_fest_master where id_event={0} ", (object) High.id_event).FirstOrDefault<tbl_sul_fest_master>();
          this.SendOTP(tblProfile.EMAIL, tblProfile.FIRSTNAME, sem, reg, mas);
          higherEduResponse.Message = "Registered successfully.";
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

    public void SendOTP(
      string Semail,
      string Name,
      tbl_sul_higher_education_master sem,
      tbl_sul_higher_education_user_registration reg,
      tbl_sul_fest_master mas)
    {
      try
      {
        string str = "skillmuni@thegamificationcompany.com";
        string to = Semail;
        string empty = string.Empty;
        string subject = "Your slot has been successfully booked!";
        string body = "Dear " + Name + ",<br/><br/> Your slot has been booked. Below are the details of your booked slot;<br/><br/>Registration ID: " + reg.id_register.ToString() + "<br/><br/>What:HIGHER EDUCATION COUNSELING<br/><br/>Where: " + sem.location + "<br/><br/>When:" + mas.event_start_date.ToString() + "|Slot- " + reg.slot + "<br/><br/>Thanks and Regards,<br/>Skillmuni Team";
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
