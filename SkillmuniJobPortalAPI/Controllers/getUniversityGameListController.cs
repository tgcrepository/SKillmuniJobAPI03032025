// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getUniversityGameListController
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

    public class getUniversityGameListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage GET(int OID)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      List<tbl_game_master> tblGameMasterList = new List<tbl_game_master>();
      try
      {
        using (db_m2ostEntities dbM2ostEntities = new db_m2ostEntities())
        {
          string sql = "SELECT * FROM tbl_game_master  INNER JOIN tbl_theme_master ON tbl_game_master.id_theme = tbl_theme_master.id_theme where tbl_game_master.id_org=" + OID.ToString() + " and tbl_game_master.status='A'";
          tblGameMasterList = dbM2ostEntities.Database.SqlQuery<tbl_game_master>(sql).ToList<tbl_game_master>();
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<List<tbl_game_master>>(this.Request, HttpStatusCode.OK, tblGameMasterList);
    }
  }
}
