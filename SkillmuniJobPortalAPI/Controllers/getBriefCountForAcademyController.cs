// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefCountForAcademyController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class getBriefCountForAcademyController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int AcadamyTileId)
    {
      new Utility().mysqlTrim(UID.ToString());
      new Utility().mysqlTrim(OID.ToString());
      BriefCountResponse briefCountResponse = new BriefCountResponse();
      briefCountResponse.TOTALCOUNT = 0;
      briefCountResponse.ReadCount = 0;
      briefCountResponse.UnReadCount = 0;
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        briefCountResponse.TOTALCOUNT = m2ostnextserviceDbContext.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) FROM tbl_brief_master m INNER JOIN tbl_brief_tile_category_mapping catm ON catm.id_brief_category = m.id_brief_category INNER JOIN tbl_brief_category_tile cat ON cat.id_brief_category_tile = catm.id_brief_category_tile INNER JOIN tbl_brief_tile_academic_mapping ac ON ac.id_journey_tile = cat.id_brief_category_tile WHERE m.status='A' and cat.id_organization={0} and ac.id_academic_tile={1};", (object) OID, (object) AcadamyTileId)).FirstOrDefault<int>();
        briefCountResponse.ReadCount = m2ostnextserviceDbContext.Database.SqlQuery<int>(string.Format("SELECT COUNT(*) from tbl_brief_log log\r\nINNER JOIN tbl_brief_master m ON m.id_brief_master = log.id_brief_master\r\nINNER JOIN tbl_brief_tile_category_mapping catm ON catm.id_brief_category = m.id_brief_category\r\nINNER JOIN tbl_brief_category_tile cat ON cat.id_brief_category_tile = catm.id_brief_category_tile \r\nINNER JOIN tbl_brief_tile_academic_mapping ac ON ac.id_journey_tile = cat.id_brief_category_tile\r\nWHERE m.status='A' and cat.id_organization={0} and ac.id_academic_tile={1} and log.id_user = {2};", (object) OID, (object) AcadamyTileId, (object) UID)).FirstOrDefault<int>();
        briefCountResponse.UnReadCount = briefCountResponse.TOTALCOUNT - briefCountResponse.ReadCount;
      }
      return namespace2.CreateResponse<BriefCountResponse>(this.Request, HttpStatusCode.OK, briefCountResponse);
    }
  }
}
