// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateJobpreferencesController
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

    public class UpdateJobpreferencesController : ApiController
  {
    public HttpResponseMessage Post([FromBody] preferencemodel obj)
    {
      string str1 = this.ControllerContext.RouteData.Values["controller"].ToString();
      string str2;
      try
      {
        using (JobDbContext jobDbContext = new JobDbContext())
        {
          tbl_user_job_preferences userJobPreferences = new tbl_user_job_preferences();
          if (jobDbContext.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", (object) obj.id_user).FirstOrDefault<tbl_user_job_preferences>() == null)
          {
            jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences (experience_years,experience_months,status,updated_date_time,id_user) values({0},{1},{2},{3},{4})", (object) obj.experience_years, (object) obj.experience_months, (object) "A", (object) DateTime.Now, (object) obj.id_user);
            string[] strArray1 = obj.skill.Split(',');
            string[] strArray2 = obj.category.Split(',');
            string[] strArray3 = obj.id_location.Split(',');
            string[] strArray4 = obj.job_type.Split(',');
            string[] strArray5 = obj.industry_str.Split(',');
            string[] strArray6 = obj.role_str.Split(',');
            foreach (string str3 in strArray1)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_skill (id_user,skill,status,updated_date_time) values({0},{1},{2},{3})", (object) obj.id_user, (object) str3, (object) "A", (object) DateTime.Now);
            foreach (string str4 in strArray2)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_category (id_category,id_user,status,updated_date_time) values({0},{1},{2},{3})", (object) str4, (object) obj.id_user, (object) "A", (object) DateTime.Now);
            foreach (string str5 in strArray3)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_location (id_location,id_user,status,updated_date_time) values({0},{1},{2},{3})", (object) str5, (object) obj.id_user, (object) "A", (object) DateTime.Now);
            foreach (string str6 in strArray4)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_job_type (id_user,job_type,status,updated_date_time) values({0},{1},{2},{3})", (object) obj.id_user, (object) str6, (object) "A", (object) DateTime.Now);
            foreach (string str7 in strArray5)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_ce_evaluation_jobindustry_user_mapping (id_ce_evaluation_jobindustry,id_user,date_time_stamp,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) str7, (object) obj.id_user, (object) DateTime.Now, (object) "A", (object) DateTime.Now);
            foreach (string str8 in strArray6)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_ce_evaluation_jobrole_user_mapping (id_ce_evaluation_jobrole,id_user,dated_time_stamp,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) str8, (object) obj.id_user, (object) DateTime.Now, (object) "A", (object) DateTime.Now);
          }
          else
          {
            jobDbContext.Database.ExecuteSqlCommand("update  tbl_user_job_preferences set experience_years={0},experience_months={1},status={2},updated_date_time={3} where id_user={4}", (object) obj.experience_years, (object) obj.experience_months, (object) "A", (object) DateTime.Now, (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_skill where id_user={0}", (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_category where id_user={0}", (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_location where id_user={0}", (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_user_job_preferences_job_type where id_user={0}", (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_ce_evaluation_jobindustry_user_mapping where id_user={0}", (object) obj.id_user);
            jobDbContext.Database.ExecuteSqlCommand("delete from  tbl_ce_evaluation_jobrole_user_mapping where id_user={0}", (object) obj.id_user);
            string[] strArray7 = obj.skill.Split(',');
            string[] strArray8 = obj.category.Split(',');
            string[] strArray9 = obj.id_location.Split(',');
            string[] strArray10 = obj.job_type.Split(',');
            string[] strArray11 = obj.industry_str.Split(',');
            string[] strArray12 = obj.role_str.Split(',');
            foreach (string str9 in strArray7)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_skill (id_user,skill,status,updated_date_time) values({0},{1},{2},{3})", (object) obj.id_user, (object) str9, (object) "A", (object) DateTime.Now);
            foreach (string str10 in strArray8)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_category (id_category,id_user,status,updated_date_time) values({0},{1},{2},{3})", (object) str10, (object) obj.id_user, (object) "A", (object) DateTime.Now);
            foreach (string str11 in strArray9)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_location (id_location,id_user,status,updated_date_time) values({0},{1},{2},{3})", (object) str11, (object) obj.id_user, (object) "A", (object) DateTime.Now);
            foreach (string str12 in strArray10)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_user_job_preferences_job_type (id_user,job_type,status,updated_date_time) values({0},{1},{2},{3})", (object) obj.id_user, (object) str12, (object) "A", (object) DateTime.Now);
            foreach (string str13 in strArray11)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_ce_evaluation_jobindustry_user_mapping (id_ce_evaluation_jobindustry,id_user,date_time_stamp,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) str13, (object) obj.id_user, (object) DateTime.Now, (object) "A", (object) DateTime.Now);
            foreach (string str14 in strArray12)
              jobDbContext.Database.ExecuteSqlCommand("insert into tbl_ce_evaluation_jobrole_user_mapping (id_ce_evaluation_jobrole,id_user,dated_time_stamp,status,updated_date_time) values({0},{1},{2},{3},{4})", (object) str14, (object) obj.id_user, (object) DateTime.Now, (object) "A", (object) DateTime.Now);
          }
        }
        str2 = "SUCCESS";
      }
      catch (Exception ex)
      {
        new Utility().eventLog(str1 + " : " + ex.Message);
        new Utility().eventLog("Inner Exeption : " + ex.InnerException.ToString());
        new Utility().eventLog("Additional Details : " + ex.Message);
        str2 = "FAILURE";
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, str2);
    }
  }
}
