// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getBriefListwithAcademyController
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

    public class getBriefListwithAcademyController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID, string ENC, int id_academy)
    {
      BriefResponse briefResponse = new BriefResponse();
      int num1 = 0;
      try
      {
        new Utility().mysqlTrim(UID.ToString());
        new Utility().mysqlTrim(OID.ToString());
        string str1 = new Utility().mysqlTrim(ENC);
        int num2 = 0;
        tbl_brief_tile_level_brief_restriction briefRestriction1 = new tbl_brief_tile_level_brief_restriction();
        tbl_academy_level_brief_restriction briefRestriction2 = new tbl_academy_level_brief_restriction();
        List<tbl_restriction_user_log> restrictionUserLogList = new List<tbl_restriction_user_log>();
        List<BriefAPIResource> briefApiResourceList1 = new List<BriefAPIResource>();
        List<BriefAPIResource> briefApiResourceList2 = new List<BriefAPIResource>();
        tbl_brief_category_tile briefCategoryTile = new tbl_brief_category_tile();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          briefCategoryTile = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_category_tile>("Select * from  tbl_brief_category_tile where tile_code = '" + str1 + "'").FirstOrDefault<tbl_brief_category_tile>();
          if (briefCategoryTile != null)
          {
            briefRestriction2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction>("select * from tbl_academy_level_brief_restriction where id_academy = {0}", (object) id_academy).FirstOrDefault<tbl_academy_level_brief_restriction>();
            briefRestriction1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_tile_level_brief_restriction>("select * from tbl_brief_tile_level_brief_restriction where id_brief_tile = {0} and id_academy={1}", (object) briefCategoryTile.id_brief_category_tile, (object) id_academy).FirstOrDefault<tbl_brief_tile_level_brief_restriction>();
          }
        }
        if (briefCategoryTile != null)
        {
          int num3 = 0;
          using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
          {
            if (briefRestriction1 != null)
            {
              if (briefRestriction1.time == 1)
              {
                DateTime date = DateTime.Now.Date;
                num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(*) from tbl_restriction_user_log where UID = {0} and id_brief_tile={1} and date(updated_date_time)={2} and id_academy={3}", (object) UID, (object) briefCategoryTile.id_brief_category_tile, (object) date, (object) id_academy).FirstOrDefault<int>() + 1;
              }
              else if (briefRestriction1.time == 2)
              {
                int hour = DateTime.Now.Hour;
                DateTime date = DateTime.Now.Date;
                num3 = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(*) from tbl_restriction_user_log where UID = {0} and id_brief_tile={1} and EXTRACT(HOUR  FROM updated_date_time)={2} and  date(updated_date_time)={3} and id_academy={4}", (object) UID, (object) briefCategoryTile.id_brief_category_tile, (object) hour, (object) date, (object) id_academy).FirstOrDefault<int>() + 1;
              }
            }
          }
          List<BriefAPIResource> source1 = new List<BriefAPIResource>();
          List<BriefRow> source2 = new List<BriefRow>();
          List<tbl_brief_m2ost_category_mapping> source3 = new List<tbl_brief_m2ost_category_mapping>();
          List<tbl_third_party_app_right_swipe_mapping> source4 = new List<tbl_third_party_app_right_swipe_mapping>();
          using (new m2ostnextserviceDbContext())
          {
            source1 = new BriefModel().getBriefListWithAcademy(string.Format("SELECT distinct m.* , lg.brief_result as briefLogResult, bcat.brief_category, \r\nCASE WHEN feed.id_feedback is not null THEN feed.liked ELSE 0 END as liked,\r\nCASE WHEN feed.id_feedback is not null THEN feed.disliked ELSE 0 END as disliked,\r\nCASE WHEN mt.id_brief_master_template is not null THEN mt.brief_template ELSE '0' END as brief_template \r\nfrom tbl_brief_master m\r\nINNER JOIN tbl_brief_tile_category_mapping catm ON catm.id_brief_category = m.id_brief_category\r\nINNER JOIN tbl_brief_category_tile cat ON cat.id_brief_category_tile = catm.id_brief_category_tile \r\nINNER JOIN tbl_brief_status sta ON sta.id_brief_master = m.id_brief_master\r\nLEFT OUTER JOIN tbl_brief_log lg ON lg.attempt_no = 1 and lg.id_brief_master = m.id_brief_master and m.id_user = {0}\r\nLEFT OUTER JOIN tbl_brief_category bcat ON bcat.id_brief_category = m.id_brief_category\r\nLEFT OUTER JOIN (select UID,OID,id_brief_master, MAX(updated_date_time) as updated_date_time\r\nfrom tbl_brief_user_feedback_master GROUP BY UID,OID,id_brief_master) feedGrp \r\nON feedGrp.id_brief_master = m.id_brief_master and feedGrp.UID = {1}\r\nLEFT OUTER JOIN tbl_brief_user_feedback_master feed \r\nON feed.UID = feedGrp.UID and feed.OID = feedGrp.OID \r\nand feed.id_brief_master = feedGrp.id_brief_master\r\nand feed.updated_date_time = feedGrp.updated_date_time\r\nLEFT OUTER JOIN tbl_brief_master_template mt ON mt.id_brief_master = m.id_brief_master\r\nwhere m.status='A' and cat.id_organization={2} and cat.tile_code = '{3}' and sta.brief_status = 'Published';", (object) UID, (object) UID, (object) OID, (object) str1), UID);
            if (source1 != null)
            {
              if (source1.Any<BriefAPIResource>())
              {
                string empty1 = string.Empty;
                foreach (BriefAPIResource briefApiResource in source1)
                  empty1 += string.Format("{0},", (object) briefApiResource.id_brief_master);
                string str2 = empty1.Substring(0, empty1.Length - 1);
                source2 = this.db.Database.SqlQuery<BriefRow>("SELECT \r\nCASE WHEN mb.media_type is not null THEN mb.media_type ELSE 0 END as media_type,\r\nCASE WHEN mb.srno is not null THEN mb.srno ELSE 0 END as srno,\r\nCASE WHEN mb.resource_type is not null THEN mb.resource_type ELSE 0 END as resource_type,\r\nmb.resouce_code,mb.brief_destination,mb.resource_number\r\n,mb.resouce_data,mb.resouce_code,mb.media_type,mb.resource_mime\r\n,mb.file_extension,mb.file_type,mb.id_brief_master\r\n,mt.resource_order from tbl_brief_master_body mb \r\nLEFT OUTER JOIN tbl_brief_master_template mt ON mt.id_brief_master = mb.id_brief_master\r\n where mb.id_brief_master in ( " + str2 + ");").ToList<BriefRow>();
                source3 = this.db.Database.SqlQuery<tbl_brief_m2ost_category_mapping>("select m.*, c.CATEGORYNAME, c.IMAGE_PATH as CategoryImage, h.Heading_title \r\nfrom tbl_brief_m2ost_category_mapping m\r\nLEFT OUTER JOIN tbl_category c ON c.id_category = m.id_category\r\nLEFT OUTER JOIN tbl_category_heading h ON h.id_category_heading = m.id_category_heading\r\nwhere id_brief in ( " + str2 + ");").ToList<tbl_brief_m2ost_category_mapping>();
                if (source3 != null)
                {
                  if (source3.Any<tbl_brief_m2ost_category_mapping>())
                  {
                    string empty2 = string.Empty;
                    foreach (tbl_brief_m2ost_category_mapping m2ostCategoryMapping in source3)
                      empty2 += string.Format("{0},", (object) m2ostCategoryMapping.id_mapping);
                    source4 = this.db.Database.SqlQuery<tbl_third_party_app_right_swipe_mapping>("select * from tbl_third_party_app_right_swipe_mapping where id_mapping in (" + empty2.Substring(0, empty2.Length - 1) + ") and status='A'").ToList<tbl_third_party_app_right_swipe_mapping>();
                  }
                }
              }
            }
          }
          int num4 = 1;
          if (source1 != null && source1.Any<BriefAPIResource>())
          {
            foreach (BriefAPIResource briefApiResource in source1)
            {
              BriefAPIResource inst = briefApiResource;
              inst.SRNO = num4;
              ++num4;
              if (source2 != null && source2.Any<BriefRow>((Func<BriefRow, bool>) (x => x.id_brief_master == inst.id_brief_master)))
              {
                inst.briefResource = new List<BriefRow>();
                inst.briefResource.AddRange((IEnumerable<BriefRow>) source2.Where<BriefRow>((Func<BriefRow, bool>) (x => x.id_brief_master == inst.id_brief_master)).ToList<BriefRow>());
              }
              if (source3 != null && source3.Any<tbl_brief_m2ost_category_mapping>((Func<tbl_brief_m2ost_category_mapping, bool>) (x => x.id_brief == inst.id_brief_master)))
              {
                inst.cat_mapping = source3.FirstOrDefault<tbl_brief_m2ost_category_mapping>((Func<tbl_brief_m2ost_category_mapping, bool>) (x => x.id_brief == inst.id_brief_master));
                if (inst.cat_mapping.type == 2)
                  inst.cat_mapping.CategoryImage = ConfigurationManager.AppSettings["CATIm"].ToString() + inst.cat_mapping.CategoryImage;
                else if (inst.cat_mapping.type == 3 && source4 != null && source4.Any<tbl_third_party_app_right_swipe_mapping>((Func<tbl_third_party_app_right_swipe_mapping, bool>) (x => x.id_mapping == inst.cat_mapping.id_mapping)))
                  inst.cat_mapping.third_party_app = source4.Where<tbl_third_party_app_right_swipe_mapping>((Func<tbl_third_party_app_right_swipe_mapping, bool>) (x => x.id_mapping == inst.cat_mapping.id_mapping)).ToList<tbl_third_party_app_right_swipe_mapping>();
              }
              if (inst.RESULTSTATUS == 1)
              {
                briefApiResourceList2.Add(inst);
                ++num2;
              }
              else if (briefRestriction1 != null)
              {
                if (num3 <= briefRestriction1.brief_count)
                {
                  briefApiResourceList2.Add(inst);
                  ++num3;
                }
              }
              else
                briefApiResourceList2.Add(inst);
            }
          }
        }
        if (briefApiResourceList2 == null)
          return namespace2.CreateResponse<BriefResponse>(this.Request, HttpStatusCode.NoContent, briefResponse);
        briefResponse.BriefList = briefApiResourceList2;
        briefResponse.BriefList = briefResponse.BriefList.OrderBy<BriefAPIResource, int>((Func<BriefAPIResource, int>) (x => x.RESULTSTATUS)).ThenBy<BriefAPIResource, DateTime?>((Func<BriefAPIResource, DateTime?>) (x => x.BrfDate)).ToList<BriefAPIResource>();
        briefResponse.ValidationNumber = num2;
        return namespace2.CreateResponse<BriefResponse>(this.Request, HttpStatusCode.OK, briefResponse);
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<int>(this.Request, HttpStatusCode.NoContent, num1);
      }
    }
  }
}
