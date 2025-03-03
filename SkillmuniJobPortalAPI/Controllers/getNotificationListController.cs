// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getNotificationListController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    public class getNotificationListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(int userid, int orgid)
    {
      APIRESPONSE apiresponse = new APIRESPONSE();
      NotificationList notificationList1 = new NotificationList();
      DateTime now = DateTime.Now;
      tbl_user user = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.ID_USER == userid)).FirstOrDefault<tbl_user>();
      if (user != null)
      {
        List<tbl_notification_config> notificationConfigList1 = new List<tbl_notification_config>();
        List<tbl_notification_config> list1 = this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_user == (int?) user.ID_USER && t.status == "A")).ToList<tbl_notification_config>();
        if (list1 != null)
        {
          List<tbl_notification_config> list2 = list1.OrderByDescending<tbl_notification_config, DateTime>((Func<tbl_notification_config, DateTime>) (c => c.updated_date_time.Value.Date)).ThenBy<tbl_notification_config, TimeSpan>((Func<tbl_notification_config, TimeSpan>) (c => c.updated_date_time.Value.TimeOfDay)).ToList<tbl_notification_config>();
          List<Notification> notificationList2 = new List<Notification>();
          foreach (tbl_notification_config notificationConfig in list2)
          {
            tbl_notification_config item = notificationConfig;
            tbl_notification tblNotification = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => (int?) t.id_notification == item.id_notification)).FirstOrDefault<tbl_notification>();
            if (tblNotification != null)
            {
              Notification notification = new Notification();
              notification.NOTIFICATION_ID = tblNotification.id_notification;
              if (tblNotification.notification_type == 1)
                notification.NOTIFICATION_TYPE = "Generic Notification";
              else if (tblNotification.notification_type == 2)
                notification.NOTIFICATION_TYPE = "Event Based Motification";
              else if (tblNotification.notification_type == 3)
                notification.NOTIFICATION_TYPE = "Content Specific Notification";
              else if (tblNotification.notification_type == 4)
                notification.NOTIFICATION_TYPE = "Reminder Notification";
              else if (tblNotification.notification_type == 5)
                notification.NOTIFICATION_TYPE = "Generic Notification with Content";
              else if (tblNotification.notification_type == 7)
                notification.NOTIFICATION_TYPE = "Generic Notification with Assessment";
              if (item.notification_action_type == "CON")
              {
                tbl_content_user_assisgnment userAss = this.db.tbl_content_user_assisgnment.Where<tbl_content_user_assisgnment>((Expression<Func<tbl_content_user_assisgnment, bool>>) (t => t.id_content == item.id_content && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_user_assisgnment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => (int?) t.ID_CONTENT == userAss.id_content)).FirstOrDefault<tbl_content>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblContent.CONTENT_QUESTION;
                }
              }
              else if (item.notification_action_type == "PRO")
              {
                tbl_content_program_mapping userAss = this.db.tbl_content_program_mapping.Where<tbl_content_program_mapping>((Expression<Func<tbl_content_program_mapping, bool>>) (t => t.id_category == item.id_category && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_program_mapping>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == userAss.id_category)).FirstOrDefault<tbl_category>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblCategory.CATEGORYNAME;
                }
              }
              else if (item.notification_action_type == "ASS")
              {
                tbl_assessment_user_assignment userAss = this.db.tbl_assessment_user_assignment.Where<tbl_assessment_user_assignment>((Expression<Func<tbl_assessment_user_assignment, bool>>) (t => t.id_assessment == item.id_assessment && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_user_assignment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expire_date.Value.ToString("dd-MM-yyyy");
                  tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == userAss.id_assessment)).FirstOrDefault<tbl_assessment>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblAssessment.assessment_title;
                }
              }
              else if (item.notification_action_type == "GENCON")
              {
                tbl_content_user_assisgnment userAss = this.db.tbl_content_user_assisgnment.Where<tbl_content_user_assisgnment>((Expression<Func<tbl_content_user_assisgnment, bool>>) (t => t.id_content == item.id_content && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_user_assisgnment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => (int?) t.ID_CONTENT == userAss.id_content)).FirstOrDefault<tbl_content>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblContent.CONTENT_QUESTION;
                }
              }
              else if (item.notification_action_type == "GENASS")
              {
                tbl_assessment_user_assignment userAss = this.db.tbl_assessment_user_assignment.Where<tbl_assessment_user_assignment>((Expression<Func<tbl_assessment_user_assignment, bool>>) (t => t.id_assessment == item.id_assessment && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_user_assignment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expire_date.Value.ToString("dd-MM-yyyy");
                  tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == userAss.id_assessment)).FirstOrDefault<tbl_assessment>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblAssessment.assessment_title;
                }
              }
              else
              {
                notification.EXPIRYDATE = "";
                notification.NOTIFICATION_MESSAGE = tblNotification.notification_message;
              }
              notification.SENTDATE = item.updated_date_time.Value.ToString("dd-MM-yyyy");
              notification.NOTIFICATION_CONFIG_ID = item.id_notification_config;
              notification.NOTIFICATION_KEY = tblNotification.notification_key;
              notification.NOTIFICATION_TITLE = tblNotification.notification_name;
              notification.NOTIFICATION_DESCRIPTION = tblNotification.notification_description;
              notification.START_DATE = tblNotification.start_date.Value.ToShortDateString();
              notification.END_DATE = tblNotification.end_date.Value.ToShortDateString();
              notificationList2.Add(notification);
            }
          }
          notificationList1.UNREAD = notificationList2;
        }
        List<tbl_notification_config> notificationConfigList2 = new List<tbl_notification_config>();
        List<tbl_notification_config> list3 = this.db.tbl_notification_config.Where<tbl_notification_config>((Expression<Func<tbl_notification_config, bool>>) (t => t.id_user == (int?) user.ID_USER && t.status == "R")).ToList<tbl_notification_config>();
        if (list3 != null)
        {
          List<tbl_notification_config> list4 = list3.OrderByDescending<tbl_notification_config, DateTime>((Func<tbl_notification_config, DateTime>) (c => c.updated_date_time.Value.Date)).ThenBy<tbl_notification_config, TimeSpan>((Func<tbl_notification_config, TimeSpan>) (c => c.updated_date_time.Value.TimeOfDay)).ToList<tbl_notification_config>();
          List<Notification> notificationList3 = new List<Notification>();
          foreach (tbl_notification_config notificationConfig in list4)
          {
            tbl_notification_config item = notificationConfig;
            tbl_notification tblNotification = this.db.tbl_notification.Where<tbl_notification>((Expression<Func<tbl_notification, bool>>) (t => (int?) t.id_notification == item.id_notification)).FirstOrDefault<tbl_notification>();
            if (tblNotification != null)
            {
              Notification notification = new Notification();
              notification.NOTIFICATION_ID = tblNotification.id_notification;
              if (tblNotification.notification_type == 1)
                notification.NOTIFICATION_TYPE = "Generic Notification";
              else if (tblNotification.notification_type == 2)
                notification.NOTIFICATION_TYPE = "Event Based Motification";
              else if (tblNotification.notification_type == 3)
                notification.NOTIFICATION_TYPE = "Content Specific Notification";
              else if (tblNotification.notification_type == 4)
                notification.NOTIFICATION_TYPE = "Reminder Notification";
              else if (tblNotification.notification_type == 5)
                notification.NOTIFICATION_TYPE = "Generic Notification with Content";
              else if (tblNotification.notification_type == 7)
                notification.NOTIFICATION_TYPE = "Generic Notification with Assessment";
              if (item.notification_action_type == "CON")
              {
                tbl_content_user_assisgnment userAss = this.db.tbl_content_user_assisgnment.Where<tbl_content_user_assisgnment>((Expression<Func<tbl_content_user_assisgnment, bool>>) (t => t.id_content == item.id_content && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_user_assisgnment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => (int?) t.ID_CONTENT == userAss.id_content)).FirstOrDefault<tbl_content>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblContent.CONTENT_QUESTION;
                }
              }
              else if (item.notification_action_type == "PRO")
              {
                tbl_content_program_mapping userAss = this.db.tbl_content_program_mapping.Where<tbl_content_program_mapping>((Expression<Func<tbl_content_program_mapping, bool>>) (t => t.id_category == item.id_category && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_program_mapping>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == userAss.id_category)).FirstOrDefault<tbl_category>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblCategory.CATEGORYNAME;
                }
              }
              else if (item.notification_action_type == "ASS")
              {
                tbl_assessment_user_assignment userAss = this.db.tbl_assessment_user_assignment.Where<tbl_assessment_user_assignment>((Expression<Func<tbl_assessment_user_assignment, bool>>) (t => t.id_assessment == item.id_assessment && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_user_assignment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expire_date.Value.ToString("dd-MM-yyyy");
                  tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == userAss.id_assessment)).FirstOrDefault<tbl_assessment>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblAssessment.assessment_title;
                }
              }
              else if (item.notification_action_type == "GENCON")
              {
                tbl_content_user_assisgnment userAss = this.db.tbl_content_user_assisgnment.Where<tbl_content_user_assisgnment>((Expression<Func<tbl_content_user_assisgnment, bool>>) (t => t.id_content == item.id_content && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_content_user_assisgnment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expiry_date.Value.ToString("dd-MM-yyyy");
                  tbl_content tblContent = this.db.tbl_content.Where<tbl_content>((Expression<Func<tbl_content, bool>>) (t => (int?) t.ID_CONTENT == userAss.id_content)).FirstOrDefault<tbl_content>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblContent.CONTENT_QUESTION;
                }
              }
              else if (item.notification_action_type == "GENASS")
              {
                tbl_assessment_user_assignment userAss = this.db.tbl_assessment_user_assignment.Where<tbl_assessment_user_assignment>((Expression<Func<tbl_assessment_user_assignment, bool>>) (t => t.id_assessment == item.id_assessment && t.id_user == (int?) user.ID_USER && t.id_organization == (int?) orgid)).FirstOrDefault<tbl_assessment_user_assignment>();
                if (userAss != null)
                {
                  notification.EXPIRYDATE = userAss.expire_date.Value.ToString("dd-MM-yyyy");
                  tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == userAss.id_assessment)).FirstOrDefault<tbl_assessment>();
                  notification.NOTIFICATION_MESSAGE = tblNotification.notification_message + " - " + tblAssessment.assessment_title;
                }
              }
              else
              {
                notification.EXPIRYDATE = "";
                notification.NOTIFICATION_MESSAGE = tblNotification.notification_message;
              }
              notification.SENTDATE = item.updated_date_time.Value.ToString("dd-MM-yyyy");
              notification.NOTIFICATION_CONFIG_ID = item.id_notification_config;
              notification.NOTIFICATION_KEY = tblNotification.notification_key;
              notification.NOTIFICATION_TITLE = tblNotification.notification_name;
              notification.NOTIFICATION_DESCRIPTION = tblNotification.notification_description;
              notification.START_DATE = tblNotification.start_date.Value.ToShortDateString();
              notification.END_DATE = tblNotification.end_date.Value.ToShortDateString();
              notificationList3.Add(notification);
            }
          }
          notificationList1.READ = notificationList3;
        }
        apiresponse.KEY = "SUCCESS";
        string str = JsonConvert.SerializeObject((object) notificationList1);
        apiresponse.MESSAGE = str;
      }
      else
      {
        apiresponse.KEY = "FAILURE";
        apiresponse.MESSAGE = "Invalid User";
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
