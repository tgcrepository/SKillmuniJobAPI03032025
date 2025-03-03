// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getCategoryTileListForNonLearningController
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

    public class getCategoryTileListForNonLearningController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID, int tile_type)
    {
      string str = ConfigurationManager.AppSettings["SERVERPATH"].ToString() + "BRIEF/";
      List<tbl_brief_category_tile> briefCategoryTileList = new List<tbl_brief_category_tile>();
      List<tbl_brief_category_tile> list1 = this.db.Database.SqlQuery<tbl_brief_category_tile>("select * from tbl_brief_category_tile where tile_type={0} and status='A' ORDER BY tile_position ASC", (object) tile_type).ToList<tbl_brief_category_tile>();
      foreach (tbl_brief_category_tile briefCategoryTile in list1)
      {
        briefCategoryTile.buttontext = this.db.Database.SqlQuery<string>("select buttontext from tbl_brief_category_tile where id_brief_category_tile={0} ", (object) briefCategoryTile.id_brief_category_tile).FirstOrDefault<string>();
        briefCategoryTile.tile_image = str + briefCategoryTile.id_organization.ToString() + "/TILE/" + briefCategoryTile.tile_image;
      }
      List<tbl_brief_category_tile> list2 = list1.OrderBy<tbl_brief_category_tile, DateTime?>((Func<tbl_brief_category_tile, DateTime?>) (o => o.updated_date_time)).ToList<tbl_brief_category_tile>();
      BriefTilResponse briefTilResponse = new BriefTilResponse();
      if (list2.Count > 0)
      {
        briefTilResponse.Status = "SUCCESS";
        List<tbl_brief_category_tile> list3 = list2.OrderBy<tbl_brief_category_tile, int?>((Func<tbl_brief_category_tile, int?>) (x => x.tile_position)).ToList<tbl_brief_category_tile>();
        briefTilResponse.Tile = list3;
      }
      else
        briefTilResponse.Status = "FAILED";
      return namespace2.CreateResponse<BriefTilResponse>(this.Request, HttpStatusCode.OK, briefTilResponse);
    }
  }
}
