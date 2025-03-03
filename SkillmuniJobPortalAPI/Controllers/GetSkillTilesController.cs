// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetSkillTilesController
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

    public class GetSkillTilesController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID)
    {
      string str = ConfigurationManager.AppSettings["GameTile"].ToString();
      List<RoadMapModels.tbl_academic_tiles> list = new RoadMapLogic().getGameTiles(OID).Where<RoadMapModels.tbl_academic_tiles>((Func<RoadMapModels.tbl_academic_tiles, bool>) (o => o.tile_position > 10000)).ToList<RoadMapModels.tbl_academic_tiles>();
      foreach (RoadMapModels.tbl_academic_tiles tblAcademicTiles in list)
        tblAcademicTiles.tile_image = str + tblAcademicTiles.tile_image;
      return namespace2.CreateResponse<List<RoadMapModels.tbl_academic_tiles>>(this.Request, HttpStatusCode.OK, list);
    }
  }
}
