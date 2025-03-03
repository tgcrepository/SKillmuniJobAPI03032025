// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.OrgGameChangePasswordController
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

    public class OrgGameChangePasswordController : ApiController
  {
    public HttpResponseMessage Post([FromBody] PasswordData PostData)
    {
      Response response = new Response();
      try
      {
        tbl_sul_fest_otp tblSulFestOtp = new tbl_sul_fest_otp();
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          tbl_user tblUser = m2ostnextserviceDbContext.Database.SqlQuery<tbl_user>("select * from tbl_user where USERID={0}", (object) PostData.UserID).FirstOrDefault<tbl_user>();
          if (tblUser != null)
          {
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user set PASSWORD={0} where ID_USER={1}", (object) PostData.NewPassword, (object) tblUser.ID_USER);
            m2ostnextserviceDbContext.Database.ExecuteSqlCommand("update  tbl_user set is_first_time_login=0 where ID_USER={0} ", (object) tblUser.ID_USER);
            response.ResponseMessage = "Password updated successfully.";
            response.ResponseCode = "SUCCESS";
          }
          else
          {
            response.ResponseMessage = "Entered User Id is wrong.";
            response.ResponseCode = "FAILED";
          }
        }
      }
      catch (Exception ex)
      {
        response.ResponseCode = "FAILED";
        response.ResponseMessage = "Something went wrong please try after some time.Or else please contact admin.";
        return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
      }
      return namespace2.CreateResponse<Response>(this.Request, HttpStatusCode.OK, response);
    }
  }
}
