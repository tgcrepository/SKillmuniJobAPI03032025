// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameSendSecretKeyController
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

    public class OrgGameSendSecretKeyController : ApiController
  {
    private static Random random = new Random();

    public HttpResponseMessage Get(string userId)
    {
      SendKeyResult sendKeyResult = new SendKeyResult();
      try
      {
        using (M2ostCatDbContext m2ostCatDbContext = new M2ostCatDbContext())
        {
          string OTP = OrgGameSendSecretKeyController.RandomString(4);
          tbl_profile tblProfile = m2ostCatDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_user as a inner join tbl_profile as b on a.ID_USER=b.ID_USER where a.USERID={0} and a.STATUS='A'", (object) userId).FirstOrDefault<tbl_profile>();
          tbl_email_verification_key_log verificationKeyLog = m2ostCatDbContext.Database.SqlQuery<tbl_email_verification_key_log>("select * from tbl_email_verification_key_log where id_user={0} and status='P' ", (object) tblProfile.ID_USER).FirstOrDefault<tbl_email_verification_key_log>();
          if (verificationKeyLog == null)
            m2ostCatDbContext.Database.ExecuteSqlCommand("insert into tbl_email_verification_key_log (id_user,secret_key,updated_date_time,status) values({0},{1},{2},{3})", (object) tblProfile.ID_USER, (object) OTP, (object) DateTime.Now, (object) "P");
          else
            OTP = verificationKeyLog.secret_key;
          this.SendOTP(tblProfile.EMAIL, tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME, OTP);
          sendKeyResult.STATUS = "SUCCESS";
          sendKeyResult.MESSAGE = "Secret Key has been sent to your registered email ID.";
        }
      }
      catch (Exception ex)
      {
        sendKeyResult.STATUS = "FAILED";
        sendKeyResult.MESSAGE = "Something went wrong.";
      }
      return namespace2.CreateResponse<SendKeyResult>(this.Request, HttpStatusCode.OK, sendKeyResult);
    }

    public void SendOTP(string Semail, string Name, string OTP)
    {
      try
      {
        string str = "playtolearn@thegamificationcompany.com";
        string to = Semail;
        string empty = string.Empty;
        string subject = "Email Verification - TGC";
        string body = "Dear " + Name + ",<br/><br/> Please use the secret key " + OTP + " to verify your email id. <br/><br/> Thanks and Regards,<br/>The Gamification Company";
        new SmtpClient()
        {
          Host = "smtp.gmail.com",
          Port = 587,
          EnableSsl = true,
          DeliveryMethod = SmtpDeliveryMethod.Network,
          Credentials = ((ICredentialsByHost) new NetworkCredential(str, "TGC@03012020")),
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

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[OrgGameSendSecretKeyController.random.Next(s.Length)])).ToArray<char>());
  }
}
