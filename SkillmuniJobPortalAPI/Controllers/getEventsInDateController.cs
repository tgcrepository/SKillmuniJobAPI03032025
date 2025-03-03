// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getEventsInDateController
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Models;
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

    public class getEventsInDateController : ApiController
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public HttpResponseMessage Get(string IdString, string id_user)
    {
      tbl_user tblUser = this.db.tbl_user.Where<tbl_user>((Expression<Func<tbl_user, bool>>) (t => t.USERID == id_user)).FirstOrDefault<tbl_user>();
      List<tbl_scheduled_event> tblScheduledEventList = new List<tbl_scheduled_event>();
      List<int> intList = new List<int>();
      string str1 = IdString;
      char[] chArray = new char[1]{ '|' };
      foreach (string str2 in str1.Split(chArray))
        intList.Add(Convert.ToInt32(str2));
      List<skill_lab_event> skillLabEventList = new List<skill_lab_event>();
      foreach (int num in intList)
      {
        int itm = num;
        tbl_scheduled_event tblScheduledEvent = this.db.tbl_scheduled_event.Where<tbl_scheduled_event>((Expression<Func<tbl_scheduled_event, bool>>) (t => t.id_scheduled_event == itm)).FirstOrDefault<tbl_scheduled_event>();
        skill_lab_event skillLabEvent = new skill_lab_event();
        skillLabEvent.event_additional_info = tblScheduledEvent.event_additional_info;
        skillLabEvent.event_comment = tblScheduledEvent.event_comment;
        skillLabEvent.event_description = tblScheduledEvent.event_description;
        skillLabEvent.event_start_datetime = tblScheduledEvent.registration_start_date;
        skillLabEvent.event_title = tblScheduledEvent.event_title;
        skillLabEvent.facilitator_name = tblScheduledEvent.facilitator_name;
        skillLabEvent.id_scheduled_event = tblScheduledEvent.id_scheduled_event;
        skillLabEvent.participant_level = tblScheduledEvent.participant_level;
        skillLabEvent.program_image = tblScheduledEvent.program_image;
        skillLabEvent.program_location = tblScheduledEvent.program_location;
        skillLabEvent.program_venue = tblScheduledEvent.program_venue;
        skillLabEvent.id_organization = tblScheduledEvent.id_organization;
        List<EventBatch> eventBatchList = new List<EventBatch>();
        skillLabEvent.BatchList = new EventLogic().getBatchList(skillLabEvent.id_scheduled_event);
        skillLabEvent.BatchList = new EventLogic().getAvailable(skillLabEvent.BatchList);
        tbl_user_event_mapping userEventMapping = new tbl_user_event_mapping();
        tbl_user_event_mapping mappedEvent = new EventLogic().getMappedEvent(tblUser.ID_USER, itm);
        if (mappedEvent != null)
        {
          skillLabEvent.id_batch = mappedEvent.id_batch;
          skillLabEvent.batch = new EventLogic().getBatch(mappedEvent.id_batch);
        }
        skillLabEventList.Add(skillLabEvent);
      }
      return namespace2.CreateResponse<List<skill_lab_event>>(this.Request, HttpStatusCode.OK, skillLabEventList);
    }
  }
}
