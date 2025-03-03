// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getTeamMembersController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

    public class getTeamMembersController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int UID, string FLAG, int OID)
    {
      List<TeamMember> teamMemberList = new List<TeamMember>();
      tbl_user user = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == UID)).FirstOrDefault<tbl_user>();
      if (user != null)
      {
        DbSet<tbl_user> tblUser1 = this.db.tbl_user;
        Expression<Func<tbl_user, bool>> predicate = (Expression<Func<tbl_user, bool>>) (t => t.reporting_manager == (int?) user.ID_USER);
        foreach (tbl_user tblUser2 in tblUser1.Where<tbl_user>(predicate).ToList<tbl_user>())
        {
          tbl_user item = tblUser2;
          tbl_profile tblProfile = this.db.tbl_profile.Where<tbl_profile>((Expression<Func<tbl_profile, bool>>) (t => t.ID_USER == item.ID_USER)).FirstOrDefault<tbl_profile>();
          if (tblProfile != null)
            teamMemberList.Add(new TeamMember()
            {
              USERID = item.USERID,
              ID_USER = item.ID_USER.ToString(),
              USERNAME = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME,
              DEPARTMENT = item.user_department,
              DESIGNATION = item.user_designation,
              GRADE = item.user_grade,
              FUNCTION = item.user_function,
              EMPLOYEEID = item.EMPLOYEEID
            });
        }
      }
      return namespace2.CreateResponse<List<TeamMember>>(this.Request, HttpStatusCode.OK, teamMemberList);
    }
  }
}
