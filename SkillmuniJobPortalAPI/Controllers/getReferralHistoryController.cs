// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getReferralHistoryController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
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

    public class getReferralHistoryController : ApiController
  {
    public HttpResponseMessage Get(int UID)
    {
      List<ReferralHistory> referralHistoryList = new List<ReferralHistory>();
      try
      {
        using (JobDbContext jobDbContext = new JobDbContext())
        {
          string str = jobDbContext.Database.SqlQuery<string>("select ref_id from tbl_user where ID_USER={0}", (object) UID).FirstOrDefault<string>();
          Database database = jobDbContext.Database;
          object[] objArray = new object[1]{ (object) str };
          foreach (tbl_referral_code_user_mapping referralCodeUserMapping in database.SqlQuery<tbl_referral_code_user_mapping>("select * from tbl_referral_code_user_mapping where referral_code={0}", objArray).ToList<tbl_referral_code_user_mapping>())
          {
            ReferralHistory referralHistory = new ReferralHistory();
            referralHistory.credit_points = referralCodeUserMapping.referral_points;
            referralHistory.date = referralCodeUserMapping.updated_date_time;
            tbl_profile tblProfile = jobDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) referralCodeUserMapping.id_user).FirstOrDefault<tbl_profile>();
            referralHistory.mobile = tblProfile.MOBILE;
            referralHistory.name = tblProfile.FIRSTNAME + " " + tblProfile.LASTNAME;
            referralHistory.profile_pic = !(tblProfile.PROFILE_IMAGE != "null") || tblProfile.PROFILE_IMAGE == null ? ConfigurationManager.AppSettings["ProfileDefaultBase"].ToString() : tblProfile.PROFILE_IMAGE;
            referralHistoryList.Add(referralHistory);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<List<ReferralHistory>>(this.Request, HttpStatusCode.OK, referralHistoryList);
    }
  }
}
