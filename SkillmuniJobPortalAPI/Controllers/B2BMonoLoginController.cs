// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.B2BMonoLoginController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
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

    public class B2BMonoLoginController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] MonoLogin user)
    {
      APIRESPONSE apiresponse = new APIRESPONSE();
      try
      {
        tbl_user tblUser = this.db.tbl_user.SqlQuery(" select * from tbl_user where id_user='" + user.UID + "' and id_organization='" + user.OID + "'   limit 1").FirstOrDefault<tbl_user>();
        if (tblUser != null)
        {
          if (tblUser.STATUS == "A")
          {
            tbl_report_login_log tblReportLoginLog = this.db.tbl_report_login_log.SqlQuery(" select * from tbl_report_login_log where id_user='" + user.UID + "' and id_organization='" + user.OID + "' and IMEI not like 'WEBSITE' order by id_report_login_log desc limit 1").FirstOrDefault<tbl_report_login_log>();
            if (tblReportLoginLog != null)
            {
              if (tblReportLoginLog.IMEI == user.IMEI)
              {
                apiresponse.KEY = "SUCCESS";
                apiresponse.MESSAGE = "SUCCESS";
              }
              else
              {
                apiresponse.KEY = "FAILURE";
                apiresponse.MESSAGE = "You are Logged in with some other device , please login again to use in this device.";
              }
            }
            else
            {
              apiresponse.KEY = "SUCCESS";
              apiresponse.MESSAGE = "SUCCESS";
            }
          }
          else
          {
            apiresponse.KEY = "FAILURE";
            apiresponse.MESSAGE = "User account is deactivated. Please contact your administrator";
          }
        }
        else
        {
          apiresponse.KEY = "FAILURE";
          apiresponse.MESSAGE = "'User account is invalid. Please contact your administrator";
        }
      }
      catch (Exception ex)
      {
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
