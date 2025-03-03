// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.UpdateCategoryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class UpdateCategoryController : ApiController
  {
    public HttpResponseMessage Get(string ctList, int orgID, int uid)
    {
      List<CategoryResponce> categoryResponceList1 = new List<CategoryResponce>();
      if (string.IsNullOrEmpty(ctList))
        return namespace2.CreateResponse<List<CategoryResponce>>(this.Request, HttpStatusCode.NoContent, categoryResponceList1);
      string[] strArray = ctList.Split('|');
      string str1 = string.Join(",", strArray);
      foreach (string str2 in strArray)
      {
        if (!string.IsNullOrEmpty(str2))
        {
          CategoryResponce categoryResponce = new CategoryModel().GetCategory(str2);
          if (categoryResponce != null)
          {
            categoryResponce.Status = "safe";
          }
          else
          {
            categoryResponce = new CategoryResponce();
            categoryResponce.CategoryID = Convert.ToInt32(str2);
            categoryResponce.Status = "false";
          }
          categoryResponceList1.Add(categoryResponce);
        }
      }
      List<CategoryResponce> categoryResponceList2 = new List<CategoryResponce>();
      List<CategoryResponce> newCategory = new CategoryModel().GetNewCategory(str1, orgID.ToString());
      if (newCategory.Count > 0)
      {
        foreach (CategoryResponce categoryResponce in newCategory)
        {
          categoryResponce.Status = "true";
          categoryResponceList1.Add(categoryResponce);
        }
      }
      return namespace2.CreateResponse<List<CategoryResponce>>(this.Request, HttpStatusCode.OK, categoryResponceList1);
    }
  }
}
