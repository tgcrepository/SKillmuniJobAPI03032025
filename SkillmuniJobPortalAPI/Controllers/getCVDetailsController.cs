// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCVDetailsController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class getCVDetailsController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      List<tbl_cv_master> tblCvMasterList = new List<tbl_cv_master>();
      CreateResumeDetails createResumeDetails = new CreateResumeDetails();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tblCvMasterList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>("select * from tbl_cv_master where id_user={0} and cv_type={1}", (object) UID, (object) 2).ToList<tbl_cv_master>();
      if (tblCvMasterList.Count == 0)
      {
        createResumeDetails.data_flag = 1;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          createResumeDetails.ProfilePicture = tblProfile.PROFILE_IMAGE;
          createResumeDetails.FirstName = tblProfile.FIRSTNAME;
          createResumeDetails.LastName = tblProfile.LASTNAME;
          createResumeDetails.Phone = tblProfile.MOBILE;
          createResumeDetails.Email = tblProfile.EMAIL;
          createResumeDetails.Country = tblProfile.COUNTRY;
          createResumeDetails.City = tblProfile.CITY;
          createResumeDetails.DOB = Convert.ToString((object) tblProfile.DATE_OF_BIRTH);
          createResumeDetails.College = new List<CVCollegeDetails>()
          {
            new CVCollegeDetails()
            {
              College = tblProfile.COLLEGE,
              Degree = m2ostnextserviceDbContext.Database.SqlQuery<string>("select degree from tbl_degree_master where id_degree={0}", (object) tblProfile.id_degree).FirstOrDefault<string>()
            }
          };
          List<tbl_user_job_preferences_skill> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user_job_preferences_skill>("select * from tbl_user_job_preferences_skill where id_user={0}", (object) UID).ToList<tbl_user_job_preferences_skill>();
          int num = 1;
          foreach (tbl_user_job_preferences_skill preferencesSkill in list)
          {
            createResumeDetails.Skills += preferencesSkill.skill;
            if (num < list.Count<tbl_user_job_preferences_skill>())
              createResumeDetails.Skills += ",";
            ++num;
          }
        }
        return namespace2.CreateResponse<CreateResumeDetails>(this.Request, HttpStatusCode.OK, createResumeDetails);
      }
      tbl_cv_master tblCvMaster1 = new tbl_cv_master();
      createResumeDetails.data_flag = 2;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tbl_profile tblProfile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
        createResumeDetails.ProfilePicture = tblProfile.PROFILE_IMAGE;
        tbl_cv_master tblCvMaster2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_master>("select * from tbl_cv_master where id_user={0} and cv_type={1}", (object) UID, (object) 2).FirstOrDefault<tbl_cv_master>();
        createResumeDetails.personel = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_personel_info>("select * from tbl_cv_personel_info where id_cv={0}", (object) tblCvMaster2.id_cv).FirstOrDefault<tbl_cv_personel_info>();
        createResumeDetails.education = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_education>("select * from tbl_cv_education where id_cv={0}", (object) tblCvMaster2.id_cv).ToList<tbl_cv_education>();
        createResumeDetails.project_list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_project>("select * from tbl_cv_project where id_cv={0}", (object) tblCvMaster2.id_cv).ToList<tbl_cv_project>();
        createResumeDetails.additional_info = m2ostnextserviceDbContext.Database.SqlQuery<tbl_cv_additional_info>("select * from tbl_cv_additional_info where id_cv={0}", (object) tblCvMaster2.id_cv).FirstOrDefault<tbl_cv_additional_info>();
      }
      return namespace2.CreateResponse<CreateResumeDetails>(this.Request, HttpStatusCode.OK, createResumeDetails);
    }
  }
}
