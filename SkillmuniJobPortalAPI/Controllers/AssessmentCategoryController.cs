// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.AssessmentCategoryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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
    public class AssessmentCategoryController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int orgID, int cid, int uid)
    {
      List<DisplayCategory> displayCategoryList = new List<DisplayCategory>();
      List<tbl_category_heading> list1 = this.db.tbl_category_heading.SqlQuery("select * from  tbl_category_heading a left join tbl_category_associantion b on a.id_category_heading=b.id_category_heading where b.id_category_tile = " + cid.ToString() + " and b.status = 'A'").ToList<tbl_category_heading>();
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == uid)).FirstOrDefault<tbl_user>();
      List<tbl_csst_role> list2 = this.db.tbl_csst_role.SqlQuery("select * from tbl_csst_role where id_csst_role in (select id_csst_role from tbl_role_user_mapping where id_user=" + uid.ToString() + ")").ToList<tbl_csst_role>();
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
      string[] strArray = new string[5]
      {
        "select * from tbl_category_heading where id_category_heading in (select distinct id_category_heading from tbl_content_program_mapping where id_category_tile=",
        cid.ToString(),
        " and ",
        str3,
        ")"
      };
      foreach (tbl_category_heading tblCategoryHeading in this.db.tbl_category_heading.SqlQuery(string.Concat(strArray)).ToList<tbl_category_heading>())
        list1.Add(tblCategoryHeading);
      List<tbl_category_heading> list3 = list1.Distinct<tbl_category_heading>().ToList<tbl_category_heading>();
      if (list3.Count > 0)
      {
        foreach (tbl_category_heading tblCategoryHeading in list3)
        {
          List<tbl_category_associantion> list4 = this.db.tbl_category_associantion.SqlQuery("select distinct * from tbl_category_associantion where id_category_tile=" + cid.ToString() + " and id_category_heading=" + tblCategoryHeading.id_category_heading.ToString() + " and status='A' ").ToList<tbl_category_associantion>();
          DisplayCategory displayCategory = new DisplayCategory();
          displayCategory.Heading = tblCategoryHeading.Heading_title;
          displayCategory.Order = tblCategoryHeading.heading_order.ToString();
          List<Category> categoryList = new List<Category>();
          foreach (tbl_category_associantion categoryAssociantion in list4)
          {
            Category categoryValue = new CategoryModel().GetCategoryValue(Convert.ToInt32((object) categoryAssociantion.id_category));
            categoryValue.ORDERID = Convert.ToInt32((object) categoryAssociantion.category_order);
            categoryValue.CategoryHeader = categoryValue.CategoryName;
            categoryList.Add(categoryValue);
          }
          List<tbl_category> list5 = this.db.tbl_category.SqlQuery("select distinct * from tbl_category where id_category in (select distinct id_category from tbl_content_program_mapping where id_category_tile=" + cid.ToString() + " and id_category_heading =" + tblCategoryHeading.id_category_heading.ToString() + " and " + str3 + ")").ToList<tbl_category>();
          int num = 1;
          foreach (tbl_category tblCategory in list5)
          {
            Category categoryValue = new CategoryModel().GetCategoryValue(Convert.ToInt32(tblCategory.ID_CATEGORY));
            categoryValue.ORDERID = num;
            categoryValue.CategoryHeader = categoryValue.CategoryName;
            categoryList.Add(categoryValue);
            ++num;
          }
          displayCategory.Categories = categoryList;
          displayCategoryList.Add(displayCategory);
        }
      }
      return displayCategoryList != null ? namespace2.CreateResponse<List<DisplayCategory>>(this.Request, HttpStatusCode.OK, displayCategoryList) : namespace2.CreateResponse<List<DisplayCategory>>(this.Request, HttpStatusCode.NoContent, displayCategoryList);
    }
  }
}
