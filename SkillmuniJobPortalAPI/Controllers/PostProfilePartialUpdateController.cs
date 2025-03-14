﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.PostProfilePartialUpdateController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace m2ostnextservice.Controllers
{
    extern alias alias1;
    extern alias alias2;

    using namespace1 = alias1::System.Net.Http.HttpRequestMessageExtensions;
    using namespace2 = alias2::System.Net.Http.HttpRequestMessageExtensions;

    public class PostProfilePartialUpdateController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] PartialProfileUpdate obj)
    {
      this.ControllerContext.RouteData.Values["controller"].ToString();
      PartialProfileUpdate partialProfileUpdate = new PartialProfileUpdate();
      try
      {
        using (db_m2ostEntities dbM2ostEntities = new db_m2ostEntities())
          dbM2ostEntities.Database.ExecuteSqlCommand("update tbl_profile set FIRSTNAME={0}, EMAIL={1},MOBILE={2},STUDENT={3} where ID_USER={4}", (object) obj.FIRSTNAME, (object) obj.MAILID, (object) obj.MOBILENO, (object) obj.STUDENT, (object) obj.ID_USER);
      }
      catch (Exception ex)
      {
        return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Failed");
      }
      return namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, "Success");
    }
  }
}
