// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getJobCatListController
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

    public class getJobCatListController : ApiController
  {
    public HttpResponseMessage Get(int OID)
    {
      tbl_job_category_header jobCategoryHeader = new tbl_job_category_header();
      jobCategoryHeader.header = "Select Category";
      jobCategoryHeader.id_header = 1;
      jobCategoryHeader.status = "A";
      jobCategoryHeader.updated_date_time = DateTime.Now;
      List<tbl_job_category> tblJobCategoryList = new List<tbl_job_category>();
      List<JOBCATEGORYLIST> jobcategorylistList = new List<JOBCATEGORYLIST>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tblJobCategoryList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_job_category>("select * from tbl_job_category where status='A'").ToList<tbl_job_category>();
        foreach (tbl_job_category tblJobCategory in tblJobCategoryList)
          jobcategorylistList.Add(new JOBCATEGORYLIST()
          {
            id_job_category = tblJobCategory.id_job_category,
            job_category = tblJobCategory.job_category,
            tile_image = ConfigurationManager.AppSettings["jobcatimg"].ToString() + tblJobCategory.tile_image,
            tile_position = tblJobCategory.tile_position
          });
      }
      if (tblJobCategoryList.Count > 0)
        jobCategoryHeader.category = tblJobCategoryList;
      return namespace2.CreateResponse<tbl_job_category_header>(this.Request, HttpStatusCode.OK, jobCategoryHeader);
    }
  }
}
