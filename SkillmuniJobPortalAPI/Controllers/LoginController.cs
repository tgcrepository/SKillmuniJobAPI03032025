// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.LoginController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class LoginController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] Login login)
    {
      LoginResponse loginResponse = new LoginResponse();
      int userID = new RegistrationModel().NewCheckUserExist(login.UserName, login.Role);
      if (userID > 0)
      {
        if (string.Equals(login.DeviceType, "MOBILE", StringComparison.OrdinalIgnoreCase))
        {
          string deviceId = login.DeviceID;
          if (!new RegistrationModel().CheckDeviceExist(deviceId, login.Role))
          {
            int deviceTypeId = new RegistrationModel().GetDeviceTypeID(login.DeviceType);
            new RegistrationModel().UpdateUserDevice(userID, deviceTypeId, deviceId);
          }
          tbl_organization tblOrganization = this.db.tbl_organization.Find(new object[1]
          {
            (object) login.OrganizationID
          });
          loginResponse.ResponseCode = "SUCCESS";
          loginResponse.ResponseAction = 5;
          loginResponse.ResponseMessage = "User successfully registered";
          loginResponse.UserID = userID;
          loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
          loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
          loginResponse.ORGEMAIL = tblOrganization.DEFAULT_EMAIL;
          this.db.tbl_report_login_log.Add(new tbl_report_login_log()
          {
            id_user = new int?(userID),
            id_organization = new int?(login.OrganizationID),
            IMEI = login.DeviceID,
            LOG_DATETIME = new DateTime?(DateTime.Now)
          });
          this.db.SaveChanges();
          return namespace2.CreateResponse<LoginResponse>(this.Request, HttpStatusCode.OK, loginResponse);
        }
      }
      else
      {
        if (false)
        {
          loginResponse.ResponseCode = "FAILURE";
          loginResponse.ResponseAction = 1;
          loginResponse.ResponseMessage = "Your device is already registered with a different profile. Please use the other profile to login.";
          loginResponse.UserID = 0;
          loginResponse.LogoPath = "";
          loginResponse.BannerPath = "";
          return namespace2.CreateResponse<LoginResponse>(this.Request, HttpStatusCode.OK, loginResponse);
        }
        string password = new PasswordGeneration().GetPassword();
        login.Password = password.ToMD5Hash();
        Authcode authcode = new Authcode();
        Authcode activeAuthcode = new RegistrationModel().GetActiveAuthcode();
        if (new RegistrationModel().CheckUserExist(login.UserName, 0))
        {
          loginResponse.ResponseCode = "FAILURE";
          loginResponse.ResponseAction = 1;
          loginResponse.ResponseMessage = "User already present with this UserId....";
          loginResponse.UserID = 0;
          loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
          loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
          return namespace2.CreateResponse<LoginResponse>(this.Request, HttpStatusCode.OK, loginResponse);
        }
        if (activeAuthcode != null)
        {
          Registration user = new Registration();
          user.OrganizationID = login.OrganizationID;
          user.Role = 11;
          user.FirstName = "m2ost ";
          user.LastName = " user";
          user.UserName = login.UserName;
          user.Password = login.Password;
          user.DeviceType = login.DeviceType;
          user.DeviceID = login.DeviceID;
          user.FBSocialID = login.FBSocialID;
          user.GPSocialID = login.GPSocialID;
          user.Provider = login.Provider;
          user.Email = login.UserName;
          user.Age = "0";
          user.Mobile = "";
          int userResult = new RegistrationModel().CreateUser(user, activeAuthcode, "A");
          if (userResult > 0)
          {
            this.db.tbl_role_user_mapping.Add(new tbl_role_user_mapping()
            {
              id_csst_role = new int?(11),
              id_user = new int?(userResult),
              status = "A",
              updated_date_time = new DateTime?(DateTime.Now)
            });
            this.db.SaveChanges();
            DbSet<tbl_assignment_role_program> assignmentRoleProgram1 = this.db.tbl_assignment_role_program;
            Expression<Func<tbl_assignment_role_program, bool>> predicate = (Expression<Func<tbl_assignment_role_program, bool>>) (t => t.id_role == (int?) 11 && t.id_organization == (int?) login.OrganizationID);
            foreach (tbl_assignment_role_program assignmentRoleProgram2 in assignmentRoleProgram1.Where<tbl_assignment_role_program>(predicate).ToList<tbl_assignment_role_program>())
            {
              tbl_assignment_role_program rpItem = assignmentRoleProgram2;
              if (this.db.tbl_content_program_mapping.Where<tbl_content_program_mapping>((Expression<Func<tbl_content_program_mapping, bool>>) (t => t.id_category == rpItem.id_program && t.id_organization == (int?) login.OrganizationID && t.id_user == (int?) userResult)).FirstOrDefault<tbl_content_program_mapping>() == null)
              {
                this.db.tbl_content_program_mapping.Add(new tbl_content_program_mapping()
                {
                  map_type = new int?(1),
                  id_role = rpItem.id_role,
                  id_user = new int?(userResult),
                  id_organization = new int?(login.OrganizationID),
                  id_category = rpItem.id_program,
                  status = "A",
                  option_type = new int?(0),
                  start_date = rpItem.start_datetime,
                  expiry_date = rpItem.end_datetime,
                  id_category_tile = rpItem.id_category_tile,
                  id_category_heading = rpItem.id_category_heading,
                  id_assessment_sheet = new int?(0),
                  updated_date_time = new DateTime?(DateTime.Now)
                });
                this.db.SaveChanges();
              }
            }
            activeAuthcode.Status = "U";
            if (true)
            {
              if (new RegistrationModel().CreateProfile(userResult, user) == 1)
              {
                int deviceTypeId = new RegistrationModel().GetDeviceTypeID(user.DeviceType);
                if (deviceTypeId != 0)
                {
                  if (new RegistrationModel().UpdateUserDevice(userResult, deviceTypeId, user.DeviceID) != 0)
                  {
                    loginResponse.ResponseCode = "SUCCESS";
                    loginResponse.ResponseAction = 1;
                    loginResponse.ResponseMessage = "User successfully registered";
                    loginResponse.UserID = userResult;
                    loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
                    loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
                    this.db.tbl_report_login_log.Add(new tbl_report_login_log()
                    {
                      id_user = new int?(userResult),
                      id_organization = new int?(login.OrganizationID),
                      IMEI = login.DeviceID,
                      LOG_DATETIME = new DateTime?(DateTime.Now)
                    });
                    this.db.SaveChanges();
                  }
                  else
                  {
                    loginResponse.ResponseCode = "SUCCESS";
                    loginResponse.ResponseAction = 2;
                    loginResponse.ResponseMessage = "User successfully registered , but could not log device details.";
                    loginResponse.UserID = userResult;
                    loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
                    loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
                  }
                }
                else
                {
                  loginResponse.ResponseCode = "SUCCESS";
                  loginResponse.ResponseAction = 2;
                  loginResponse.ResponseMessage = "User successfully registered, but could not capture device deails. Please update device details.";
                  loginResponse.UserID = userResult;
                  loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
                  loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
                }
              }
              else
              {
                loginResponse.ResponseCode = "SUCCESS";
                loginResponse.ResponseAction = 2;
                loginResponse.ResponseMessage = "User successfully registered, but profile could not be updated. Please update profile.";
                loginResponse.UserID = userResult;
                loginResponse.LogoPath = new RegistrationModel().getOrgLogo(login.OrganizationID);
                loginResponse.BannerPath = new RegistrationModel().getOrgBanner(login.OrganizationID);
              }
            }
            else if (new RegistrationModel().DeleteUserRollback(userResult) == 1)
            {
              loginResponse.ResponseCode = "FAILURE";
              loginResponse.ResponseAction = 0;
              loginResponse.ResponseMessage = "Registration Failed. Could not generate auth code. Please try again later.";
              loginResponse.UserID = 0;
            }
            else
            {
              loginResponse.ResponseCode = "FAILURE";
              loginResponse.ResponseAction = 0;
              loginResponse.ResponseMessage = "Registration Failed. Could not delete user while roll back. Please contact skillmuni support.";
              loginResponse.UserID = 0;
            }
          }
          else
          {
            loginResponse.ResponseCode = "FAILURE";
            loginResponse.ResponseAction = 0;
            loginResponse.ResponseMessage = "Registration Failed. Could not create user. Please try again later.";
            loginResponse.UserID = 0;
          }
        }
        else
        {
          loginResponse.ResponseCode = "FAILURE";
          loginResponse.ResponseAction = 0;
          loginResponse.ResponseMessage = "Registration Failed. Could not generate auth code. Please try again later.";
          loginResponse.UserID = 0;
        }
      }
      return namespace2.CreateResponse<LoginResponse>(this.Request, HttpStatusCode.OK, loginResponse);
    }
  }
}
