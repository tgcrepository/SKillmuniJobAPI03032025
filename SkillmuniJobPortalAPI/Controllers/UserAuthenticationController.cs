// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UserAuthenticationController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
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
    public class UserAuthenticationController : ApiController
  {
    private static Random random = new Random();

    public HttpResponseMessage Post([FromBody] UserCredentials user)
    {
      AuthResponse authResponse = new AuthResponse();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext1 = new m2ostnextserviceDbContext())
        {
          tbl_user tblUser = m2ostnextserviceDbContext1.Database.SqlQuery<tbl_user>("select * from tbl_user where USERID={0} and PASSWORD={1}", (object) user.USERID, (object) user.PASSWORD).FirstOrDefault<tbl_user>();
          if (tblUser != null)
          {
            tbl_profile tblProfile1 = m2ostnextserviceDbContext1.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            authResponse.AuthStatus = "SUCCESS";
            authResponse.AuthMessage = "User authenticated successfully.";
            authResponse.FIRST_NAME = tblProfile1.FIRSTNAME;
            authResponse.IDUSER = tblProfile1.ID_USER;
            authResponse.LAST_NAME = tblProfile1.LASTNAME;
            authResponse.PROFILE_IMAGE = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile1.PROFILE_IMAGE;
            authResponse.USERID = tblUser.USERID;
            authResponse.OID = Convert.ToInt32((object) tblUser.ID_ORGANIZATION);
            authResponse.id_org_game_unit = tblProfile1.id_org_game_unit;
            authResponse.UserFunction = tblUser.user_function;
            authResponse.unit = m2ostnextserviceDbContext1.Database.SqlQuery<string>("select unit from tbl_org_game_unit_master where id_org_game_unit={0}", (object) authResponse.id_org_game_unit).FirstOrDefault<string>();
            string sql = "select avatar_type from tbl_org_game_user_avatar where id_user=" + authResponse.IDUSER.ToString() + " and status='A'";
            int num = 0;
            num = Convert.ToInt32(m2ostnextserviceDbContext1.Database.SqlQuery<int>(sql).FirstOrDefault<int>());
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext2 = new m2ostnextserviceDbContext())
            {
              authResponse.is_first_time_login = m2ostnextserviceDbContext2.Database.SqlQuery<int>("select is_first_time_login from tbl_user where ID_USER={0} ", (object) authResponse.IDUSER).FirstOrDefault<int>();
              tbl_profile tblProfile2 = m2ostnextserviceDbContext2.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) authResponse.IDUSER).FirstOrDefault<tbl_profile>();
              if (authResponse.is_first_time_login == 1)
              {
                string str = UserAuthenticationController.RandomString(4);
                string email = tblProfile2.EMAIL;
                tbl_email_verification_key_log verificationKeyLog = m2ostnextserviceDbContext2.Database.SqlQuery<tbl_email_verification_key_log>("select * from tbl_email_verification_key_log where id_user={0} and status='P' ", (object) authResponse.IDUSER).FirstOrDefault<tbl_email_verification_key_log>();
                if (verificationKeyLog == null)
                {
                  m2ostnextserviceDbContext2.Database.ExecuteSqlCommand("insert into tbl_email_verification_key_log (id_user,secret_key,updated_date_time,status) values({0},{1},{2},{3})", (object) authResponse.IDUSER, (object) str, (object) DateTime.Now, (object) "P");
                }
                else
                {
                  string secretKey = verificationKeyLog.secret_key;
                }
              }
            }
          }
          else
          {
            authResponse.AuthStatus = "FAILED";
            authResponse.AuthMessage = "Entered userid and password is wrong.";
          }
        }
      }
      catch (Exception ex)
      {
        authResponse.AuthStatus = "FAILED";
        authResponse.AuthMessage = "Something went wrong.";
      }
      return namespace2.CreateResponse<AuthResponse>(this.Request, HttpStatusCode.OK, authResponse);
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

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[UserAuthenticationController.random.Next(s.Length)])).ToArray<char>());
  }
}
