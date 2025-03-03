// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefTilesController
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

    public class getBriefTilesController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID, int AcademicTileId)
    {
      new RoadMapLogic().GameTileLog(UID, OID, AcademicTileId);
      string str = ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "BRIEF/";
      List<RoadMapModels.tbl_brief_tile_academic_mapping> tilesMapping = new RoadMapLogic().getTilesMapping(AcademicTileId);
      List<tbl_brief_category_tile> source = new List<tbl_brief_category_tile>();
      foreach (RoadMapModels.tbl_brief_tile_academic_mapping tileAcademicMapping in tilesMapping)
      {
        tbl_brief_category_tile journeytile = new RoadMapLogic().getJourneytile(tileAcademicMapping.id_journey_tile);
        source.Add(journeytile);
      }
      foreach (tbl_brief_category_tile briefCategoryTile in source)
        briefCategoryTile.tile_image = str + briefCategoryTile.id_organization.ToString() + "/TILE/" + briefCategoryTile.tile_image;
      return namespace2.CreateResponse<List<tbl_brief_category_tile>>(this.Request, HttpStatusCode.OK, source.OrderBy<tbl_brief_category_tile, int?>((Func<tbl_brief_category_tile, int?>) (o => o.tile_position)).ToList<tbl_brief_category_tile>());
    }
  }
}
