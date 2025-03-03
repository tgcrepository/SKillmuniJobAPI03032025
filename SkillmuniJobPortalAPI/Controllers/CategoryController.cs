// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CategoryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class CategoryController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int orgID, int uid)
    {
      APIRESPONSE apiresponse = new APIRESPONSE();
      downtime_log downtimeLog = this.db.downtime_log.Where<downtime_log>((Expression<Func<downtime_log, bool>>) (t => t.status == "A")).FirstOrDefault<downtime_log>();
      if (downtimeLog != null)
      {
        apiresponse.KEY = "FAILURE";
        apiresponse.MESSAGE = downtimeLog.message_text;
      }
      else
      {
        List<tbl_category_tiles> list1 = this.db.tbl_category_tiles.Where<tbl_category_tiles>((Expression<Func<tbl_category_tiles, bool>>) (t => t.id_organization == (int?) orgID && t.status == "A")).ToList<tbl_category_tiles>();
        List<CategoryTile> source = new List<CategoryTile>();
        foreach (tbl_category_tiles tblCategoryTiles in list1)
        {
          CategoryTile categoryTile1 = new CategoryTile();
          categoryTile1.CategoryID = tblCategoryTiles.id_category_tiles;
          categoryTile1.CategoryName = tblCategoryTiles.tile_heading;
          categoryTile1.CategoryDescription = "";
          categoryTile1.OrganisationId = orgID;
          string str1 = HttpUtility.UrlEncode(tblCategoryTiles.tile_image);
          categoryTile1.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + "Tiles/" + str1;
          categoryTile1.Is_Primary = 1;
          categoryTile1.SubCount = new CategoryModel().getSubCount(tblCategoryTiles.id_category_tiles.ToString());
          CategoryTile categoryTile2 = categoryTile1;
          int? categoryTheme = tblCategoryTiles.category_theme;
          string str2 = categoryTheme.ToString();
          categoryTile2.Template = str2;
          int int32 = Convert.ToInt32((object) tblCategoryTiles.category_order);
          categoryTile1.ORDERID = int32;
          categoryTheme = tblCategoryTiles.category_theme;
          int num1 = 1;
          if (categoryTheme.GetValueOrDefault() == num1 & categoryTheme.HasValue)
          {
            categoryTile1.NEXTAPI = "api/DisplayCategory?orgID=" + orgID.ToString() + "&cid=" + tblCategoryTiles.id_category_tiles.ToString() + "&uid=" + uid.ToString();
          }
          else
          {
            categoryTheme = tblCategoryTiles.category_theme;
            int num2 = 2;
            if (categoryTheme.GetValueOrDefault() == num2 & categoryTheme.HasValue)
            {
              categoryTile1.NEXTAPI = "api/MyAssignment?orgID=" + orgID.ToString() + "&cid=" + tblCategoryTiles.id_category_tiles.ToString() + "&uid=" + uid.ToString();
            }
            else
            {
              categoryTheme = tblCategoryTiles.category_theme;
              int num3 = 5;
              if (categoryTheme.GetValueOrDefault() == num3 & categoryTheme.HasValue)
              {
                categoryTile1.NEXTAPI = "api/getLearningAssessment?CID=" + tblCategoryTiles.id_category_tiles.ToString() + "&uid=" + uid.ToString() + "&oid=" + orgID.ToString();
              }
              else
              {
                categoryTheme = tblCategoryTiles.category_theme;
                int num4 = 4;
                if (categoryTheme.GetValueOrDefault() == num4 & categoryTheme.HasValue)
                {
                  categoryTile1.NEXTAPI = "api/DisplayCategory?orgID=" + orgID.ToString() + "&cid=" + tblCategoryTiles.id_category_tiles.ToString() + "&uid=" + uid.ToString();
                }
                else
                {
                  categoryTheme = tblCategoryTiles.category_theme;
                  int num5 = 6;
                  if (categoryTheme.GetValueOrDefault() == num5 & categoryTheme.HasValue)
                  {
                    categoryTile1.NEXTAPI = "api/DisplayCategory?orgID=" + orgID.ToString() + "&cid=" + tblCategoryTiles.id_category_tiles.ToString() + "&uid=" + uid.ToString();
                  }
                  else
                  {
                    categoryTheme = tblCategoryTiles.category_theme;
                    int num6 = 7;
                    if (categoryTheme.GetValueOrDefault() == num6 & categoryTheme.HasValue)
                      categoryTile1.NEXTAPI = tblCategoryTiles.image_url;
                  }
                }
              }
            }
          }
          source.Add(categoryTile1);
        }
        List<CategoryTile> list2 = source.OrderBy<CategoryTile, int>((Func<CategoryTile, int>) (t => t.ORDERID)).ToList<CategoryTile>();
        apiresponse.KEY = "SUCCESS";
        string str = JsonConvert.SerializeObject((object) list2);
        apiresponse.MESSAGE = str;
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
