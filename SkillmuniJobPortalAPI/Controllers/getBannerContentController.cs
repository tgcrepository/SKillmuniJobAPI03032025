// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBannerContentController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class getBannerContentController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int LOCATION)
    {
      tbl_banner_config_master bannerConfigMaster = new tbl_banner_config_master();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        bannerConfigMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_banner_config_master>("SELECT * FROM tbl_banner_config_master where banner_location={0} and banner_type={1}", (object) LOCATION, (object) 1).FirstOrDefault<tbl_banner_config_master>();
        if (bannerConfigMaster != null)
        {
          bannerConfigMaster.bannerbody = m2ostnextserviceDbContext.Database.SqlQuery<tbl_banner_body>("SELECT * FROM tbl_banner_body where status='A' and id_banner_config={0} ", (object) bannerConfigMaster.id_banner_config).ToList<tbl_banner_body>();
          foreach (tbl_banner_body tblBannerBody in bannerConfigMaster.bannerbody)
            tblBannerBody.banner_image = ConfigurationManager.AppSettings["BANNERBODYIMAGE"].ToString() + tblBannerBody.banner_image;
        }
      }
      return namespace2.CreateResponse<tbl_banner_config_master>(this.Request, HttpStatusCode.OK, bannerConfigMaster);
    }
  }
}
