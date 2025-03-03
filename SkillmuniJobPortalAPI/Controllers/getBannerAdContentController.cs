// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBannerAdContentController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class getBannerAdContentController : ApiController
  {
    public HttpResponseMessage Get(int id_academy, int id_cat_tile)
    {
      bannerApi bannerApi = new bannerApi();
      List<tbl_banner_config_master> bannerConfigMasterList = new List<tbl_banner_config_master>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<tbl_banner_config_master> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_banner_config_master>("select * from tbl_banner_config_master inner join tbl_banner_ad_config on tbl_banner_config_master.id_banner_config= tbl_banner_ad_config.id_banner_config where tbl_banner_ad_config.id_academic_tile={0} and tbl_banner_ad_config.id_brief_category_tile={1} and tbl_banner_config_master.status='A'", (object) id_academy, (object) id_cat_tile).ToList<tbl_banner_config_master>();
        foreach (tbl_banner_config_master bannerConfigMaster in list)
        {
          if (list != null)
          {
            bannerConfigMaster.banner_ad = m2ostnextserviceDbContext.Database.SqlQuery<tbl_banner_ad_config>("SELECT * FROM tbl_banner_ad_config where status='A' and id_banner_config={0} ", (object) bannerConfigMaster.id_banner_config).ToList<tbl_banner_ad_config>();
            bannerConfigMaster.bannerbody = m2ostnextserviceDbContext.Database.SqlQuery<tbl_banner_body>("SELECT * FROM tbl_banner_body where status='A' and id_banner_config={0} ", (object) bannerConfigMaster.id_banner_config).ToList<tbl_banner_body>();
            foreach (tbl_banner_body tblBannerBody in bannerConfigMaster.bannerbody)
              tblBannerBody.banner_image = ConfigurationManager.AppSettings["BANNERBODYIMAGE"].ToString() + tblBannerBody.banner_image;
          }
        }
        if (list.Count > 0)
        {
          bannerApi.status = "SUCCESS";
          bannerApi.ad_master_banner = list;
        }
        else
          bannerApi.status = "FAILURE";
      }
      return namespace2.CreateResponse<bannerApi>(this.Request, HttpStatusCode.OK, bannerApi);
    }
  }
}
