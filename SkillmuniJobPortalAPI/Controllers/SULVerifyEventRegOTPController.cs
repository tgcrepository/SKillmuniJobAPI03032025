// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.SULVerifyEventRegOTPController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;
    public class SULVerifyEventRegOTPController : ApiController
  {
    public HttpResponseMessage Post([FromBody] VerifyOTP OTP)
    {
      VerifyOTPResponse verifyOtpResponse = new VerifyOTPResponse();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      tbl_sul_fest_master tblSulFestMaster = new tbl_sul_fest_master();
      tbl_sul_fest_event_registration eventRegistration = new tbl_sul_fest_event_registration();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp1 = new tbl_sul_fest_otp();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_sul_fest_otp tblSulFestOtp2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_otp>("select * from tbl_sul_fest_otp where id_event={0} and UID={1} ", (object) OTP.id_event, (object) OTP.UID).FirstOrDefault<tbl_sul_fest_otp>();
          tbl_sul_fest_event_registration reg = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where UID={0} and id_event={1} ", (object) OTP.UID, (object) OTP.id_event).FirstOrDefault<tbl_sul_fest_event_registration>();
          DateTime dateTime = DateTime.Now.AddMinutes((double) m2ostnextserviceDbContext.Database.SqlQuery<int>("select expiry_mins from tbl_sul_fest_otp_expiry_config where status={0}", (object) "A").FirstOrDefault<int>());
          if (tblSulFestOtp2.OTP == OTP.OTP && tblSulFestOtp2.updated_date_time <= dateTime)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_sul_fest_event_registration set status={0} where UID={1} and id_event={2}", (object) "A", (object) OTP.UID, (object) OTP.id_event);
            verifyOtpResponse.Message = "OTP verified successfully.";
            verifyOtpResponse.Status = "SUCCESS";
            string Semail = m2ostnextserviceDbContext.Database.SqlQuery<string>("SELECT EMAIL FROM tbl_profile where ID_USER={0}", (object) OTP.UID).FirstOrDefault<string>();
            string Name = m2ostnextserviceDbContext.Database.SqlQuery<string>("SELECT FIRSTNAME FROM tbl_profile where ID_USER={0}", (object) OTP.UID).FirstOrDefault<string>();
            List<tbl_user_gcm_log> tblUserGcmLogList = new List<tbl_user_gcm_log>();
            List<tbl_user_gcm_log> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_gcm_log>("select * from tbl_user_gcm_log where id_user={0} and status='A'", (object) OTP.UID).ToList<tbl_user_gcm_log>();
            tbl_sul_fest_master fes = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where id_event={0}", (object) OTP.id_event).FirstOrDefault<tbl_sul_fest_master>();
            foreach (tbl_user_gcm_log tblUserGcmLog in list)
              this.SendNotification(tblUserGcmLog.GCMID, "Successfully registered for the event '" + fes.event_title + "'", fes.event_title, ConfigurationManager.AppSettings["FestEventLogo"].ToString() + fes.event_logo);
            this.SendOTP(Semail, Name, fes, reg);
          }
          else if (tblSulFestOtp2.OTP == OTP.OTP)
          {
            verifyOtpResponse.Message = "OTP expired. Please click on resend and verify again.";
            verifyOtpResponse.Status = "FAILED";
          }
          else
          {
            verifyOtpResponse.Message = "Entered OTP is wrong.";
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

    public void SendOTP(
      string Semail,
      string Name,
      tbl_sul_fest_master fes,
      tbl_sul_fest_event_registration reg)
    {
      try
      {
        string str1 = "skillmuni@thegamificationcompany.com";
        string to = Semail;
        string str2 = string.Empty;
        using (StreamReader streamReader = new StreamReader(ConfigurationManager.AppSettings["mail_sul_event_reg_thanks"] ?? ""))
          str2 = streamReader.ReadToEnd();
        string newValue = "";
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          newValue = m2ostnextserviceDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) fes.id_college).FirstOrDefault<string>();
        string body = str2.Replace("{ID}", Convert.ToString(reg.id_event)).Replace("{TITLE}", fes.event_title).Replace("{COLLEGE}", newValue).Replace("{ADDRESS}", fes.address).Replace("{STRT_DATE}", Convert.ToString(fes.event_start_date)).Replace("{END_DATE}", Convert.ToString(fes.event_end_date));
        string subject = "SUL Event registered Successfully";
        string str3 = "Hi " + Name + ",<br/><br/> You have been successfully registered  for  Skillmuni University Event. ";
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Credentials = ((ICredentialsByHost) new NetworkCredential(str1, "03012019@Skillmuni")),
          Timeout = 30000
        }.Send(new MailMessage(str1, to, subject, body)
        {
          IsBodyHtml = true
        });
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public string SendNotification(
      string deviceRegIds,
      string message,
      string title,
      string image)
    {
      string str1 = "";
      try
      {
        string str2 = "AAAAGrnsAbc:APA91bH3oHyM5R0KrFxEexkW-DmnOr5HD1oyKmsmP_nlUjNEdlmAUu1ZF7YuD3y8NGmMx_760dgynH1hYw603TN7CAnpgD4yp59dUFOMi198H42RweTvKHYEwfVzdusHMMZuKnRvjXUW";
        string str3 = "114788401591";
        WebRequest webRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        webRequest.Method = "post";
        webRequest.Headers.Add(string.Format("Authorization: key={0}", (object) str2));
        webRequest.Headers.Add(string.Format("Sender: id={0}", (object) str3));
        webRequest.ContentType = "application/json";
        NotificationData notificationData = new NotificationData();
        var data = new
        {
          to = deviceRegIds,
          priority = "high",
          content_available = true,
          data = new
          {
            body = message,
            title = title,
            icon = image
          }
        };
        byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) data).ToString());
        webRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = webRequest.GetRequestStream())
        {
          requestStream.Write(bytes, 0, bytes.Length);
          using (WebResponse response = webRequest.GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
            {
              if (responseStream != null)
              {
                using (StreamReader streamReader = new StreamReader(responseStream))
                  streamReader.ReadToEnd();
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
      return str1;
    }
  }
}
