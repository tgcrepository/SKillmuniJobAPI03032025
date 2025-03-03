// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateProfileController
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
    public class UpdateProfileController : ApiController
  {
    public HttpResponseMessage Post([FromBody] SignupModel obj)
    {
      string str = this.ControllerContext.RouteData.Values["controller"].ToString();
      SignupModel signupModel = new SignupModel();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (obj.ref_code != null)
          {
            if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select ID_USER from tbl_user where ref_id={0} ", (object) obj.ref_code).FirstOrDefault<int>() > 0)
            {
              int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select ref_points from tbl_referral_code_points_config where ref_type={0}", (object) 1).FirstOrDefault<int>();
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_referral_code_user_mapping (id_user,referral_code,status,updated_date_time,referral_points) values({0},{1},{2},{3},{4}) ", (object) obj.ID_USER, (object) obj.ref_code, (object) "A", (object) DateTime.Now, (object) num);
            }
            else
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("Insert into tbl_referral_code_user_mapping (id_user,referral_code,status,updated_date_time) values({0},{1},{2},{3}) ", (object) obj.ID_USER, (object) obj.ref_code, (object) "A", (object) DateTime.Now);
          }
          m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_profile  set FIRSTNAME={0} , LASTNAME={1} , STATE={2} , CITY={3},  GENDER={4} , DATE_OF_BIRTH={5} , COLLEGE={6} , GRADUATIONYEAR={7} , id_degree={8} , id_stream={9} , COUNTRY={11},STUDENT={12},NOTSTUDENT={13},id_foundation={14} ,clg_city = {15},clg_state = {16},clg_country = {17},MOBILE={18} where ID_USER={10}", (object) obj.FIRSTNAME, (object) obj.LASTNAME, (object) obj.State, (object) obj.City, (object) obj.Gender, (object) obj.DOB, (object) obj.College, (object) obj.GraduationYear, (object) obj.id_degree, (object) obj.id_stream, (object) obj.ID_USER, (object) obj.COUNTRY, (object) obj.STUDENT, (object) obj.NOTSTUDENT, (object) obj.id_foundation, (object) obj.clg_city, (object) obj.clg_state, (object) obj.clg_country, (object) obj.MOBILENO);
          if (obj.id_stream == 0)
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update   tbl_profile  set OTHERSTREAM={0} where ID_USER={1}", (object) obj.OTHERSTREAM, (object) obj.ID_USER);
          signupModel.response_status = "SUCCESS";
          signupModel.response_message = "USER SUCCESSFULLY UPDATED";
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
