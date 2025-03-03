// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getJobPreferencesDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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

    public class getJobPreferencesDetailsController : ApiController
  {
    public HttpResponseMessage Get(int UID)
    {
      preferencemodel preferencemodel = new preferencemodel();
      try
      {
        using (JobDbContext jobDbContext = new JobDbContext())
        {
          tbl_user_job_preferences userJobPreferences1 = new tbl_user_job_preferences();
          tbl_user_job_preferences userJobPreferences2 = jobDbContext.Database.SqlQuery<tbl_user_job_preferences>("select * from  tbl_user_job_preferences where id_user={0} ", (object) UID).FirstOrDefault<tbl_user_job_preferences>();
          if (userJobPreferences2 != null)
          {
            preferencemodel.experience_months = userJobPreferences2.experience_months;
            preferencemodel.experience_years = userJobPreferences2.experience_years;
          }
          List<tbl_user_job_preferences_skill> list1 = jobDbContext.Database.SqlQuery<tbl_user_job_preferences_skill>("select * from  tbl_user_job_preferences_skill where id_user={0}", (object) UID).ToList<tbl_user_job_preferences_skill>();
          int num1 = 1;
          int count1 = list1.Count;
          foreach (tbl_user_job_preferences_skill preferencesSkill in list1)
          {
            preferencemodel.skill += preferencesSkill.skill;
            if (num1 < count1)
              preferencemodel.skill += ",";
            ++num1;
          }
          List<tbl_user_job_preferences_category> list2 = jobDbContext.Database.SqlQuery<tbl_user_job_preferences_category>("select * from  tbl_user_job_preferences_category where id_user={0} and status = 'A'", (object) UID).ToList<tbl_user_job_preferences_category>();
          int num2 = 1;
          int count2 = list2.Count;
          if (count2 > 0)
          {
            foreach (tbl_user_job_preferences_category preferencesCategory in list2)
            {
              preferencemodel.category += preferencesCategory.id_category.ToString();
              if (num2 < count2)
                preferencemodel.category += ",";
              ++num2;
            }
          }
          List<tbl_user_job_preferences_location> list3 = jobDbContext.Database.SqlQuery<tbl_user_job_preferences_location>("select * from  tbl_user_job_preferences_location where id_user={0}", (object) UID).ToList<tbl_user_job_preferences_location>();
          int num3 = 1;
          int count3 = list3.Count;
          if (count3 > 0)
          {
            foreach (tbl_user_job_preferences_location preferencesLocation in list3)
            {
              preferencemodel.id_location += preferencesLocation.id_location.ToString();
              if (num3 < count3)
                preferencemodel.id_location += ",";
              ++num3;
            }
          }
          List<tbl_user_job_preferences_job_type> list4 = jobDbContext.Database.SqlQuery<tbl_user_job_preferences_job_type>("select * from  tbl_user_job_preferences_job_type where id_user={0}", (object) UID).ToList<tbl_user_job_preferences_job_type>();
          int num4 = 1;
          int count4 = list4.Count;
          foreach (tbl_user_job_preferences_job_type preferencesJobType in list4)
          {
            preferencemodel.job_type += preferencesJobType.job_type;
            if (num4 < count4)
              preferencemodel.job_type += ",";
            ++num4;
          }
          preferencemodel.certificatepath = ConfigurationManager.AppSettings["CertificatePath"].ToString() + jobDbContext.Database.SqlQuery<string>("select certificate_file from  tbl_user_extra_curricular_certificates  where id_user={0} ", (object) UID).FirstOrDefault<string>();
        }
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          if (m2ostnextserviceDbContext.Database.SqlQuery<int>("select ResumeFlag from  tbl_profile  where ID_USER={0} ", (object) UID).FirstOrDefault<int>() == 1)
            preferencemodel.resumepath = ConfigurationManager.AppSettings["ResumePath"].ToString() + m2ostnextserviceDbContext.Database.SqlQuery<string>("select ResumeLocation from  tbl_profile  where ID_USER={0} ", (object) UID).FirstOrDefault<string>();
          preferencemodel.role = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_jobrole_user_mapping>("select * from  tbl_ce_evaluation_jobrole_user_mapping  where id_user={0} ", (object) UID).ToList<tbl_ce_evaluation_jobrole_user_mapping>();
          preferencemodel.industry = m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_jobindustry_user_mapping>("select * from  tbl_ce_evaluation_jobindustry_user_mapping  where id_user={0} and status='A'", (object) UID).ToList<tbl_ce_evaluation_jobindustry_user_mapping>();
          int num5 = 1;
          int count5 = preferencemodel.role.Count;
          if (count5 > 0)
          {
            foreach (tbl_ce_evaluation_jobrole_user_mapping jobroleUserMapping in preferencemodel.role)
            {
              preferencemodel.role_str += jobroleUserMapping.id_ce_evaluation_jobrole.ToString();
              if (num5 < count5)
                preferencemodel.role_str += ",";
              ++num5;
            }
          }
          int num6 = 1;
          int count6 = preferencemodel.industry.Count;
          if (count6 > 0)
          {
            foreach (tbl_ce_evaluation_jobindustry_user_mapping jobindustryUserMapping in preferencemodel.industry)
            {
              preferencemodel.industry_str += jobindustryUserMapping.id_ce_evaluation_jobindustry.ToString();
              if (num6 < count6)
                preferencemodel.industry_str += ",";
              ++num6;
            }
          }
          tbl_cv_master tblCvMaster1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", (object) UID, (object) 1).FirstOrDefault<tbl_cv_master>();
          if (tblCvMaster1 != null)
          {
            preferencemodel.isVideoCvPresent = 1;
            tbl_video_cv tblVideoCv = m2ostnextserviceDbContext.Database.SqlQuery<tbl_video_cv>(" select * from tbl_video_cv where id_cv ={0}", (object) tblCvMaster1.id_cv).FirstOrDefault<tbl_video_cv>();
            preferencemodel.VideoCVStatus = tblVideoCv.status;
            preferencemodel.VideoCVLink = ConfigurationManager.AppSettings["vidcv"].ToString() + UID.ToString() + "." + tblVideoCv.extn;
          }
          else
            preferencemodel.isVideoCvPresent = 0;
          tbl_cv_master tblCvMaster2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>(" select * from tbl_cv_master where id_user ={0} and cv_type={1}", (object) UID, (object) 2).FirstOrDefault<tbl_cv_master>();
          if (tblCvMaster2 != null)
          {
            preferencemodel.isClassicCVPresent = 1;
            preferencemodel.ClassicCvLink = ConfigurationManager.AppSettings["CVControl"].ToString() + tblCvMaster2.id_cv.ToString();
          }
          else
            preferencemodel.isClassicCVPresent = 0;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<preferencemodel>(this.Request, HttpStatusCode.OK, preferencemodel);
    }
  }
}
