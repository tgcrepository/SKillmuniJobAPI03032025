// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getScheduledEventListController
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

    public class getScheduledEventListController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] EventUser USER)
    {
      DateTime now = DateTime.Now;
      APIRESPONSE apiresponse = new APIRESPONSE();
      List<EventThumbnail> eventThumbnailList1 = new List<EventThumbnail>();
      List<EventThumbnail> eventThumbnailList2 = new List<EventThumbnail>();
      EventResponse eventResponse = new EventResponse();
      string str1 = "";
      if (USER.DNO != "0")
        str1 = " and event_start_datetime LIKE '" + USER.YNO + "-" + USER.MNO + "-" + USER.DNO + "%'";
      else if (USER.MNO != "0")
        str1 = " and event_start_datetime LIKE '" + USER.YNO + "-" + USER.MNO + "%'";
      string[] strArray = new string[7];
      strArray[0] = "select * from tbl_scheduled_event_subscription_log where id_user=";
      int num = USER.UID;
      strArray[1] = num.ToString();
      strArray[2] = " and id_organization=";
      num = USER.OID;
      strArray[3] = num.ToString();
      strArray[4] = " and id_scheduled_event in (select id_scheduled_event from tbl_scheduled_event where status in ('A','X') ";
      strArray[5] = str1;
      strArray[6] = ")";
      foreach (tbl_scheduled_event_subscription_log eventSubscriptionLog in this.db.tbl_scheduled_event_subscription_log.SqlQuery(string.Concat(strArray)).ToList<tbl_scheduled_event_subscription_log>())
      {
        tbl_scheduled_event_subscription_log item = eventSubscriptionLog;
        tbl_scheduled_event tblScheduledEvent = this.db.tbl_scheduled_event.Where<tbl_scheduled_event>((Expression<Func<tbl_scheduled_event, bool>>) (t => (int?) t.id_scheduled_event == item.id_scheduled_event)).FirstOrDefault<tbl_scheduled_event>();
        if (tblScheduledEvent != null)
        {
          EventThumbnail eventThumbnail1 = new EventThumbnail();
          eventThumbnail1.id_scheduled_event = tblScheduledEvent.id_scheduled_event;
          eventThumbnail1.event_description = tblScheduledEvent.event_description;
          EventThumbnail eventThumbnail2 = eventThumbnail1;
          DateTime? nullable1 = tblScheduledEvent.event_start_datetime;
          string str2 = nullable1.Value.ToString("dd-MM-yyyy HH:mm");
          eventThumbnail2.event_start_datetime = str2;
          eventThumbnail1.event_title = tblScheduledEvent.event_title;
          eventThumbnail1.program_name = tblScheduledEvent.program_name;
          eventThumbnail1.program_objective = tblScheduledEvent.program_objective;
          EventThumbnail eventThumbnail3 = eventThumbnail1;
          nullable1 = tblScheduledEvent.registration_start_date;
          string str3 = nullable1.Value.ToString("dd-MM-yyyy HH:mm");
          eventThumbnail3.registration_start_date = str3;
          EventThumbnail eventThumbnail4 = eventThumbnail1;
          nullable1 = tblScheduledEvent.registration_end_date;
          string str4 = nullable1.Value.ToString("dd-MM-yyyy HH:mm");
          eventThumbnail4.registration_end_date = str4;
          int? nullable2 = tblScheduledEvent.event_type;
          num = 1;
          if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
          {
            eventThumbnail1.event_type = "Open";
          }
          else
          {
            nullable2 = tblScheduledEvent.event_type;
            num = 2;
            if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
              eventThumbnail1.event_type = "Closed";
          }
          eventThumbnail1.attachment_info = "";
          nullable2 = tblScheduledEvent.event_group_type;
          num = 1;
          if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
          {
            eventThumbnail1.event_group_type = "Face to Face";
          }
          else
          {
            nullable2 = tblScheduledEvent.event_group_type;
            num = 2;
            if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
            {
              eventThumbnail1.event_group_type = "Online";
            }
            else
            {
              nullable2 = tblScheduledEvent.event_group_type;
              num = 3;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
              {
                eventThumbnail1.event_group_type = "M2OST";
                nullable2 = tblScheduledEvent.attachment_type;
                num = 1;
                if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                {
                  eventThumbnail1.attachment_info = "Program is attached";
                }
                else
                {
                  nullable2 = tblScheduledEvent.attachment_type;
                  num = 2;
                  if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                    eventThumbnail1.attachment_info = "Attachment is attached";
                }
              }
            }
          }
          if (tblScheduledEvent.status == "X")
          {
            eventThumbnail1.STATUS = "X";
            eventThumbnail1.MESSAGE = "Event has been cancelled.";
            eventThumbnail1.COMMENT = tblScheduledEvent.event_comment;
          }
          else if (item.subscription_status == "A")
          {
            eventThumbnail1.STATUS = "A";
            eventThumbnail1.MESSAGE = "You have subscribed to the event.";
            eventThumbnail1.COMMENT = "";
          }
          else if (item.subscription_status == "R")
          {
            eventThumbnail1.STATUS = "R";
            eventThumbnail1.MESSAGE = "You have declined the invitation to the event.";
            eventThumbnail1.COMMENT = item.event_user_comment;
          }
          else if (item.subscription_status == "C")
          {
            eventThumbnail1.STATUS = "R";
            eventThumbnail1.MESSAGE = "Your manager has rejected your request.";
            eventThumbnail1.COMMENT = item.event_user_comment;
          }
          else if (item.subscription_status == "P")
          {
            DateTime dateTime = now;
            nullable1 = tblScheduledEvent.event_start_datetime;
            if ((nullable1.HasValue ? (dateTime > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              eventThumbnail1.STATUS = "W";
              eventThumbnail1.MESSAGE = "This event has been completed. You are put on waiting List .";
              eventThumbnail1.COMMENT = "";
            }
            else
            {
              eventThumbnail1.STATUS = "P";
              eventThumbnail1.MESSAGE = "Your subscription request is pending for approval.";
              eventThumbnail1.COMMENT = "";
            }
          }
          else if (item.subscription_status == "O")
          {
            DateTime dateTime1 = now;
            nullable1 = tblScheduledEvent.event_start_datetime;
            if ((nullable1.HasValue ? (dateTime1 > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              eventThumbnail1.STATUS = "E";
              eventThumbnail1.MESSAGE = "This event has been completed.";
              eventThumbnail1.COMMENT = "";
            }
            else
            {
              DateTime dateTime2 = now;
              nullable1 = tblScheduledEvent.registration_end_date;
              if ((nullable1.HasValue ? (dateTime2 > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                eventThumbnail1.STATUS = "T";
                eventThumbnail1.MESSAGE = "Registration to the event is closed.";
                eventThumbnail1.COMMENT = "";
              }
              else
              {
                eventThumbnail1.STATUS = "O";
                eventThumbnail1.MESSAGE = "You have not yet subscribed to the event.";
                eventThumbnail1.COMMENT = "";
              }
            }
          }
          else if (item.subscription_status == "L")
          {
            eventThumbnail1.STATUS = "L";
            EventThumbnail eventThumbnail5 = eventThumbnail1;
            nullable2 = tblScheduledEvent.no_of_participants;
            string str5 = "Participant limit of " + nullable2.ToString() + " is already full for the event.";
            eventThumbnail5.MESSAGE = str5;
            eventThumbnail1.COMMENT = "";
          }
          if (item.status == "S")
            eventThumbnailList2.Add(eventThumbnail1);
          else if (item.status == "R")
            eventThumbnailList1.Add(eventThumbnail1);
        }
      }
      eventResponse.READ = eventThumbnailList1;
      eventResponse.UNREAD = eventThumbnailList2;
      apiresponse.KEY = "SUCCESS";
      string str6 = JsonConvert.SerializeObject((object) eventResponse);
      apiresponse.MESSAGE = str6;
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
