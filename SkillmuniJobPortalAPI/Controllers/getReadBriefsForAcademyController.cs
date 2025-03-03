// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getReadBriefsForAcademyController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class getReadBriefsForAcademyController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, int OID, int AcadamyTileId)
    {
      string str = new Utility().mysqlTrim(UID.ToString());
      new Utility().mysqlTrim(OID.ToString());
      List<tbl_brief_tile_academic_mapping> tileAcademicMappingList = new List<tbl_brief_tile_academic_mapping>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        tileAcademicMappingList = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_tile_academic_mapping>("Select * from  tbl_brief_tile_academic_mapping  where id_academic_tile={0}", (object) AcadamyTileId).ToList<tbl_brief_tile_academic_mapping>();
      foreach (tbl_brief_tile_academic_mapping tileAcademicMapping in tileAcademicMappingList)
        tileAcademicMapping.BriefTileCode = this.db.Database.SqlQuery<string>("Select tile_code from  tbl_brief_category_tile  where id_brief_category_tile={0}", (object) tileAcademicMapping.id_journey_tile).FirstOrDefault<string>();
      List<BriefAPIResource> source = new List<BriefAPIResource>();
      List<tbl_brief_master> tblBriefMasterList = new List<tbl_brief_master>();
      ////"select * from tbl_brief_user_assignment where id_user='" + str + "' and assignment_status='S'  and id_brief_master in (SELECT id_brief_master FROM tbl_brief_master where status='A' and id_organization=" + OID.ToString() + ")";
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        List<tbl_brief_tile_academic_mapping> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_tile_academic_mapping>("Select * from  tbl_brief_tile_academic_mapping  where id_academic_tile={0}", (object) AcadamyTileId).ToList<tbl_brief_tile_academic_mapping>();
        foreach (tbl_brief_tile_academic_mapping tileAcademicMapping in list)
          tileAcademicMapping.BriefTileCode = m2ostnextserviceDbContext.Database.SqlQuery<string>("Select tile_code from  tbl_brief_category_tile  where id_brief_category_tile={0}", (object) tileAcademicMapping.id_journey_tile).FirstOrDefault<string>();
        foreach (tbl_brief_tile_academic_mapping tileAcademicMapping in list)
        {
          int num1 = m2ostnextserviceDbContext.Database.SqlQuery<int>("SELECT id_brief_category_tile FROM tbl_brief_category_tile where tile_code={0}", (object) tileAcademicMapping.BriefTileCode).FirstOrDefault<int>();
          foreach (tbl_brief_master tblBriefMaster in m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_master>("SELECT * FROM tbl_brief_category_tile INNER JOIN tbl_brief_tile_category_mapping ON tbl_brief_category_tile.id_brief_category_tile = tbl_brief_tile_category_mapping.id_brief_category_tile where tbl_brief_category_tile.id_organization={0} and tbl_brief_tile_category_mapping.id_brief_category_tile={1} ;", (object) OID, (object) num1).ToList<tbl_brief_master>())
          {
            int? idBriefCategory = tblBriefMaster.id_brief_category;
            int num2 = 0;
            if (!(idBriefCategory.GetValueOrDefault() == num2 & idBriefCategory.HasValue))
            {
              List<tbl_brief_master> briefList = new BriefModel().getBriefList("select * from tbl_brief_master where status='A' and id_brief_category=" + tblBriefMaster.id_brief_category.ToString(), OID);
              tblBriefMasterList.AddRange((IEnumerable<tbl_brief_master>) briefList);
            }
          }
        }
      }
      int num = 1;
      foreach (tbl_brief_master tblBriefMaster in tblBriefMasterList)
      {
        tbl_brief_master itm = tblBriefMaster;
        BriefAPIResource briefApiResource = new BriefAPIResource();
        briefApiResource.SRNO = num;
        briefApiResource.brief_title = itm.brief_title;
        briefApiResource.brief_description = itm.brief_description;
        briefApiResource.id_brief_category = itm.id_brief_category;
        briefApiResource.id_brief_master = itm.id_brief_master;
        briefApiResource.id_organization = OID;
        briefApiResource.id_user = UID;
        briefApiResource.brief_code = itm.brief_code;
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          List<tbl_brief_question_mapping> list = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_question_mapping>("SELECT * FROM tbl_brief_question_mapping where id_brief_master={0}", (object) briefApiResource.id_brief_master).ToList<tbl_brief_question_mapping>();
          if (list.Count > 0)
          {
            briefApiResource.question_count = list.Count;
            briefApiResource.is_question_attached = 1;
          }
          briefApiResource.brief_category = m2ostnextserviceDbContext.Database.SqlQuery<string>("select brief_category from tbl_brief_category where id_brief_category={0}", (object) briefApiResource.id_brief_category).FirstOrDefault<string>();
        }
        ++num;
        tbl_brief_log tblBriefLog = this.db.tbl_brief_log.Where<tbl_brief_log>((Expression<Func<tbl_brief_log, bool>>) (t => t.attempt_no == 1 && t.id_brief_master == itm.id_brief_master && t.id_user == UID)).FirstOrDefault<tbl_brief_log>();
        if (tblBriefLog != null)
        {
          briefApiResource.read_status = 1;
          briefApiResource.RESULTSTATUS = 1;
          briefApiResource.RESULTSCORE = Convert.ToDouble((object) tblBriefLog.brief_result);
        }
        else
        {
          briefApiResource.read_status = 0;
          briefApiResource.RESULTSTATUS = 0;
          briefApiResource.RESULTSCORE = 0.0;
        }
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_brief_m2ost_category_mapping m2ostCategoryMapping = new tbl_brief_m2ost_category_mapping();
          briefApiResource.cat_mapping = m2ostnextserviceDbContext.Database.SqlQuery<tbl_brief_m2ost_category_mapping>("select * from tbl_brief_m2ost_category_mapping where id_brief={0}", (object) itm.id_brief_master).FirstOrDefault<tbl_brief_m2ost_category_mapping>();
          if (briefApiResource.cat_mapping != null)
          {
            if (briefApiResource.cat_mapping.type == 2)
            {
              using (M2ostCatDbContext m2ostCatDbContext = new M2ostCatDbContext())
              {
                briefApiResource.cat_mapping.CATEGORYNAME = m2ostCatDbContext.Database.SqlQuery<string>("select CATEGORYNAME from tbl_category where ID_CATEGORY={0} ", (object) briefApiResource.cat_mapping.id_category).FirstOrDefault<string>();
                briefApiResource.cat_mapping.Heading_title = m2ostCatDbContext.Database.SqlQuery<string>("select Heading_title from tbl_category_heading where id_category_heading={0} ", (object) briefApiResource.cat_mapping.id_category_heading).FirstOrDefault<string>();
                briefApiResource.cat_mapping.CategoryImage = ConfigurationManager.AppSettings["CATIm"].ToString() + m2ostCatDbContext.Database.SqlQuery<string>("select IMAGE_PATH from tbl_category where ID_CATEGORY={0} ", (object) briefApiResource.cat_mapping.id_category).FirstOrDefault<string>();
              }
            }
          }
        }
        this.db.tbl_brief_master.Where<tbl_brief_master>((Expression<Func<tbl_brief_master, bool>>) (t => t.id_brief_master == itm.id_brief_master)).FirstOrDefault<tbl_brief_master>();
        tbl_brief_master_template briefMasterTemplate = this.db.tbl_brief_master_template.Where<tbl_brief_master_template>((Expression<Func<tbl_brief_master_template, bool>>) (t => t.id_brief_master == (int?) itm.id_brief_master)).FirstOrDefault<tbl_brief_master_template>();
        briefApiResource.brief_template = briefMasterTemplate == null ? "0" : briefMasterTemplate.brief_template;
        List<tbl_brief_master_body> list1 = this.db.tbl_brief_master_body.Where<tbl_brief_master_body>((Expression<Func<tbl_brief_master_body, bool>>) (t => t.id_brief_master == (int?) itm.id_brief_master)).OrderBy<tbl_brief_master_body, int?>((Expression<Func<tbl_brief_master_body, int?>>) (t => t.srno)).ToList<tbl_brief_master_body>();
        List<BriefRow> briefRowList = new List<BriefRow>();
        foreach (tbl_brief_master_body tblBriefMasterBody in list1)
        {
          BriefRow briefRow = new BriefRow()
          {
            media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type),
            resouce_code = tblBriefMasterBody.resouce_code,
            resource_order = briefMasterTemplate.resource_order,
            brief_destination = tblBriefMasterBody.brief_destination,
            resource_number = tblBriefMasterBody.resource_number,
            srno = Convert.ToInt32((object) tblBriefMasterBody.srno),
            resource_type = Convert.ToInt32((object) tblBriefMasterBody.resource_type),
            resouce_data = tblBriefMasterBody.resouce_data
          };
          briefRow.resouce_code = tblBriefMasterBody.resouce_code;
          briefRow.media_type = Convert.ToInt32((object) tblBriefMasterBody.media_type);
          briefRow.resource_mime = tblBriefMasterBody.resource_mime;
          briefRow.file_extension = tblBriefMasterBody.file_extension;
          briefRow.file_type = tblBriefMasterBody.file_type;
          briefRowList.Add(briefRow);
        }
        briefApiResource.briefResource = briefRowList;
        source.Add(briefApiResource);
      }
      List<BriefAPIResource> briefApiResourceList = new List<BriefAPIResource>();
      return source != null ? namespace2.CreateResponse<List<BriefAPIResource>>(this.Request, HttpStatusCode.OK, source.Where<BriefAPIResource>((Func<BriefAPIResource, bool>) (t => t.read_status == 1)).ToList<BriefAPIResource>()) : namespace2.CreateResponse<List<BriefAPIResource>>(this.Request, HttpStatusCode.NoContent, briefApiResourceList);
    }
  }
}
