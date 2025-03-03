// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getJobCategoryListController
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

    public class getJobCategoryListController : ApiController
  {
    public HttpResponseMessage Get()
    {
      APIRESULTCatTile apiresultCatTile = new APIRESULTCatTile();
      List<tbl_ce_evaluation_jobindustry> evaluationJobindustryList = new List<tbl_ce_evaluation_jobindustry>();
      List<JOBCATTILE> jobcattileList = new List<JOBCATTILE>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        foreach (tbl_ce_evaluation_jobindustry evaluationJobindustry in m2ostnextserviceDbContext.Database.SqlQuery<tbl_ce_evaluation_jobindustry>("select * from tbl_ce_evaluation_jobindustry where status='A'").ToList<tbl_ce_evaluation_jobindustry>())
          jobcattileList.Add(new JOBCATTILE()
          {
            id_job_category = evaluationJobindustry.id_ce_evaluation_jobindustry,
            job_category = evaluationJobindustry.ce_job_industry,
            tile_image = ConfigurationManager.AppSettings["jobcatimg"].ToString() + evaluationJobindustry.tile_image,
            tile_position = evaluationJobindustry.tile_position,
            buttontext = evaluationJobindustry.buttontext
          });
      }
      if (jobcattileList.Count > 0)
      {
        apiresultCatTile.Tile = jobcattileList;
        apiresultCatTile.Tile = apiresultCatTile.Tile.OrderBy<JOBCATTILE, int>((Func<JOBCATTILE, int>) (x => x.tile_position)).ToList<JOBCATTILE>();
        apiresultCatTile.STATUS = "SUCCESS";
      }
      else
        apiresultCatTile.STATUS = "FAILURE";
      return namespace2.CreateResponse<APIRESULTCatTile>(this.Request, HttpStatusCode.OK, apiresultCatTile);
    }
  }
}
