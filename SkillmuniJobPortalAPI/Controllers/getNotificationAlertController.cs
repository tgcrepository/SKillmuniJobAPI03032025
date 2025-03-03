// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getNotificationAlertController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
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
    public class getNotificationAlertController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int configid, int userid, int orgid)
    {
      APIRESPONSE apiresponse = new APIRESPONSE();
      NotificationAlert notificationAlert1 = new NotificationAlert();
      tbl_user user = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == userid)).FirstOrDefault<tbl_user>();
      if (user != null)
      {
        tbl_notification_config config = new tbl_notification_config();
        config = this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_user == (int?) user.ID_USER && t.id_notification_config == configid)).FirstOrDefault<tbl_notification_config>();
        if (config != null)
        {
          if (config.status == "A")
          {
            config.status = "R";
            config.read_timestamp = new DateTime?(DateTime.Now);
            this.db.SaveChanges();
          }
          tbl_notification tblNotification = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => (int?) t.id_notification == config.id_notification)).FirstOrDefault<tbl_notification>();
          if (tblNotification != null)
          {
            NotificationAlert notificationAlert2 = new NotificationAlert();
            notificationAlert2.NOTIFICATION_ID = tblNotification.id_notification;
            notificationAlert2.NOTIFICATION_KEY = tblNotification.notification_key;
            notificationAlert2.NOTIFICATION_TITLE = tblNotification.notification_name;
            notificationAlert2.NOTIFICATION_DESCRIPTION = tblNotification.notification_description;
            notificationAlert2.NOTIFICATION_MESSAGE = tblNotification.notification_message;
            notificationAlert2.START_DATE = tblNotification.start_date.Value.ToShortDateString();
            notificationAlert2.END_DATE = tblNotification.end_date.Value.ToShortDateString();
            if (config.notification_action_type == "CON")
            {
              notificationAlert2.ACTION_TYPE = "CON";
              notificationAlert2.NOTIFICATION_TYPE = "Content Specific Notification - Content";
              notificationAlert2.REDIRECTION_URL = "api/GetContentDetails?conId=" + config.id_content.ToString() + "&userid=" + config.id_user.ToString() + "&orgid=" + orgid.ToString();
            }
            else if (config.notification_action_type == "PRO")
            {
              notificationAlert2.ACTION_TYPE = "PRO";
              notificationAlert2.NOTIFICATION_TYPE = "Content Specific Notification - Program";
              notificationAlert2.REDIRECTION_URL = "api/getCategoryFromNotification?catid=" + config.id_category.ToString() + "&userid=" + config.id_user.ToString() + "&orgid=" + orgid.ToString();
            }
            else if (config.notification_action_type == "ASS")
            {
              notificationAlert2.ACTION_TYPE = "ASS";
              notificationAlert2.NOTIFICATION_TYPE = "Content Specific Notification - Assessment";
              notificationAlert2.REDIRECTION_URL = "api/Assessmentsheet?ASID=" + config.id_assessment.ToString() + "&UID=" + config.id_user.ToString() + "&OID=" + orgid.ToString();
            }
            else if (config.notification_action_type == "GEN")
            {
              notificationAlert2.ACTION_TYPE = "GEN";
              notificationAlert2.NOTIFICATION_TYPE = "Generic Notification";
              notificationAlert2.REDIRECTION_URL = "NA";
            }
            else if (config.notification_action_type == "GENCON")
            {
              notificationAlert2.ACTION_TYPE = "GENCON";
              notificationAlert2.NOTIFICATION_TYPE = "Generic Notification With Content";
              notificationAlert2.REDIRECTION_URL = "api/GetContentDetails?conId=" + config.id_content.ToString() + "&userid=" + config.id_user.ToString() + "&orgid=" + orgid.ToString();
            }
            else if (config.notification_action_type == "GENASS")
            {
              notificationAlert2.ACTION_TYPE = "GENASS";
              notificationAlert2.NOTIFICATION_TYPE = "Generic Notification with Assessment";
              notificationAlert2.REDIRECTION_URL = "api/Assessmentsheet?ASID=" + config.id_assessment.ToString() + "&UID=" + config.id_user.ToString() + "&OID=" + orgid.ToString();
            }
            apiresponse.KEY = "SUCCESS";
            string str = JsonConvert.SerializeObject((object) notificationAlert2);
            apiresponse.MESSAGE = str;
          }
          else
          {
            apiresponse.KEY = "FAILURE";
            apiresponse.MESSAGE = "Invalid Notification";
          }
        }
        else
        {
          apiresponse.KEY = "FAILURE";
          apiresponse.MESSAGE = "Invalid Notification / Notification Expired ";
        }
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
