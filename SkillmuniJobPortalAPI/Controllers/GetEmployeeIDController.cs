﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.GetEmployeeIDController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
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

    public class GetEmployeeIDController : ApiController
  {
    public HttpResponseMessage Get(int uid)
    {
      tbl_user tblUser = new db_m2ostEntities().tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == uid)).FirstOrDefault<tbl_user>();
      return tblUser == null ? namespace2.CreateResponse<string>(this.Request, HttpStatusCode.Unauthorized, "") : namespace2.CreateResponse<string>(this.Request, HttpStatusCode.OK, tblUser.EMPLOYEEID);
    }
  }
}
