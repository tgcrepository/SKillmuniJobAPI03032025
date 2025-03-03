// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.AuthenticateBriefTileController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
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

    public class AuthenticateBriefTileController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int id_academy)
    {
      AuthenticateBrief authenticateBrief = new AuthenticateBrief();
      authenticateBrief.AuthFlag = "0";
      string str = "0";
      tbl_academy_level_brief_restriction briefRestriction1 = new tbl_academy_level_brief_restriction();
      List<tbl_restriction_user_log> restrictionUserLogList = new List<tbl_restriction_user_log>();
      using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
      {
        tbl_academy_level_brief_restriction briefRestriction2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_academy_level_brief_restriction>("select * from tbl_academy_level_brief_restriction where id_academy = {0}", (object) id_academy).FirstOrDefault<tbl_academy_level_brief_restriction>();
        if (briefRestriction2 != null)
        {
          if (briefRestriction2.time == 1)
          {
            DateTime date = DateTime.Now.Date;
            if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_restriction_user_log>("select * from tbl_restriction_user_log where UID = {0} and id_academy={1} and date(updated_date_time)={2}", (object) UID, (object) id_academy, (object) date).ToList<tbl_restriction_user_log>().Count < briefRestriction2.brief_count)
            {
              str = "1";
              authenticateBrief.AuthFlag = "1";
            }
            else
              authenticateBrief.Message = "Please comeback tommorrow to read more.";
          }
          else if (briefRestriction2.time == 2)
          {
            int hour = DateTime.Now.Hour;
            DateTime date = DateTime.Now.Date;
            if (m2ostnextserviceDbContext.Database.SqlQuery<tbl_restriction_user_log>("SELECT * FROM tbl_restriction_user_log where UID = {0} and id_academy={1} and EXTRACT(HOUR  FROM updated_date_time)={2} and  date(updated_date_time)={3}", (object) UID, (object) id_academy, (object) hour, (object) date).ToList<tbl_restriction_user_log>().Count < briefRestriction2.brief_count)
            {
              str = "1";
              authenticateBrief.AuthFlag = "1";
            }
            else
            {
              int num1 = DateTime.Now.Hour + 1;
              if (num1 >= 12)
              {
                int num2 = num1 - 12;
                authenticateBrief.Message = num1 != 24 ? "Please comeback after  1 hour to read more." : "Please comeback after 1 hour to read more.";
              }
              else
                authenticateBrief.Message = "Please comeback after 1 hour to read more.";
            }
          }
        }
        else
        {
          authenticateBrief.AuthFlag = "1";
          str = "1";
        }
      }
      return namespace2.CreateResponse<AuthenticateBrief>(this.Request, HttpStatusCode.OK, authenticateBrief);
    }
  }
}
