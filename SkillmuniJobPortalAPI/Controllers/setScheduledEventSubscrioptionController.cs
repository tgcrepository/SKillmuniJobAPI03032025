// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.setScheduledEventSubscrioptionController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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
    public class setScheduledEventSubscrioptionController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] EventSubscription USER)
    {
      APIRESPONSE apiresponse1 = new APIRESPONSE();
      tbl_scheduled_event_subscription_log item = this.db.tbl_scheduled_event_subscription_log.Where<tbl_scheduled_event_subscription_log>((Expression<Func<tbl_scheduled_event_subscription_log, bool>>) (t => t.id_scheduled_event == (int?) USER.EID && t.id_user == (int?) USER.UID)).FirstOrDefault<tbl_scheduled_event_subscription_log>();
      tbl_user uData = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == USER.UID && t.STATUS == "A")).FirstOrDefault<tbl_user>();
      if (uData == null)
      {
        apiresponse1.KEY = "FAILURE";
        apiresponse1.MESSAGE = "Could not Find the User.";
        return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse1);
      }
      if (item != null)
      {
        tbl_scheduled_event iEvent = this.db.tbl_scheduled_event.Where<tbl_scheduled_event>((Expression<Func<tbl_scheduled_event, bool>>) (t => (int?) t.id_scheduled_event == item.id_scheduled_event && t.status == "A")).FirstOrDefault<tbl_scheduled_event>();
        int num1 = this.db.tbl_scheduled_event_subscription_log.Where<tbl_scheduled_event_subscription_log>((Expression<Func<tbl_scheduled_event_subscription_log, bool>>) (t => t.id_scheduled_event == (int?) USER.EID)).Count<tbl_scheduled_event_subscription_log>((Expression<Func<tbl_scheduled_event_subscription_log, bool>>) (t => t.subscription_status == "A"));
        if (iEvent != null)
        {
          item.event_user_response = new int?(USER.OPT);
          item.event_user_response_timestamp = new DateTime?(DateTime.Now);
          int? nullable = iEvent.event_type;
          int num2 = 1;
          int num3;
          if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
          {
            string[] strArray1 = iEvent.participant_level.Split(',');
            for (int index = 0; index < strArray1.Length; ++index)
              strArray1[index] = "'" + strArray1[index].ToUpper().Trim() + "'";
            string str1 = string.Join(",", strArray1);
            string[] strArray2 = new string[7];
            strArray2[0] = "select * from tbl_user where id_organization=";
            num3 = USER.OID;
            strArray2[1] = num3.ToString();
            strArray2[2] = " AND  id_user=";
            num3 = USER.UID;
            strArray2[3] = num3.ToString();
            strArray2[4] = " AND status='A' AND upper(user_designation) in (";
            strArray2[5] = str1;
            strArray2[6] = ")";
            if (this.db.tbl_user.SqlQuery(string.Concat(strArray2)).FirstOrDefault<tbl_user>() == null)
            {
              apiresponse1.KEY = "FAILURE";
              apiresponse1.MESSAGE = "Your Designation does not match with Participant Level...";
              return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse1);
            }
            if (USER.OPT == 1)
            {
              nullable = iEvent.no_of_participants;
              num3 = num1;
              if (nullable.GetValueOrDefault() <= num3 & nullable.HasValue)
              {
                item.subscription_status = "P";
                this.db.SaveChanges();
                apiresponse1.KEY = "FAILURE";
                APIRESPONSE apiresponse2 = apiresponse1;
                nullable = iEvent.no_of_participants;
                string str2 = "Participant limit " + nullable.ToString() + " has already reached.Cannot Subscribe to the content.";
                apiresponse2.MESSAGE = str2;
                return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse1);
              }
              if (iEvent.is_approval == "1")
              {
                item.subscription_status = "P";
                item.event_user_comment = USER.COMMENT;
                new SendApprovalMail().sendApporovalmail(uData, iEvent, item);
              }
              else
              {
                item.subscription_status = "A";
                item.event_user_comment = USER.COMMENT;
              }
            }
            else
            {
              item.subscription_status = "R";
              item.event_user_comment = USER.COMMENT;
            }
            this.db.SaveChanges();
          }
          nullable = iEvent.event_type;
          num3 = 2;
          if (nullable.GetValueOrDefault() == num3 & nullable.HasValue)
          {
            if (USER.OPT == 1)
            {
              item.subscription_status = "A";
              item.event_user_comment = USER.COMMENT;
            }
            else
            {
              item.subscription_status = "R";
              item.event_user_comment = USER.COMMENT;
            }
            this.db.SaveChanges();
          }
        }
      }
      apiresponse1.KEY = "SUCCESS";
      apiresponse1.MESSAGE = "SUCCESS";
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse1);
    }
  }
}
