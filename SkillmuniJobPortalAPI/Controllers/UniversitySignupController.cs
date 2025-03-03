// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UniversitySignupController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class UniversitySignupController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] SignupModel obj)
    {
      string str = this.ControllerContext.RouteData.Values["controller"].ToString();
      SignupModel signupModel = new SignupModel();
      try
      {
        using (db_m2ostEntities dbM2ostEntities = new db_m2ostEntities())
        {
          if (dbM2ostEntities.Database.SqlQuery<int>("SELECT id_college FROM tbl_college_list where college_name={0}", (object) obj.College).FirstOrDefault<int>() == 0)
            dbM2ostEntities.Database.ExecuteSqlCommand("insert into tbl_college_list(college_name,status,id_user,updated_datetime) values({0},{1},{2},{3})", (object) obj.College, (object) "A", (object) obj.ID_USER, (object) DateTime.Now);
          dbM2ostEntities.Database.ExecuteSqlCommand("insert into  tbl_profile  (FIRSTNAME,LASTNAME,MOBILE,EMAIL,STATE,CITY,GENDER,DATE_OF_BIRTH,COLLEGE,GRADUATIONYEAR,ID_USER,PROFILE_IMAGE,id_degree,id_stream,COUNTRY,STUDENT,NOTSTUDENT,id_foundation) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17})", (object) obj.FIRSTNAME, (object) obj.LASTNAME, (object) obj.MOBILENO, (object) obj.MAILID, (object) obj.State, (object) obj.City, (object) obj.Gender, (object) obj.DOB, (object) obj.College, (object) obj.GraduationYear, (object) obj.ID_USER, (object) obj.PROFILEIMAGE, (object) obj.id_degree, (object) obj.id_stream, (object) obj.COUNTRY, (object) obj.STUDENT, (object) obj.NOTSTUDENT, (object) obj.id_foundation);
          if (obj.id_stream == 0)
            dbM2ostEntities.Database.ExecuteSqlCommand("update   tbl_profile  set OTHERSTREAM={0} where ID_USER={1}", (object) obj.OTHERSTREAM, (object) obj.ID_USER);
          signupModel.response_status = "SUCCESS";
          signupModel.response_message = "USER SUCCESSFULLY REGISTERED";
          tbl_user_log_master tblUserLogMaster = new tbl_user_log_master();
          if (dbM2ostEntities.Database.SqlQuery<tbl_user_log_master>("select * from tbl_user_log_master where id_user={0}", (object) obj.ID_USER).FirstOrDefault<tbl_user_log_master>() == null)
            dbM2ostEntities.Database.ExecuteSqlCommand("insert into tbl_user_log_master (id_user,is_registered,academic_tiles,study_abroad,job,entrepreneurship,updated_date_time) values ({0},{1},{2},{3},{4},{5},{6})", (object) obj.ID_USER, (object) "YES", (object) 0, (object) 0, (object) 0, (object) 0, (object) DateTime.Now);
          else
            dbM2ostEntities.Database.ExecuteSqlCommand("update  tbl_user_log_master set is_registered={0} where id_user={1}", (object) "YES", (object) obj.ID_USER);
          tbl_profile tblProfile = dbM2ostEntities.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) obj.ID_USER).FirstOrDefault<tbl_profile>();
          if (tblProfile != null)
          {
            signupModel.FIRSTNAME = tblProfile.FIRSTNAME;
            signupModel.MAILID = tblProfile.EMAIL;
            signupModel.State = tblProfile.STATE;
            signupModel.City = tblProfile.CITY;
            signupModel.College = tblProfile.COLLEGE;
            signupModel.clg_city = tblProfile.clg_city;
            signupModel.clg_country = tblProfile.clg_state;
          }
        }
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
      }
      return namespace2.CreateResponse<SignupModel>(this.Request, HttpStatusCode.OK, signupModel);
    }
  }
}
