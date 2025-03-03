// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BAuthenticationController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class B2BAuthenticationController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();
    private static Random random = new Random();

    public HttpResponseMessage Post([FromBody] B2BUser user)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
      try
      {
        user.USERID = new Utility().mysqlTrim(user.USERID);
        user.PASSWORD = HttpUtility.UrlDecode(user.PASSWORD);
        tbl_user dbuser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.USERID == user.USERID && t.STATUS == "A")).FirstOrDefault<tbl_user>();
        if (dbuser != null)
        {
          if (dbuser.PASSWORD == user.PASSWORD)
          {
            tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
            {
              (object) dbuser.ID_ORGANIZATION
            });
            if (dbuser.STATUS == "A")
            {
              loginResponseAuth.ResponseCode = "SUCCESS";
              loginResponseAuth.ResponseAction = 0;
              loginResponseAuth.ResponseMessage = "User successfully registered";
              loginResponseAuth.UserID = Convert.ToInt32(dbuser.ID_USER);
              loginResponseAuth.UserName = dbuser.USERID;
              int idOrganization = tblOrganization.ID_ORGANIZATION;
              loginResponseAuth.ROLEID = "1";
              loginResponseAuth.ORGID = idOrganization.ToString();
              loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
              loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
              loginResponseAuth.ORGEMAIL = tblOrganization.DEFAULT_EMAIL;
              loginResponseAuth.log_flag = new ChangePasswordLogic().CheckFirstLogin(loginResponseAuth.UserID);
              tbl_profile tblProfile1 = new tbl_profile();
              using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              {
                loginResponseAuth.is_first_time_login = m2ostnextserviceDbContext.Database.SqlQuery<int>("select is_first_time_login from tbl_user where ID_USER={0} ", (object) dbuser.ID_USER).FirstOrDefault<int>();
                tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) dbuser.ID_USER).FirstOrDefault<tbl_profile>();
                if (loginResponseAuth.is_first_time_login == 1)
                {
                  string OTP = B2BAuthenticationController.RandomString(4);
                  string decryptedString = new AESAlgorithm().getDecryptedString(tblProfile2.EMAIL);
                  tbl_email_verification_key_log verificationKeyLog = m2ostnextserviceDbContext.Database.SqlQuery<tbl_email_verification_key_log>("select * from tbl_email_verification_key_log where id_user={0} and status='P' ", (object) dbuser.ID_USER).FirstOrDefault<tbl_email_verification_key_log>();
                  if (verificationKeyLog == null)
                    m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_email_verification_key_log (id_user,key,updated_date_time,status) values({0},{1},{2},{3})", (object) dbuser.ID_USER, (object) OTP, (object) DateTime.Now, (object) "P");
                  else
                    OTP = verificationKeyLog.secret_key;
                  this.SendOTP(decryptedString, tblProfile2.FIRSTNAME + " " + tblProfile2.LASTNAME, OTP);
                }
              }
              tbl_profile tblProfile3 = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == dbuser.ID_USER)).FirstOrDefault<tbl_profile>();
              if (tblProfile3 != null)
              {
                loginResponseAuth.fullname = tblProfile3.FIRSTNAME + " " + tblProfile3.LASTNAME;
                loginResponseAuth.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + tblProfile3.PROFILE_IMAGE;
              }
              else
              {
                loginResponseAuth.fullname = "NA";
                loginResponseAuth.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + "default.png";
              }
              if (string.IsNullOrEmpty(user.IMEI))
              {
                this.db.tbl_report_login_log.Add(new tbl_report_login_log()
                {
                  id_user = new int?(dbuser.ID_USER),
                  id_organization = dbuser.ID_ORGANIZATION,
                  IMEI = "WEBSITE",
                  LOG_DATETIME = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
              else
              {
                this.db.tbl_report_login_log.Add(new tbl_report_login_log()
                {
                  id_user = new int?(dbuser.ID_USER),
                  id_organization = dbuser.ID_ORGANIZATION,
                  IMEI = user.IMEI,
                  LOG_DATETIME = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
            }
            else
            {
              string str2 = "User account is deactivated. Please contact your administrator.";
              loginResponseAuth.ResponseCode = "FAILURE";
              loginResponseAuth.ResponseAction = 0;
              loginResponseAuth.ResponseMessage = str2;
              loginResponseAuth.UserID = 0;
              loginResponseAuth.UserName = "";
              int num = 0;
              loginResponseAuth.ROLEID = "";
              loginResponseAuth.ORGID = num.ToString();
              loginResponseAuth.LogoPath = "";
              loginResponseAuth.BannerPath = "";
              loginResponseAuth.ORGEMAIL = "";
            }
          }
          else
          {
            string str3 = "User account is deactivated. Please contact your administrator.";
            loginResponseAuth.ResponseCode = "FAILURE";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = str3;
            loginResponseAuth.UserID = 0;
            loginResponseAuth.UserName = "";
            int num = 0;
            loginResponseAuth.ROLEID = "";
            loginResponseAuth.ORGID = num.ToString();
            loginResponseAuth.LogoPath = "";
            loginResponseAuth.BannerPath = "";
            loginResponseAuth.ORGEMAIL = "";
          }
        }
        else
        {
          string str4 = this.db.tbl_user.SqlQuery("select * from tbl_user where USERID='" + user.USERID + "' and PASSWORD='" + user.PASSWORD + "' ").FirstOrDefault<tbl_user>() == null ? "Invalid Username and Password..." : "Device not Registered with M2OST.Please contact Administrator..";
          loginResponseAuth.ResponseCode = "FAILURE";
          loginResponseAuth.ResponseAction = 0;
          loginResponseAuth.ResponseMessage = str4;
          loginResponseAuth.UserID = 0;
          loginResponseAuth.UserName = "";
          int num = 0;
          loginResponseAuth.ROLEID = "";
          loginResponseAuth.ORGID = num.ToString();
          loginResponseAuth.LogoPath = "";
          loginResponseAuth.BannerPath = "";
          loginResponseAuth.ORGEMAIL = "";
        }
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
      }
      return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
    }

    public HttpResponseMessage Get(
      string USERID = "",
      string PASSWORD = "",
      string IMEI = "",
      string OS = "",
      string Network = "",
      string OSVersion = "",
      string Details = "")
    {
      LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
      USERID = new Utility().mysqlTrim(USERID);
      PASSWORD = new AESAlgorithm().getEncryptedString(PASSWORD);
      tbl_user dbuser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.USERID == USERID && t.STATUS == "A" && t.PASSWORD == PASSWORD)).FirstOrDefault<tbl_user>();
      if (dbuser != null)
      {
        tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
        {
          (object) dbuser.ID_ORGANIZATION
        });
        if (dbuser.STATUS == "A")
        {
          loginResponseAuth.ResponseCode = "SUCCESS";
          loginResponseAuth.ResponseAction = 0;
          loginResponseAuth.ResponseMessage = "User successfully authenticated";
          loginResponseAuth.UserID = Convert.ToInt32(dbuser.ID_USER);
          loginResponseAuth.UserName = dbuser.USERID;
          int idOrganization = tblOrganization.ID_ORGANIZATION;
          loginResponseAuth.ROLEID = "1";
          loginResponseAuth.ORGID = idOrganization.ToString();
          loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
          loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
          loginResponseAuth.ORGEMAIL = tblOrganization.DEFAULT_EMAIL;
          loginResponseAuth.log_flag = new ChangePasswordLogic().CheckFirstLogin(loginResponseAuth.UserID);
          tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == dbuser.ID_USER)).FirstOrDefault<tbl_profile>();
          if (tblProfile != null)
          {
            loginResponseAuth.fullname = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
              loginResponseAuth.profile_image = ConfigurationManager.AppSettings["profileimage_base"].ToString() + m2ostnextserviceDbContext.Database.SqlQuery<string>("select PROFILE_IMAGE from tbl_profile where ID_USER={0}", (object) loginResponseAuth.UserID).FirstOrDefault<string>();
          }
          else
            loginResponseAuth.fullname = "NA";
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            loginResponseAuth.total_score = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE(SUM(total_score),0) total FROM tbl_user_level_log WHERE id_user = {0} and is_qualified=1 and status='A';", (object) loginResponseAuth.UserID).FirstOrDefault<int>();
            loginResponseAuth.last_successive_level = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT COALESCE( max(level),0) last_level FROM db_sul_beta.tbl_user_level_log where id_user={0} and is_qualified=1 and status='A';", (object) loginResponseAuth.UserID).FirstOrDefault<int>();
          }
          if (string.IsNullOrEmpty(IMEI))
          {
            this.db.tbl_report_login_log.Add(new tbl_report_login_log()
            {
              id_user = new int?(dbuser.ID_USER),
              id_organization = dbuser.ID_ORGANIZATION,
              IMEI = "WEBSITE",
              LOG_DATETIME = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
          }
          else
          {
            this.db.tbl_report_login_log.Add(new tbl_report_login_log()
            {
              id_user = new int?(dbuser.ID_USER),
              id_organization = dbuser.ID_ORGANIZATION,
              IMEI = IMEI,
              LOG_DATETIME = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
          }
        }
        else
        {
          string str = "User account is deactivated. Please contact your administrator.";
          loginResponseAuth.ResponseCode = "FAILURE";
          loginResponseAuth.ResponseAction = 0;
          loginResponseAuth.ResponseMessage = str;
          loginResponseAuth.UserID = 0;
          loginResponseAuth.UserName = "";
          int num = 0;
          loginResponseAuth.ROLEID = "";
          loginResponseAuth.ORGID = num.ToString();
          loginResponseAuth.LogoPath = "";
          loginResponseAuth.BannerPath = "";
          loginResponseAuth.ORGEMAIL = "";
        }
      }
      else
      {
        string str = "Invalid User Name/Password";
        loginResponseAuth.ResponseCode = "FAILURE";
        loginResponseAuth.ResponseAction = 0;
        loginResponseAuth.ResponseMessage = str;
        loginResponseAuth.UserID = 0;
        loginResponseAuth.UserName = "";
        int num = 0;
        loginResponseAuth.ROLEID = "";
        loginResponseAuth.ORGID = num.ToString();
        loginResponseAuth.LogoPath = "";
        loginResponseAuth.BannerPath = "";
        loginResponseAuth.ORGEMAIL = "";
      }
      return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
    }

    public void SendOTP(string Semail, string Name, string OTP)
    {
      try
      {
        string str = "skillmuni@thegamificationcompany.com";
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

    public static string RandomString(int length) => new string(Enumerable.Repeat<string>("0123456789", length).Select<string, char>((Func<string, char>) (s => s[B2BAuthenticationController.random.Next(s.Length)])).ToArray<char>());
  }
}
