// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostBriefUserFeedbackController
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

    public class PostBriefUserFeedbackController : ApiController
  {
    private static Random random = new Random();

    public HttpResponseMessage Post([FromBody] tbl_brief_user_feedback_master Feedback)
    {
      APIPostRes apiPostRes = new APIPostRes();
      this.ControllerContext.RouteData.Values["controller"].ToString();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          Feedback.id_feedback = m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_brief_user_feedback_master(UID,OID,liked,disliked,reason,status,updated_date_time,feedback_type,id_brief_master) values({0},{1},{2},{3},{4},{5},{6},{7},{8});select max(id_feedback) from tbl_brief_user_feedback_master;", (object) Feedback.UID, (object) Feedback.OID, (object) Feedback.liked, (object) Feedback.disliked, (object) Feedback.reason, (object) "A", (object) DateTime.Now, (object) Feedback.feedback_type, (object) Feedback.id_brief_master);
        int num = 1;
        if (Feedback.MediaFlag == 1)
        {
          foreach (FeedbackMedia medium in Feedback.Media)
          {
            byte[] bytes = Convert.FromBase64String(medium.media);
            System.IO.File.WriteAllBytes("C:\\SulAPIBetaV2\\Content\\BriefFeedback\\" + Feedback.id_feedback.ToString() + "_" + num.ToString() + "." + medium.extension, bytes);
            medium.media = Feedback.id_feedback.ToString() + "_" + num.ToString() + "." + medium.extension;
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into  tbl_brief_feedback_media (id_feedback,media,extension,updated_time) values({0},{1},{2},{3})", (object) Feedback.id_feedback, (object) medium.media, (object) medium.extension, (object) DateTime.Now);
            ++num;
          }
        }
        apiPostRes.Message = "Thanks for your feedback.";
        apiPostRes.Status = "SUCCESS";
      }
      catch (Exception ex)
      {
        apiPostRes.Status = "FAILED";
        apiPostRes.Message = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<APIPostRes>(this.Request, HttpStatusCode.OK, apiPostRes);
      }
      return namespace2.CreateResponse<APIPostRes>(this.Request, HttpStatusCode.OK, apiPostRes);
    }

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[PostBriefUserFeedbackController.random.Next(s.Length)])).ToArray<char>());

    public void SendOTP(string Semail, string OTP, string Name)
    {
      try
      {
        string str = "skillmuni@thegamificationcompany.com";
        string to = Semail;
        string empty = string.Empty;
        string subject = "OTP- SUL Event Registration";
        string body = "Thanks for your intrest in Skillmuni University Event registration. Please use below OTP to verify your email ID and complete the process. <br /> OTP is <b>" + OTP + "</b>";
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
