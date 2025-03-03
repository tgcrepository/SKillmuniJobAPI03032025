// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getAssessmentsListForJobController
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

    public class getAssessmentsListForJobController : ApiController
  {
    public HttpResponseMessage Get()
    {
      List<JobAssessments> jobAssessmentsList = new List<JobAssessments>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          jobAssessmentsList = m2ostnextserviceDbContext.Database.SqlQuery<JobAssessments>("select * from tbl_ce_career_evaluation_master where status={0}", (object) "A").ToList<JobAssessments>();
          foreach (JobAssessments jobAssessments in jobAssessmentsList)
            jobAssessments.Cat = m2ostnextserviceDbContext.Database.SqlQuery<AssessmentCategory>("SELECT tbl_ce_category_mapping.id_ce_category_mapping, tbl_brief_category.brief_category, tbl_brief_category.id_brief_category FROM tbl_ce_category_mapping INNER JOIN tbl_brief_category ON tbl_ce_category_mapping.id_brief_category = tbl_brief_category.id_brief_category where tbl_ce_category_mapping.id_ce_career_evaluation_master={0} and tbl_ce_category_mapping.status={1}", (object) jobAssessments.id_ce_career_evaluation_master, (object) "A").ToList<AssessmentCategory>();
        }
      }
      catch (Exception ex)
      {
      }
      return namespace2.CreateResponse<List<JobAssessments>>(this.Request, HttpStatusCode.OK, jobAssessmentsList);
    }
  }
}
