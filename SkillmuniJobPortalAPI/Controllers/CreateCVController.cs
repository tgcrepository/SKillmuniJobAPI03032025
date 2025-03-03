// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CreateCVController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class CreateCVController : ApiController
  {
    public HttpResponseMessage Post([FromBody] CVBuilderCreation CVMaster)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      CVBuilderResponse cvBuilderResponse = new CVBuilderResponse();
      try
      {
        if (CVMaster.data_flag == 1)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            CVMaster.id_cv = m2ostnextserviceDbContext.Database.SqlQuery<int>(" insert into  tbl_cv_master (id_user,oid,created_date,modified_date,status,cv_type) values({0},{1},{2},{3},{4},{5});select max(id_cv) from tbl_cv_master", (object) CVMaster.UID, (object) CVMaster.OID, (object) DateTime.Now, (object) DateTime.Now, (object) "A", (object) 2).FirstOrDefault<int>();
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand(" insert into  tbl_cv_personel_info (id_cv,id_user,first_name,last_name,mobile,email,country,city,street,day,month,year,about) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) CVMaster.personel.first_name, (object) CVMaster.personel.last_name, (object) CVMaster.personel.mobile, (object) CVMaster.personel.email, (object) CVMaster.personel.country, (object) CVMaster.personel.city, (object) CVMaster.personel.street, (object) CVMaster.personel.day, (object) CVMaster.personel.month, (object) CVMaster.personel.year, (object) CVMaster.personel.about);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_additional_info (id_cv,id_user,skills,languages,intrests,linkedin,facebook,twitter,blog,others,refrences,awards) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) CVMaster.additional_info.skills, (object) CVMaster.additional_info.languages, (object) CVMaster.additional_info.intrests, (object) CVMaster.additional_info.linkedin, (object) CVMaster.additional_info.facebook, (object) CVMaster.additional_info.twitter, (object) CVMaster.additional_info.blog, (object) CVMaster.additional_info.others, (object) CVMaster.additional_info.refrences, (object) CVMaster.additional_info.awards);
            foreach (tbl_cv_education tblCvEducation in CVMaster.education)
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_education (id_cv,id_user,college,degree,start_date,end_date,summary) values({0},{1},{2},{3},{4},{5},{6})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) tblCvEducation.college, (object) tblCvEducation.degree, (object) tblCvEducation.start_date, (object) tblCvEducation.end_date, (object) tblCvEducation.summary);
            foreach (tbl_cv_project project in CVMaster.project_list)
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_project (id_cv,id_user,college,project_title,start_date,end_date,summary) values({0},{1},{2},{3},{4},{5},{6})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) project.college, (object) project.project_title, (object) project.start_date, (object) project.end_date, (object) project.summary);
          }
        }
        else if (CVMaster.data_flag == 2)
        {
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            CVMaster.id_cv = CVMaster.personel.id_cv;
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update tbl_cv_master set modified_date={0} where id_cv={1}", (object) DateTime.Now, (object) CVMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from tbl_cv_personel_info where id_cv={0}", (object) CVMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into  tbl_cv_personel_info (id_cv,id_user,first_name,last_name,mobile,email,country,city,street,day,month,year,about) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) CVMaster.personel.first_name, (object) CVMaster.personel.last_name, (object) CVMaster.personel.mobile, (object) CVMaster.personel.email, (object) CVMaster.personel.country, (object) CVMaster.personel.city, (object) CVMaster.personel.street, (object) CVMaster.personel.day, (object) CVMaster.personel.month, (object) CVMaster.personel.year, (object) CVMaster.personel.about);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from tbl_cv_additional_info where id_cv={0}", (object) CVMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_additional_info (id_cv,id_user,skills,languages,intrests,linkedin,facebook,twitter,blog,others,refrences,awards) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) CVMaster.additional_info.skills, (object) CVMaster.additional_info.languages, (object) CVMaster.additional_info.intrests, (object) CVMaster.additional_info.linkedin, (object) CVMaster.additional_info.facebook, (object) CVMaster.additional_info.twitter, (object) CVMaster.additional_info.blog, (object) CVMaster.additional_info.others, (object) CVMaster.additional_info.refrences, (object) CVMaster.additional_info.awards);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from tbl_cv_education where id_cv={0}", (object) CVMaster.id_cv);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("delete from tbl_cv_project where id_cv={0}", (object) CVMaster.id_cv);
            foreach (tbl_cv_education tblCvEducation in CVMaster.education)
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_education (id_cv,id_user,college,degree,start_date,end_date,summary) values({0},{1},{2},{3},{4},{5},{6})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) tblCvEducation.college, (object) tblCvEducation.degree, (object) tblCvEducation.start_date, (object) tblCvEducation.end_date, (object) tblCvEducation.summary);
            foreach (tbl_cv_project project in CVMaster.project_list)
              m2ostnextserviceDbContext.Database.ExecuteSqlCommand("insert into tbl_cv_project (id_cv,id_user,college,project_title,start_date,end_date,summary) values({0},{1},{2},{3},{4},{5},{6})", (object) CVMaster.id_cv, (object) CVMaster.UID, (object) project.college, (object) project.project_title, (object) project.start_date, (object) project.end_date, (object) project.summary);
          }
        }
      }
      catch (Exception ex)
      {
        cvBuilderResponse.STATUS = "FAILED";
        return namespace2.CreateResponse<CVBuilderResponse>(this.Request, HttpStatusCode.OK, cvBuilderResponse);
      }
      finally
      {
        cvBuilderResponse.STATUS = "SUCCESS";
        cvBuilderResponse.RESUMELINK = ConfigurationManager.AppSettings["CVControl"].ToString() + CVMaster.id_cv.ToString();
      }
      return namespace2.CreateResponse<CVBuilderResponse>(this.Request, HttpStatusCode.OK, cvBuilderResponse);
    }
  }
}
