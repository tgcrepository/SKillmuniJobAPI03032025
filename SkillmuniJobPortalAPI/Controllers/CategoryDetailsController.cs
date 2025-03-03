// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CategoryDetailsController
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

    public class CategoryDetailsController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string category, string organization)
    {
      CateogryDetails cateogryDetails = new CateogryDetails();
      List<Category> categoryList = new List<Category>();
      tbl_category_associantion categoryAssociantion = this.db.tbl_category_associantion.Find(new object[1]
      {
        (object) Convert.ToInt32(category)
      });
      tbl_category tblCategory = this.db.tbl_category.Find(new object[1]
      {
        (object) categoryAssociantion.id_category
      });
      tbl_category_heading tblCategoryHeading = this.db.tbl_category_heading.Find(new object[1]
      {
        (object) categoryAssociantion.id_category_heading
      });
      if (tblCategory == null)
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.Unauthorized, "");
      Category category1 = new Category()
      {
        CategoryID = tblCategory.ID_CATEGORY,
        CategoryName = tblCategory.CATEGORYNAME,
        CategoryDescription = tblCategory.DESCRIPTION,
        OrganisationId = tblCategory.ID_ORGANIZATION
      };
      category1.CategoryImagePath = ConfigurationManager.AppSettings["CATIMAGE"].ToString() + category1.OrganisationId.ToString() + "/" + tblCategory.IMAGE_PATH;
      category1.Is_Primary = 0;
      category1.CategoryHeader = tblCategoryHeading.Heading_title;
      category1.SubCount = 0;
      category1.ORDERID = Convert.ToInt32((object) categoryAssociantion.category_order);
      categoryList.Add(category1);
      List<tbl_content> tblContentList = new List<tbl_content>();
      List<tbl_content> list1 = this.db.tbl_content.SqlQuery("SELECT * FROM tbl_content WHERE STATUS='A' AND ID_CATEGORY=" + tblCategory.ID_CATEGORY.ToString() + "  ORDER BY CONTENT_QUESTION  LIMIT 15 ").ToList<tbl_content>();
      List<SearchResponce> source = new List<SearchResponce>();
      foreach (tbl_content tblContent in list1)
        source.Add(new SearchResponce()
        {
          CONTENT_QUESTION = tblContent.CONTENT_QUESTION,
          ID_CONTENT = tblContent.ID_CONTENT,
          ID_CONTENT_LEVEL = tblContent.ID_CONTENT_LEVEL,
          EXPIRYDATE = tblContent.EXPIRY_DATE.Value.ToString("dd-MM-yyyy")
        });
      List<SearchResponce> list2 = source.OrderBy<SearchResponce, int>((Func<SearchResponce, int>) (t => t.ID_CONTENT_LEVEL)).ThenBy<SearchResponce, string>((Func<SearchResponce, string>) (t => t.CONTENT_QUESTION)).ToList<SearchResponce>();
      cateogryDetails.Categories = categoryList;
      cateogryDetails.Contents = list2;
      cateogryDetails.Order = "0";
      cateogryDetails.Heading = "";
      return namespace2.CreateResponse<CateogryDetails>(this.Request, HttpStatusCode.OK, cateogryDetails);
    }
  }
}
