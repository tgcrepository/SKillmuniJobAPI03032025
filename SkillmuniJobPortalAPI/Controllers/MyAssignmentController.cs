// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.MyAssignmentController
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

    public class MyAssignmentController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int orgID, int cid, int uid)
    {
      List<AssignmentCategory> assignmentCategoryList = new List<AssignmentCategory>();
      List<DateTime> dateTimeList = new List<DateTime>();
      DateTime dateTime1 = DateTime.Now;
      dateTimeList.Add(dateTime1);
      dateTime1 = dateTime1.AddMonths(1);
      dateTimeList.Add(dateTime1);
      dateTime1 = dateTime1.AddMonths(1);
      dateTimeList.Add(dateTime1);
      DateTime now = DateTime.Now;
      this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == uid)).FirstOrDefault<tbl_user>();
      List<tbl_csst_role> list1 = this.db.tbl_csst_role.SqlQuery("select * from tbl_csst_role where id_csst_role in (select id_csst_role from tbl_role_user_mapping where id_user=" + uid.ToString() + ")").ToList<tbl_csst_role>();
      string str1 = "";
      foreach (tbl_csst_role tblCsstRole in list1)
        str1 = str1 + tblCsstRole.id_csst_role.ToString() + ",";
      str1.TrimEnd(',');
      string str2 = "  id_user=" + uid.ToString() + " ";
      int num1 = 1;
      foreach (DateTime dateTime2 in dateTimeList)
      {
        AssignmentCategory assignmentCategory = new AssignmentCategory();
        assignmentCategory.MONTH = dateTime2.ToString("MMMM-yyyy");
        List<DisplayCategory> source1 = new List<DisplayCategory>();
        List<tbl_category_heading> list2 = this.db.tbl_category_heading.SqlQuery("select * from tbl_category_heading where id_category_heading in (select distinct id_category_heading from tbl_category_associantion where id_category_tile=" + cid.ToString() + " and status='A')").ToList<tbl_category_heading>();
        string[] strArray1 = new string[7]
        {
          "select * from tbl_category_heading where  status='A' and  id_category_heading in (select distinct id_category_heading from tbl_content_program_mapping where MONTH(start_date)=",
          dateTime2.Month.ToString(),
          " and id_category_tile=",
          cid.ToString(),
          " and ",
          str2,
          ")"
        };
        foreach (tbl_category_heading tblCategoryHeading in this.db.tbl_category_heading.SqlQuery(string.Concat(strArray1)).ToList<tbl_category_heading>())
          list2.Add(tblCategoryHeading);
        List<tbl_category_heading> list3 = list2.Distinct<tbl_category_heading>().ToList<tbl_category_heading>();
        if (list3.Count > 0)
        {
          foreach (tbl_category_heading tblCategoryHeading in list3)
          {
            List<Category> categoryList = new List<Category>();
            List<Category> source2 = new List<Category>();
            DisplayCategory displayCategory1 = new DisplayCategory();
            displayCategory1.Heading = tblCategoryHeading.Heading_title;
            DisplayCategory displayCategory2 = displayCategory1;
            int? nullable = tblCategoryHeading.heading_order;
            string str3 = nullable.ToString();
            displayCategory2.Order = str3;
            string[] strArray2 = new string[8];
            strArray2[0] = "select distinct * from tbl_content_program_mapping where  MONTH(start_date)=";
            int num2 = dateTime2.Month;
            strArray2[1] = num2.ToString();
            strArray2[2] = " and id_category_tile=";
            strArray2[3] = cid.ToString();
            strArray2[4] = " and id_category_heading =";
            num2 = tblCategoryHeading.id_category_heading;
            strArray2[5] = num2.ToString();
            strArray2[6] = " and ";
            strArray2[7] = str2;
            List<tbl_content_program_mapping> list4 = this.db.tbl_content_program_mapping.SqlQuery(string.Concat(strArray2)).ToList<tbl_content_program_mapping>();
            int num3 = 1;
            foreach (tbl_content_program_mapping contentProgramMapping in list4)
            {
              tbl_content_program_mapping pItem = contentProgramMapping;
              DateTime? expiryDate = pItem.expiry_date;
              DateTime dateTime3 = expiryDate.Value;
              if (DateTime.Compare(dateTime3.AddDays(1.0), now) > 0)
              {
                tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == pItem.id_category && t.CATEGORY_TYPE == (int?) 0)).FirstOrDefault<tbl_category>();
                if (tblCategory != null)
                {
                  bool flag = true;
                  nullable = tblCategory.CATEGORY_TYPE;
                  int num4 = 0;
                  if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
                  {
                    nullable = tblCategory.CATEGORY_TYPE;
                    int num5 = 1;
                    if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
                    {
                      nullable = tblCategory.CATEGORY_TYPE;
                      int num6 = 2;
                      if (!(nullable.GetValueOrDefault() == num6 & nullable.HasValue))
                        goto label_23;
                    }
                  }
                  flag = new Utility().checkCategoryContentCount(tblCategory.ID_CATEGORY, orgID, uid);
label_23:
                  Category categoryValue = new CategoryModel().GetCategoryValue(Convert.ToInt32(tblCategory.ID_CATEGORY));
                  categoryValue.ORDERID = num3;
                  categoryValue.CategoryHeader = categoryValue.CategoryName;
                  categoryValue.CategoryType = Convert.ToInt32((object) tblCategory.CATEGORY_TYPE);
                  categoryValue.IS_COUNT_REQUIRED = Convert.ToInt32((object) tblCategory.COUNT_REQUIRED);
                  nullable = tblCategory.CATEGORY_TYPE;
                  int num7 = 3;
                  categoryValue.NEXTURL = !(nullable.GetValueOrDefault() == num7 & nullable.HasValue) ? "" : tblCategory.IMAGE_URL;
                  categoryValue.ContentCount = !flag ? 0 : 1;
                  Category category = categoryValue;
                  expiryDate = pItem.expiry_date;
                  dateTime3 = expiryDate.Value;
                  string str4 = dateTime3.ToString("dd-MMM-yyyy");
                  category.ExpiryDate = str4;
                  categoryValue.LINKCOUNT = new ContentModel().getContentLinkCount(Convert.ToInt32(tblCategory.ID_CATEGORY), orgID, uid);
                  source2.Add(categoryValue);
                }
              }
            }
            foreach (Category category in source2.OrderBy<Category, string>((Func<Category, string>) (t => t.CategoryName)).ToList<Category>())
            {
              category.ORDERID = num3;
              ++num3;
              categoryList.Add(category);
            }
            displayCategory1.Categories = categoryList;
            source1.Add(displayCategory1);
          }
        }
        List<DisplayCategory> list5 = source1.OrderBy<DisplayCategory, string>((Func<DisplayCategory, string>) (t => t.Order)).ToList<DisplayCategory>();
        assignmentCategory.assigment = list5;
        assignmentCategory.ORDER = num1;
        assignmentCategoryList.Add(assignmentCategory);
        ++num1;
      }
      return namespace2.CreateResponse<List<AssignmentCategory>>(this.Request, HttpStatusCode.OK, assignmentCategoryList);
    }
  }
}
