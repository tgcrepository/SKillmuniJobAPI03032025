// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.DisplayCategoryController
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

    public class DisplayCategoryController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int orgID, int cid, int uid)
    {
      List<DisplayCategory> source1 = new List<DisplayCategory>();
      DateTime now = DateTime.Now;
      try
      {
        List<tbl_category_heading> list1 = this.db.tbl_category_heading.SqlQuery("select a.* from tbl_category_heading a left join tbl_category_associantion b on a.id_category_heading =b.id_category_heading where id_category_tile=" + cid.ToString() + " and b.status='A'").ToList<tbl_category_heading>();
        this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == uid)).FirstOrDefault<tbl_user>();
        List<tbl_csst_role> list2 = this.db.tbl_csst_role.SqlQuery("select a.* from tbl_csst_role a left join tbl_role_user_mapping b on a.id_csst_role = b.id_csst_role  where b.id_user=" + uid.ToString() + " ").ToList<tbl_csst_role>();
        string str1 = "";
        foreach (tbl_csst_role tblCsstRole in list2)
          str1 = str1 + tblCsstRole.id_csst_role.ToString() + ",";
        str1.TrimEnd(',');
        string str2 = "";
        string str3;
        if (str2 == "")
          str3 = "(id_user=" + uid.ToString() + ")";
        else
          str3 = "(id_role in (" + str2 + ") or id_user=" + uid.ToString() + ")";
        string[] strArray1 = new string[5]
        {
          "select * from tbl_category_heading where  status='A' and  id_category_heading in (select distinct id_category_heading from tbl_content_program_mapping where id_category_tile=",
          cid.ToString(),
          " and ",
          str3,
          ")"
        };
        foreach (tbl_category_heading tblCategoryHeading in this.db.tbl_category_heading.SqlQuery(string.Concat(strArray1)).ToList<tbl_category_heading>())
          list1.Add(tblCategoryHeading);
        List<tbl_category_heading> list3 = list1.Distinct<tbl_category_heading>().ToList<tbl_category_heading>();
        if (list3.Count > 0)
        {
          int num1 = 1;
          foreach (tbl_category_heading tblCategoryHeading in list3)
          {
            List<Category> categoryList = new List<Category>();
            List<Category> source2 = new List<Category>();
            DisplayCategory displayCategory = new DisplayCategory();
            displayCategory.Heading = tblCategoryHeading.Heading_title;
            displayCategory.Order = tblCategoryHeading.heading_order.ToString();
            List<tbl_content_program_mapping> list4 = this.db.tbl_content_program_mapping.SqlQuery("select distinct * from tbl_content_program_mapping where id_category_tile=" + cid.ToString() + " and id_category_heading =" + tblCategoryHeading.id_category_heading.ToString() + " and " + str3).ToList<tbl_content_program_mapping>();
            int num2 = 1;
            foreach (tbl_content_program_mapping contentProgramMapping in list4)
            {
              tbl_content_program_mapping pItem = contentProgramMapping;
              if (DateTime.Compare(pItem.expiry_date.Value.AddDays(1.0), now) > 0)
              {
                tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == pItem.id_category && t.CATEGORY_TYPE == (int?) 0)).FirstOrDefault<tbl_category>();
                if (tblCategory != null)
                {
                  bool flag = true;
                  int? categoryType = tblCategory.CATEGORY_TYPE;
                  int num3 = 0;
                  if (!(categoryType.GetValueOrDefault() == num3 & categoryType.HasValue))
                  {
                    categoryType = tblCategory.CATEGORY_TYPE;
                    int num4 = 1;
                    if (!(categoryType.GetValueOrDefault() == num4 & categoryType.HasValue))
                    {
                      categoryType = tblCategory.CATEGORY_TYPE;
                      int num5 = 2;
                      if (!(categoryType.GetValueOrDefault() == num5 & categoryType.HasValue))
                        goto label_25;
                    }
                  }
                  flag = true;
label_25:
                  Category category = new Category()
                  {
                    CategoryID = tblCategory.ID_CATEGORY,
                    CategoryName = tblCategory.CATEGORYNAME,
                    CategoryDescription = tblCategory.DESCRIPTION,
                    OrganisationId = tblCategory.ID_ORGANIZATION
                  };
                  category.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category.OrganisationId.ToString() + "/" + tblCategory.IMAGE_PATH.ToString().Trim();
                  category.Is_Primary = tblCategory.ID_CATEGORY;
                  category.Is_Program = Convert.ToInt32((object) tblCategory.CATEGORY_TYPE);
                  category.IS_COUNT_REQUIRED = Convert.ToInt32((object) tblCategory.COUNT_REQUIRED);
                  category.SubCount = 0;
                  category.ORDERID = num2;
                  category.CategoryHeader = category.CategoryName;
                  category.CategoryType = Convert.ToInt32((object) tblCategory.CATEGORY_TYPE);
                  category.IS_COUNT_REQUIRED = Convert.ToInt32((object) tblCategory.COUNT_REQUIRED);
                  categoryType = tblCategory.CATEGORY_TYPE;
                  int num6 = 3;
                  category.NEXTURL = !(categoryType.GetValueOrDefault() == num6 & categoryType.HasValue) ? "" : tblCategory.IMAGE_URL;
                  category.ContentCount = !flag ? 0 : 1;
                  category.ExpiryDate = pItem.expiry_date.Value.ToString("dd-MMM-yyyy");
                  category.LINKCOUNT = 0;
                  source2.Add(category);
                }
              }
            }
            foreach (Category category in source2.OrderBy<Category, string>((Func<Category, string>) (t => t.CategoryName)).ToList<Category>())
            {
              category.ORDERID = num2;
              ++num2;
              categoryList.Add(category);
            }
            string[] strArray2 = new string[5]
            {
              "select * from tbl_category where id_category in ( select distinct a.id_category from tbl_category_associantion a,tbl_category b where a.id_category_tile=",
              cid.ToString(),
              " and a.id_category_heading=",
              tblCategoryHeading.id_category_heading.ToString(),
              " and b.status='A' and a.id_category=b.id_category ) order by CATEGORYNAME"
            };
            foreach (tbl_category tblCategory in this.db.tbl_category.SqlQuery(string.Concat(strArray2)).ToList<tbl_category>().OrderBy<tbl_category, int?>((Func<tbl_category, int?>) (t => t.ORDERID)).ToList<tbl_category>())
            {
              if (tblCategory != null)
              {
                bool flag = true;
                int? categoryType = tblCategory.CATEGORY_TYPE;
                int num7 = 0;
                if (!(categoryType.GetValueOrDefault() == num7 & categoryType.HasValue))
                {
                  categoryType = tblCategory.CATEGORY_TYPE;
                  int num8 = 1;
                  if (!(categoryType.GetValueOrDefault() == num8 & categoryType.HasValue))
                  {
                    categoryType = tblCategory.CATEGORY_TYPE;
                    int num9 = 2;
                    if (!(categoryType.GetValueOrDefault() == num9 & categoryType.HasValue))
                      goto label_40;
                  }
                }
                flag = true;
label_40:
                Category category = new Category()
                {
                  CategoryID = tblCategory.ID_CATEGORY,
                  CategoryName = tblCategory.CATEGORYNAME,
                  CategoryDescription = tblCategory.DESCRIPTION,
                  OrganisationId = tblCategory.ID_ORGANIZATION
                };
                category.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category.OrganisationId.ToString() + "/" + tblCategory.IMAGE_PATH.ToString().Trim();
                category.Is_Primary = tblCategory.ID_CATEGORY;
                category.Is_Program = Convert.ToInt32((object) tblCategory.CATEGORY_TYPE);
                category.IS_COUNT_REQUIRED = Convert.ToInt32((object) tblCategory.COUNT_REQUIRED);
                category.SubCount = 0;
                category.ORDERID = Convert.ToInt32((object) tblCategory.ORDERID) + num2;
                category.CategoryHeader = category.CategoryName;
                category.CategoryType = Convert.ToInt32((object) tblCategory.CATEGORY_TYPE);
                category.IS_COUNT_REQUIRED = Convert.ToInt32((object) tblCategory.COUNT_REQUIRED);
                categoryType = tblCategory.CATEGORY_TYPE;
                int num10 = 3;
                category.NEXTURL = !(categoryType.GetValueOrDefault() == num10 & categoryType.HasValue) ? "" : tblCategory.IMAGE_URL;
                category.ContentCount = !flag ? 0 : 1;
                category.ExpiryDate = "";
                category.LINKCOUNT = 0;
                categoryList.Add(category);
              }
            }
            displayCategory.Categories = categoryList;
            source1.Add(displayCategory);
            ++num1;
          }
        }
        source1 = source1.OrderBy<DisplayCategory, string>((Func<DisplayCategory, string>) (t => t.Order)).ToList<DisplayCategory>();
      }
      catch (Exception ex)
      {
        new Utility().eventLog("ex m :" + ex.Message);
        new Utility().eventLog("ex s :" + ex.StackTrace);
        if (ex.InnerException != null)
          new Utility().eventLog("ex i :" + ex.InnerException.ToString());
      }
      if (source1 != null)
        return namespace2.CreateResponse<List<DisplayCategory>>(this.Request, HttpStatusCode.OK, source1);
      return namespace2.CreateResponse<List<DisplayCategory>>(this.Request, HttpStatusCode.NoContent, source1);
    }
  }
}
