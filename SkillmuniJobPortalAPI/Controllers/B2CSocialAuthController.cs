// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2CSocialAuthController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class B2CSocialAuthController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] B2CSocial user)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      LoginResponseAuth loginResponseAuth = new LoginResponseAuth();
      int soid = Convert.ToInt32(ConfigurationManager.AppSettings["SOCIALORG"].ToString());
      int num1 = 0;
      try
      {
        tbl_social_platform_active_directory sc = this.db.Database.SqlQuery<tbl_social_platform_active_directory>("SELECT * FROM tbl_social_platform_active_directory where social_code='" + user.SCTYPE + "' and social_platform_code='" + user.SCID + "' and status='A' limit 1").FirstOrDefault<tbl_social_platform_active_directory>();
        if (sc != null)
        {
          tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == sc.id_user)).FirstOrDefault<tbl_user>();
          tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
          {
            (object) tblUser.ID_ORGANIZATION
          });
          if (tblUser.STATUS == "A")
          {
            loginResponseAuth.ResponseCode = "SUCCESS";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = "User successfully registered";
            loginResponseAuth.UserID = Convert.ToInt32(tblUser.ID_USER);
            loginResponseAuth.UserName = tblUser.USERID;
            int idOrganization = tblOrganization.ID_ORGANIZATION;
            loginResponseAuth.ROLEID = "1";
            loginResponseAuth.ORGID = idOrganization.ToString();
            loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(idOrganization);
            loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(idOrganization);
            loginResponseAuth.ORGEMAIL = tblOrganization.DEFAULT_EMAIL;
            loginResponseAuth.log_flag = new ChangePasswordLogic().CheckFirstLogin(loginResponseAuth.UserID);
            tbl_profile tblProfile = new tbl_profile();
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            {
              loginResponseAuth.ref_id = m2ostnextserviceDbContext.Database.SqlQuery<string>("select ref_id from tbl_user where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<string>();
              tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) tblUser.ID_USER).FirstOrDefault<tbl_profile>();
            }
            if (tblProfile != null)
            {
              loginResponseAuth.fullname = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
              loginResponseAuth.profile_data = 1;
              loginResponseAuth.UserEmail = tblProfile.EMAIL;
              loginResponseAuth.state = tblProfile.STATE;
              loginResponseAuth.city = tblProfile.CITY;
              loginResponseAuth.college = tblProfile.COLLEGE;
              int num2 = this.db.Database.SqlQuery<int>("select id_college from tbl_college_list where college_name={0}", (object) tblProfile.COLLEGE).FirstOrDefault<int>();
              if (num2 > 0)
                loginResponseAuth.id_college = num2;
              loginResponseAuth.college_city = tblProfile.clg_city;
              loginResponseAuth.college_state = tblProfile.clg_state;
            }
            else
            {
              loginResponseAuth.fullname = "NA";
              loginResponseAuth.profile_data = 0;
            }
            if (string.IsNullOrEmpty(user.IMEI))
            {
              this.db.tbl_report_login_log.Add(new tbl_report_login_log()
              {
                id_user = new int?(tblUser.ID_USER),
                id_organization = tblUser.ID_ORGANIZATION,
                IMEI = "WEBSITE",
                LOG_DATETIME = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
            }
            else
            {
              this.db.tbl_report_login_log.Add(new tbl_report_login_log()
              {
                id_user = new int?(tblUser.ID_USER),
                id_organization = tblUser.ID_ORGANIZATION,
                IMEI = user.IMEI,
                LOG_DATETIME = new DateTime?(DateTime.Now)
              });
              this.db.SaveChanges();
            }
            tbl_user_log_master tblUserLogMaster = new tbl_user_log_master();
            if (this.db.Database.SqlQuery<tbl_user_log_master>("select * from tbl_user_log_master where id_user={0}", (object) loginResponseAuth.UserID).FirstOrDefault<tbl_user_log_master>() == null)
              this.db.Database.ExecuteSqlCommand("insert into tbl_user_log_master (id_user,is_registered,academic_tiles,study_abroad,job,entrepreneurship,updated_date_time) values ({0},{1},{2},{3},{4},{5},{6})", (object) loginResponseAuth.UserID, (object) "NO", (object) 0, (object) 0, (object) 0, (object) 0, (object) DateTime.Now);
          }
          else
          {
            string str = "User account is deactivated. Please contact your administrator.";
            loginResponseAuth.ResponseCode = "FAILURE";
            loginResponseAuth.ResponseAction = 0;
            loginResponseAuth.ResponseMessage = str;
            loginResponseAuth.UserID = 0;
            loginResponseAuth.UserName = "";
            int num3 = 0;
            loginResponseAuth.ROLEID = "";
            loginResponseAuth.ORGID = num3.ToString();
            loginResponseAuth.LogoPath = "";
            loginResponseAuth.BannerPath = "";
            loginResponseAuth.ORGEMAIL = "";
          }
        }
        else
        {
          loginResponseAuth.profile_data = 0;
          if (user.SCSTATUS == "T")
          {
            string str = new Utility().uniqueIDS(8);
            tbl_csst_role tblCsstRole = this.db.tbl_csst_role.Where<tbl_csst_role>((Expression<Func<tbl_csst_role, bool>>) (t => t.id_organization == (int?) soid)).FirstOrDefault<tbl_csst_role>();
            tbl_user entity = new tbl_user()
            {
              ID_ORGANIZATION = new int?(soid),
              ID_ROLE = tblCsstRole.id_csst_role,
              EMPLOYEEID = str,
              EXPIRY_DATE = new DateTime?(DateTime.Now.AddMonths(12)),
              FBSOCIALID = "",
              GPSOCIALID = "",
              ID_CODE = 1,
              is_reporting = new int?(0),
              PASSWORD = ("PLS" + str).ToMD5Hash(),
              reporting_manager = new int?(0),
              STATUS = "A",
              USERID = str,
              UPDATEDTIME = DateTime.Now
            };
            entity.reporting_manager = new int?(Convert.ToInt32(ConfigurationManager.AppSettings["id_reporting_manager"].ToString()));
            this.db.tbl_user.Add(entity);
            this.db.SaveChanges();
            loginResponseAuth.ref_id = new RegistrationModel().RandomString(5);
            using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            {
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Update tbl_user set UPDATEDTIME={0} where ID_USER={1}", (object) DateTime.Now, (object) entity.ID_USER);
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user set ref_id={0} where ID_USER={1}", (object) loginResponseAuth.ref_id, (object) entity.ID_USER);
            }
            int idUser = entity.ID_USER;
            if (idUser > 0)
            {
              if (string.IsNullOrEmpty(user.IMEI))
              {
                this.db.tbl_report_login_log.Add(new tbl_report_login_log()
                {
                  id_user = new int?(idUser),
                  id_organization = new int?(soid),
                  IMEI = "SOCIALWEBSITE",
                  LOG_DATETIME = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
              else
              {
                this.db.tbl_report_login_log.Add(new tbl_report_login_log()
                {
                  id_user = new int?(idUser),
                  id_organization = new int?(soid),
                  IMEI = user.IMEI,
                  LOG_DATETIME = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
              tbl_social_platform_master socialPlatformMaster = this.db.Database.SqlQuery<tbl_social_platform_master>("select * from tbl_social_platform_master where social_platform_code={0}", (object) user.SCTYPE).FirstOrDefault<tbl_social_platform_master>();
              if (socialPlatformMaster != null)
              {
                tbl_social_platform_active_directory platformActiveDirectory = new tbl_social_platform_active_directory();
                platformActiveDirectory.id_organization = soid;
                platformActiveDirectory.id_user = idUser;
                platformActiveDirectory.social_code = socialPlatformMaster.social_platform_code;
                platformActiveDirectory.social_platform_code = user.SCID;
                platformActiveDirectory.id_social_platform_master = socialPlatformMaster.id_social_platform_master;
                platformActiveDirectory.status = "A";
                platformActiveDirectory.updated_date_time = DateTime.Now;
                this.db.Database.ExecuteSqlCommand("insert into tbl_social_platform_active_directory(id_organization,id_social_platform_master,social_platform_code,id_user,social_code,status,updated_date_time) values({0},{1},{2},{3},{4},{5},{6})", (object) platformActiveDirectory.id_organization, (object) platformActiveDirectory.id_social_platform_master, (object) platformActiveDirectory.social_platform_code, (object) platformActiveDirectory.id_user, (object) platformActiveDirectory.social_code, (object) platformActiveDirectory.status, (object) platformActiveDirectory.updated_date_time);
              }
              loginResponseAuth.ResponseCode = "SUCCESS";
              loginResponseAuth.ResponseAction = 0;
              loginResponseAuth.ResponseMessage = "User successfully registered";
              loginResponseAuth.UserID = Convert.ToInt32(entity.ID_USER);
              loginResponseAuth.UserName = entity.USERID;
              loginResponseAuth.ROLEID = "1";
              loginResponseAuth.ORGID = soid.ToString();
              loginResponseAuth.LogoPath = new RegistrationModel().getOrgLogo(soid);
              loginResponseAuth.BannerPath = new RegistrationModel().getOrgBanner(soid);
              loginResponseAuth.ORGEMAIL = "";
              loginResponseAuth.log_flag = 1;
              loginResponseAuth.profile_data = 0;
            }
            int num4 = soid;
            num1 = entity.ID_USER;
            List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
            Database database = this.db.Database;
            object[] objArray = new object[2]
            {
              (object) soid,
              (object) "A"
            };
            foreach (tbl_brief_master tblBriefMaster in database.SqlQuery<tbl_brief_master>("select * from tbl_brief_master where id_organization={0} and status={1}", objArray).ToList<tbl_brief_master>())
            {
              if (this.db.Database.SqlQuery<tbl_brief_status>("select * from tbl_brief_status where status_code={0} and status={1} and id_brief_master={2}", (object) 5, (object) "A", (object) tblBriefMaster.id_brief_master).Any<tbl_brief_status>())
              {
                this.db.tbl_brief_user_assignment.Add(new tbl_brief_user_assignment()
                {
                  id_brief_master = new int?(tblBriefMaster.id_brief_master),
                  scheduled_datetime = new DateTime?(),
                  id_user = new int?(num1),
                  scheduled_status = "NA",
                  published_datetime = new DateTime?(DateTime.Now),
                  published_status = "S",
                  status = "A",
                  updated_date_time = new DateTime?(DateTime.Now),
                  assignment_datetime = new DateTime?(DateTime.Now),
                  assignment_status = "P"
                });
                this.db.tbl_brief_read_status.Add(new tbl_brief_read_status()
                {
                  id_brief_master = new int?(tblBriefMaster.id_brief_master),
                  id_user = new int?(num1),
                  read_status = new int?(0),
                  action_status = new int?(0),
                  id_organization = new int?(soid),
                  status = "A",
                  updated_date_time = new DateTime?(DateTime.Now)
                });
              }
            }
            this.db.SaveChanges();
          }
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_user_log_master (id_user,is_registered,academic_tiles,study_abroad,job,entrepreneurship,updated_date_time) values ({0},{1},{2},{3},{4},{5},{6})", (object) num1, (object) "NO", (object) 0, (object) 0, (object) 0, (object) 0, (object) DateTime.Now);
        }
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<Exception>(this.Request, HttpStatusCode.OK, ex);
      }
      return namespace2.CreateResponse<LoginResponseAuth>(this.Request, HttpStatusCode.OK, loginResponseAuth);
    }
  }
}
