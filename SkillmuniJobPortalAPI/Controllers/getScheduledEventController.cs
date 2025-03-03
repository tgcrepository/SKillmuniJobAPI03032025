// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getScheduledEventController
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

    public class getScheduledEventController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Post([FromBody] EventData USER)
    {
      DateTime now = DateTime.Now;
      APIRESPONSE apiresponse = new APIRESPONSE();
      ScheduledEvent scheduledEvent1 = new ScheduledEvent();
      tbl_scheduled_event_subscription_log item = this.db.tbl_scheduled_event_subscription_log.Where<tbl_scheduled_event_subscription_log>((Expression<Func<tbl_scheduled_event_subscription_log, bool>>) (t => t.id_scheduled_event == (int?) USER.EID && t.id_user == (int?) USER.UID)).FirstOrDefault<tbl_scheduled_event_subscription_log>();
      if (item != null)
      {
        tbl_scheduled_event iEvent = this.db.tbl_scheduled_event.Where<tbl_scheduled_event>((Expression<Func<tbl_scheduled_event, bool>>) (t => (int?) t.id_scheduled_event == item.id_scheduled_event && t.status != "P")).FirstOrDefault<tbl_scheduled_event>();
        if (iEvent != null)
        {
          item.status = "R";
          this.db.SaveChanges();
          scheduledEvent1.id_scheduled_event = iEvent.id_scheduled_event;
          scheduledEvent1.event_description = iEvent.event_description;
          scheduledEvent1.event_duration = iEvent.event_duration;
          scheduledEvent1.event_start_datetime = iEvent.event_start_datetime.Value.ToString("dd-MM-yyyy HH:mm");
          scheduledEvent1.event_title = iEvent.event_title;
          int? eventGroupType = iEvent.event_group_type;
          int num1 = 1;
          int? nullable1;
          if (eventGroupType.GetValueOrDefault() == num1 & eventGroupType.HasValue)
          {
            scheduledEvent1.program_description = iEvent.event_additional_info;
          }
          else
          {
            nullable1 = iEvent.event_group_type;
            int num2 = 2;
            if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
            {
              scheduledEvent1.REDIRECTION_URL = iEvent.event_online_url;
            }
            else
            {
              nullable1 = iEvent.event_group_type;
              int num3 = 3;
              if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
              {
                string str = "";
                nullable1 = iEvent.attachment_type;
                int num4 = 1;
                if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
                {
                  tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == iEvent.id_program && t.STATUS == "A" && t.CATEGORY_TYPE == (int?) 0)).FirstOrDefault<tbl_category>();
                  tbl_category_tiles tblCategoryTiles = this.db.tbl_category_tiles.Where<tbl_category_tiles>((Expression<Func<tbl_category_tiles, bool>>) (t => (int?) t.id_category_tiles == iEvent.id_category_tile && t.status == "A")).FirstOrDefault<tbl_category_tiles>();
                  tbl_category_heading tblCategoryHeading = this.db.tbl_category_heading.Where<tbl_category_heading>((Expression<Func<tbl_category_heading, bool>>) (t => (int?) t.id_category_heading == iEvent.id_category_heading && t.status == "A")).FirstOrDefault<tbl_category_heading>();
                  if (tblCategory != null)
                    str = tblCategory.CATEGORYNAME + " [Tile : " + tblCategoryTiles.tile_heading + " , Heading : " + tblCategoryHeading.Heading_title + " ]";
                }
                else
                {
                  nullable1 = iEvent.attachment_type;
                  int num5 = 2;
                  if (nullable1.GetValueOrDefault() == num5 & nullable1.HasValue)
                  {
                    tbl_assessment tblAssessment = this.db.tbl_assessment.Where<tbl_assessment>((Expression<Func<tbl_assessment, bool>>) (t => (int?) t.id_assessment == iEvent.id_assessment && t.status == "A")).FirstOrDefault<tbl_assessment>();
                    tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == iEvent.id_category && t.STATUS == "A")).FirstOrDefault<tbl_category>();
                    if (tblAssessment != null)
                      str = tblAssessment.assessment_title + " [ " + tblCategory.CATEGORYNAME + " ]";
                  }
                }
                scheduledEvent1.program_description = str;
              }
            }
          }
          scheduledEvent1.program_location = iEvent.program_venue + " , " + iEvent.program_location;
          scheduledEvent1.program_name = iEvent.program_name;
          scheduledEvent1.facilitator_name = iEvent.facilitator_name;
          scheduledEvent1.facilitator_organization = iEvent.facilitator_organization;
          scheduledEvent1.program_objective = iEvent.program_objective;
          scheduledEvent1.is_approval = iEvent.is_approval;
          scheduledEvent1.is_response = iEvent.is_response;
          scheduledEvent1.is_unsubscribe = iEvent.is_unsubscribe;
          nullable1 = iEvent.program_duration_type;
          int num6 = 1;
          if (nullable1.GetValueOrDefault() == num6 & nullable1.HasValue)
          {
            scheduledEvent1.program_duration_type = "OPEN";
            scheduledEvent1.program_duration = "";
          }
          else
          {
            nullable1 = iEvent.program_duration_type;
            int num7 = 2;
            if (nullable1.GetValueOrDefault() == num7 & nullable1.HasValue)
            {
              scheduledEvent1.program_duration_type = "CLOSED";
              if (iEvent.program_duration_unit == "H")
              {
                ScheduledEvent scheduledEvent2 = scheduledEvent1;
                string str1 = iEvent.program_end_date.ToString();
                nullable1 = iEvent.program_duration;
                string str2 = nullable1.ToString();
                string str3 = str1 + " ( " + str2 + " Hour )";
                scheduledEvent2.program_duration = str3;
              }
              else if (iEvent.program_duration_unit == "D")
              {
                ScheduledEvent scheduledEvent3 = scheduledEvent1;
                string str4 = iEvent.program_end_date.ToString();
                nullable1 = iEvent.program_duration;
                string str5 = nullable1.ToString();
                string str6 = str4 + " ( " + str5 + " Day )";
                scheduledEvent3.program_duration = str6;
              }
              else if (iEvent.program_duration_unit == "W")
              {
                ScheduledEvent scheduledEvent4 = scheduledEvent1;
                string str7 = iEvent.program_end_date.ToString();
                nullable1 = iEvent.program_duration;
                string str8 = nullable1.ToString();
                string str9 = str7 + " ( " + str8 + " Week )";
                scheduledEvent4.program_duration = str9;
              }
              else if (iEvent.program_duration_unit == "M")
              {
                ScheduledEvent scheduledEvent5 = scheduledEvent1;
                string str10 = iEvent.program_end_date.ToString();
                nullable1 = iEvent.program_duration;
                string str11 = nullable1.ToString();
                string str12 = str10 + " ( " + str11 + " Month )";
                scheduledEvent5.program_duration = str12;
              }
            }
          }
          ScheduledEvent scheduledEvent6 = scheduledEvent1;
          DateTime dateTime = iEvent.registration_start_date.Value;
          string str13 = dateTime.ToString("dd-MM-yyyy HH:mm");
          scheduledEvent6.registration_start_date = str13;
          ScheduledEvent scheduledEvent7 = scheduledEvent1;
          dateTime = iEvent.registration_end_date.Value;
          string str14 = dateTime.ToString("dd-MM-yyyy HH:mm");
          scheduledEvent7.registration_end_date = str14;
          ScheduledEvent scheduledEvent8 = scheduledEvent1;
          nullable1 = iEvent.no_of_participants;
          string str15 = nullable1.ToString();
          scheduledEvent8.no_of_participants = str15;
          nullable1 = iEvent.event_type;
          int num8 = 1;
          if (nullable1.GetValueOrDefault() == num8 & nullable1.HasValue)
          {
            scheduledEvent1.event_type = "Open";
          }
          else
          {
            nullable1 = iEvent.event_type;
            int num9 = 2;
            if (nullable1.GetValueOrDefault() == num9 & nullable1.HasValue)
              scheduledEvent1.event_type = "Closed";
          }
          scheduledEvent1.attachment_info = "";
          scheduledEvent1.attachment_type = "0";
          nullable1 = iEvent.event_group_type;
          int num10 = 1;
          if (nullable1.GetValueOrDefault() == num10 & nullable1.HasValue)
          {
            scheduledEvent1.event_group_type = "Face to Face";
          }
          else
          {
            nullable1 = iEvent.event_group_type;
            int num11 = 2;
            if (nullable1.GetValueOrDefault() == num11 & nullable1.HasValue)
            {
              scheduledEvent1.event_group_type = "Online";
            }
            else
            {
              nullable1 = iEvent.event_group_type;
              int num12 = 3;
              if (nullable1.GetValueOrDefault() == num12 & nullable1.HasValue)
              {
                scheduledEvent1.attachment_type = "1";
                scheduledEvent1.event_group_type = "M2OST";
                nullable1 = iEvent.attachment_type;
                int num13 = 1;
                if (nullable1.GetValueOrDefault() == num13 & nullable1.HasValue)
                {
                  ScheduledEvent scheduledEvent9 = scheduledEvent1;
                  string[] strArray = new string[6];
                  strArray[0] = "api/getCategoryDashboard?catid=";
                  nullable1 = iEvent.id_program;
                  strArray[1] = nullable1.ToString();
                  strArray[2] = "&userid=";
                  strArray[3] = USER.UID.ToString();
                  strArray[4] = "&orgid=";
                  strArray[5] = USER.OID.ToString();
                  string str16 = string.Concat(strArray);
                  scheduledEvent9.REDIRECTION_URL = str16;
                  string str17 = "Program is attached : ";
                  tbl_category tblCategory = this.db.tbl_category.Where<tbl_category>((Expression<Func<tbl_category, bool>>) (t => (int?) t.ID_CATEGORY == iEvent.id_program)).FirstOrDefault<tbl_category>();
                  if (tblCategory != null)
                    scheduledEvent1.attachment_info = str17 + tblCategory.CATEGORYNAME;
                }
                else
                {
                  nullable1 = iEvent.attachment_type;
                  int num14 = 2;
                  if (nullable1.GetValueOrDefault() == num14 & nullable1.HasValue)
                  {
                    ScheduledEvent scheduledEvent10 = scheduledEvent1;
                    string[] strArray = new string[6];
                    strArray[0] = "api/Assessmentsheet?ASID=";
                    nullable1 = iEvent.id_assessment;
                    strArray[1] = nullable1.ToString();
                    strArray[2] = "&UID=";
                    strArray[3] = USER.UID.ToString();
                    strArray[4] = "&OID=";
                    strArray[5] = USER.OID.ToString();
                    string str18 = string.Concat(strArray);
                    scheduledEvent10.REDIRECTION_URL = str18;
                    scheduledEvent1.attachment_info = "Attachment is attached ";
                  }
                }
              }
            }
          }
          if (iEvent.status == "X")
          {
            scheduledEvent1.STATUS = "X";
            scheduledEvent1.MESSAGE = "Event has been canceled.";
            scheduledEvent1.COMMENT = iEvent.event_comment;
          }
          else if (item.subscription_status == "A")
          {
            dateTime = now;
            DateTime? eventStartDatetime = iEvent.event_start_datetime;
            if ((eventStartDatetime.HasValue ? (dateTime > eventStartDatetime.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              scheduledEvent1.STATUS = "E";
              scheduledEvent1.MESSAGE = "This event has been completed.";
              scheduledEvent1.COMMENT = "";
            }
            else
            {
              scheduledEvent1.STATUS = "A";
              scheduledEvent1.MESSAGE = "You have subscribed to the event.";
              scheduledEvent1.COMMENT = "";
            }
          }
          else if (item.subscription_status == "R")
          {
            scheduledEvent1.STATUS = "R";
            scheduledEvent1.MESSAGE = "You have declined the invitation to the event.";
            scheduledEvent1.COMMENT = item.event_user_comment;
          }
          else if (item.subscription_status == "C")
          {
            scheduledEvent1.STATUS = "C";
            scheduledEvent1.MESSAGE = "Your manager has rejected your request.";
            scheduledEvent1.COMMENT = item.event_user_comment;
          }
          else if (item.subscription_status == "P")
          {
            scheduledEvent1.STATUS = "P";
            scheduledEvent1.MESSAGE = "Your subscription request is still pending.";
            scheduledEvent1.COMMENT = "";
          }
          else if (item.subscription_status == "O")
          {
            dateTime = now;
            DateTime? nullable2 = iEvent.event_start_datetime;
            if ((nullable2.HasValue ? (dateTime > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
            {
              scheduledEvent1.STATUS = "E";
              scheduledEvent1.MESSAGE = "This event has been completed.";
              scheduledEvent1.COMMENT = "";
            }
            else
            {
              dateTime = now;
              nullable2 = iEvent.registration_end_date;
              if ((nullable2.HasValue ? (dateTime > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                scheduledEvent1.STATUS = "T";
                scheduledEvent1.MESSAGE = "Registration to the event is closed.";
                scheduledEvent1.COMMENT = "";
              }
              else
              {
                scheduledEvent1.STATUS = "O";
                scheduledEvent1.MESSAGE = "You have not yet subscribed to the event.";
                scheduledEvent1.COMMENT = "";
              }
            }
          }
          else if (item.subscription_status == "L")
          {
            scheduledEvent1.STATUS = "L";
            ScheduledEvent scheduledEvent11 = scheduledEvent1;
            nullable1 = iEvent.no_of_participants;
            string str19 = "Participant limit of " + nullable1.ToString() + " is already full.";
            scheduledEvent11.MESSAGE = str19;
            scheduledEvent1.COMMENT = "";
          }
        }
        else
        {
          apiresponse.KEY = "FAILURE";
          apiresponse.MESSAGE = "Could not find the Event in the System. Event might be deleted .Please contact your Repoting Manager. ";
        }
        apiresponse.KEY = "SUCCESS";
        string str20 = JsonConvert.SerializeObject((object) scheduledEvent1);
        apiresponse.MESSAGE = str20;
      }
      else
      {
        apiresponse.KEY = "FAILURE";
        apiresponse.MESSAGE = "Could not find the Event in the System. Event might be deleted .Please contact your Repoting Manager.";
      }
      return namespace2.CreateResponse<APIRESPONSE>(this.Request, HttpStatusCode.OK, apiresponse);
    }
  }
}
