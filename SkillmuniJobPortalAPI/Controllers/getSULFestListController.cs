// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.getSULFestListController
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

    public class getSULFestListController : ApiController
  {
    public HttpResponseMessage Get(int UID, int OID, int type)
    {
      List<tbl_sul_fest_master> source = new List<tbl_sul_fest_master>();
      try
      {
        using (m2ostnextserviceDbContext m2ostnextserviceDbContext = new m2ostnextserviceDbContext())
        {
          List<tbl_sul_fest_master> tblSulFestMasterList = new List<tbl_sul_fest_master>();
          tbl_profile tblProfile1 = new tbl_profile();
          List<tbl_sul_fest_master> list1 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_master>("select * from tbl_sul_fest_master where event_status={0} and status='A'", (object) "P").ToList<tbl_sul_fest_master>();
          tbl_profile tblProfile2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_profile>("select * from tbl_profile where ID_USER={0}", (object) UID).FirstOrDefault<tbl_profile>();
          foreach (tbl_sul_fest_master tblSulFestMaster in list1)
          {
            try
            {
              if (tblSulFestMaster.id_event == 113)
                ;
              List<tbl_sul_fest_event_mapping> festEventMappingList1 = new List<tbl_sul_fest_event_mapping>();
              List<tbl_sul_fest_event_mapping> list2 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_mapping>("select * from tbl_sul_fest_event_mapping where id_event={0} and type=1", (object) tblSulFestMaster.id_event).ToList<tbl_sul_fest_event_mapping>();
              List<tbl_sul_fest_event_mapping> festEventMappingList2 = new List<tbl_sul_fest_event_mapping>();
              List<tbl_sul_fest_event_mapping> list3 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_mapping>("select * from tbl_sul_fest_event_mapping where id_event={0} and type=2", (object) tblSulFestMaster.id_event).ToList<tbl_sul_fest_event_mapping>();
              List<tbl_sul_seminar_master> sulSeminarMasterList = new List<tbl_sul_seminar_master>();
              List<tbl_sul_higher_education_master> higherEducationMasterList = new List<tbl_sul_higher_education_master>();
              foreach (tbl_sul_fest_event_mapping festEventMapping in list2)
              {
                tbl_sul_seminar_master sulSeminarMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_seminar_master>("select * from tbl_sul_seminar_master where id_seminar={0}", (object) festEventMapping.id_seminar).FirstOrDefault<tbl_sul_seminar_master>();
                List<tbl_sul_seminar_timeslot_new> list4 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_seminar_timeslot_new>("select * from tbl_sul_seminar_timeslot_new where id_seminar={0}", (object) festEventMapping.id_seminar).ToList<tbl_sul_seminar_timeslot_new>();
                foreach (tbl_sul_seminar_timeslot_new seminarTimeslotNew1 in list4)
                {
                  seminarTimeslotNew1.slot_start_time = seminarTimeslotNew1.slot_start_time_hour.ToString() + ":" + seminarTimeslotNew1.slot_start_time_minute.ToString() + " " + seminarTimeslotNew1.session_start;
                  tbl_sul_seminar_timeslot_new seminarTimeslotNew2 = seminarTimeslotNew1;
                  string[] strArray = new string[5];
                  int num = seminarTimeslotNew1.slot_end_time_hour;
                  strArray[0] = num.ToString();
                  strArray[1] = ":";
                  num = seminarTimeslotNew1.slot_end_time_minute;
                  strArray[2] = num.ToString();
                  strArray[3] = " ";
                  strArray[4] = seminarTimeslotNew1.session_end;
                  string str = string.Concat(strArray);
                  seminarTimeslotNew2.slot_end_time = str;
                  List<tbl_sul_seminar_user_registration> list5 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_seminar_user_registration>("select * from tbl_sul_seminar_user_registration where id_seminar={0} and slot_id={1} ", (object) festEventMapping.id_seminar, (object) seminarTimeslotNew1.id_slot).ToList<tbl_sul_seminar_user_registration>();
                  seminarTimeslotNew1.count_restriction -= list5.Count;
                }
                sulSeminarMaster.slots = list4;
                List<tbl_sul_seminar_user_registration> list6 = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_seminar_user_registration>("select * from tbl_sul_seminar_user_registration where id_seminar={0} and id_user={1} ", (object) festEventMapping.id_seminar, (object) UID).ToList<tbl_sul_seminar_user_registration>();
                if (list6 != null)
                {
                  sulSeminarMaster.is_registered = list6.Count != list4.Count ? new int?(0) : new int?(1);
                  sulSeminarMaster.semslots = list6;
                }
                else
                  sulSeminarMaster.is_registered = new int?(0);
                sulSeminarMasterList.Add(sulSeminarMaster);
              }
              if (sulSeminarMasterList != null)
                tblSulFestMaster.seminar = sulSeminarMasterList;
              foreach (tbl_sul_fest_event_mapping festEventMapping in list3)
              {
                tbl_sul_higher_education_master higherEducationMaster = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_higher_education_master>("select * from tbl_sul_higher_education_master where id_higher_education={0}", (object) festEventMapping.id_higher_education).FirstOrDefault<tbl_sul_higher_education_master>();
                tbl_sul_higher_education_timeslot educationTimeslot = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_higher_education_timeslot>("select * from tbl_sul_higher_education_timeslot where id_higher_education={0}", (object) festEventMapping.id_higher_education).FirstOrDefault<tbl_sul_higher_education_timeslot>();
                int num1 = !(educationTimeslot.session_start_time == "AM") ? (educationTimeslot.time_slot_start_time_hour + 12) * 60 + educationTimeslot.time_slot_start_time_minute : educationTimeslot.time_slot_start_time_hour * 60 + educationTimeslot.time_slot_start_time_minute;
                int num2 = !(educationTimeslot.session_end_time == "AM") ? (educationTimeslot.time_slot_end_time_hour + 12) * 60 + educationTimeslot.time_slot_end_time_minute : educationTimeslot.time_slot_end_time_hour * 60 + educationTimeslot.time_slot_end_time_minute;
                int timeInterval = higherEducationMaster.time_interval;
                List<higher_education_time_slots> educationTimeSlotsList = new List<higher_education_time_slots>();
                int num3 = 0;
                int num4 = 1;
                while (num4 != 0)
                {
                  if (num3 != 0)
                    num1 += timeInterval;
                  higher_education_time_slots educationTimeSlots = new higher_education_time_slots();
                  educationTimeSlots.slot = Convert.ToString(Convert.ToDouble(num1) / 60.0) + " Hr";
                  string[] strArray1 = educationTimeSlots.slot.Split('.');
                  if (strArray1.Length > 1)
                  {
                    int int32 = Convert.ToInt32(Convert.ToDouble("0." + strArray1[1].Split(' ')[0]) * 60.0);
                    educationTimeSlots.slot = strArray1[0] + ":" + int32.ToString() + " Hr";
                  }
                  else
                  {
                    string[] strArray2 = strArray1[0].Split(' ');
                    educationTimeSlots.slot = strArray2[0] + ":00 Hr";
                  }
                  educationTimeSlotsList.Add(educationTimeSlots);
                  if (num1 >= num2)
                    num4 = 0;
                  ++num3;
                }
                higherEducationMaster.slots = educationTimeSlotsList;
                tbl_sul_higher_education_user_registration userRegistration = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_higher_education_user_registration>("select * from tbl_sul_higher_education_user_registration where id_higher_education={0} and id_user={1}", (object) festEventMapping.id_higher_education, (object) UID).FirstOrDefault<tbl_sul_higher_education_user_registration>();
                if (userRegistration != null)
                {
                  higherEducationMaster.is_registered = 1;
                  higherEducationMaster.slot_registered = userRegistration.slot;
                }
                else
                  higherEducationMaster.is_registered = 0;
                higherEducationMasterList.Add(higherEducationMaster);
              }
              if (higherEducationMasterList != null)
                tblSulFestMaster.highereducation = higherEducationMasterList;
              if (tblSulFestMaster.is_registration_needed == 1)
              {
                int num = m2ostnextserviceDbContext.Database.SqlQuery<int>("select count(id_register) from tbl_sul_fest_event_registration where id_event={0} and status={1}", (object) tblSulFestMaster.id_event, (object) "A").FirstOrDefault<int>();
                if (tblSulFestMaster.registration_start_date <= DateTime.Now && tblSulFestMaster.registration_end_date > DateTime.Now)
                {
                  tblSulFestMaster.registration_date_status = 1;
                  if (num < tblSulFestMaster.user_count)
                  {
                    tblSulFestMaster.registration_count_exceed_status = 0;
                    Database database = m2ostnextserviceDbContext.Database;
                    object[] objArray = new object[2]
                    {
                      (object) tblSulFestMaster.id_event,
                      (object) UID
                    };
                    tblSulFestMaster.registration_user_status = !(database.SqlQuery<string>("select status from tbl_sul_fest_event_registration where id_event={0}  and UID={1}", objArray).FirstOrDefault<string>() == "A") ? 0 : 1;
                  }
                  else
                  {
                    Database database = m2ostnextserviceDbContext.Database;
                    object[] objArray = new object[2]
                    {
                      (object) tblSulFestMaster.id_event,
                      (object) UID
                    };
                    tblSulFestMaster.registration_user_status = !(database.SqlQuery<string>("select status from tbl_sul_fest_event_registration where id_event={0}  and UID={1}", objArray).FirstOrDefault<string>() == "A") ? 0 : 1;
                    tblSulFestMaster.registration_count_exceed_status = 1;
                  }
                }
                else
                  tblSulFestMaster.registration_date_status = 0;
              }
              using (CrmDbContext crmDbContext = new CrmDbContext())
              {
                tblSulFestMaster.state_name = crmDbContext.Database.SqlQuery<string>("select name from states where id={0}", (object) tblSulFestMaster.state).FirstOrDefault<string>();
                tblSulFestMaster.city_name = crmDbContext.Database.SqlQuery<string>("select name from cities where id={0}", (object) tblSulFestMaster.city).FirstOrDefault<string>();
              }
              tblSulFestMaster.event_type = m2ostnextserviceDbContext.Database.SqlQuery<tbl_event_type_mapping>("select * from tbl_event_type_mapping inner join tbl_event_type_master on tbl_event_type_master.id_event_type=tbl_event_type_mapping.id_event_type  where tbl_event_type_mapping.id_event={0}", (object) tblSulFestMaster.id_event).ToList<tbl_event_type_mapping>();
              tblSulFestMaster.sub_event_type = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sub_event_type_mapping>("select * from tbl_sub_event_type_mapping inner join tbl_sub_event_type_master on tbl_sub_event_type_master.id_sub_event_type=tbl_sub_event_type_mapping.id_sub_event_type  where tbl_sub_event_type_mapping.id_event={0}", (object) tblSulFestMaster.id_event).ToList<tbl_sub_event_type_mapping>();
              tblSulFestMaster.event_logo = ConfigurationManager.AppSettings["FestEventLogo"].ToString() + tblSulFestMaster.event_logo;
              if (tblSulFestMaster.is_sponsor_available == 1)
              {
                tblSulFestMaster.sponsor = m2ostnextserviceDbContext.Database.SqlQuery<string>("select sponsor from tbl_event_sponsor_master where id_sponsor={0}", (object) tblSulFestMaster.id_sponsor).FirstOrDefault<string>();
                tblSulFestMaster.sponsor_logo = ConfigurationManager.AppSettings["SponSorLogo"].ToString() + tblSulFestMaster.sponsor_logo;
              }
              if (tblSulFestMaster.is_college_restricted == 1)
              {
                string str = m2ostnextserviceDbContext.Database.SqlQuery<string>("select college_name from tbl_college_list where id_college={0}", (object) tblSulFestMaster.id_college).FirstOrDefault<string>();
                if (str == tblProfile2.COLLEGE)
                {
                  tblSulFestMaster.college_name = str;
                  tbl_sul_fest_event_registration eventRegistration = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where UID={0} and id_event={1}", (object) UID, (object) tblSulFestMaster.id_event).FirstOrDefault<tbl_sul_fest_event_registration>();
                  tblSulFestMaster.register_status = eventRegistration == null ? 0 : (!(eventRegistration.status == "A") ? 2 : 1);
                  switch (type)
                  {
                    case 1:
                      if (tblSulFestMaster.event_start_date <= DateTime.Now)
                      {
                        if (tblSulFestMaster.event_end_date >= DateTime.Now)
                        {
                          source.Add(tblSulFestMaster);
                          continue;
                        }
                        continue;
                      }
                      continue;
                    case 2:
                      if (tblSulFestMaster.event_start_date > DateTime.Now)
                      {
                        source.Add(tblSulFestMaster);
                        continue;
                      }
                      continue;
                    case 3:
                      if (tblSulFestMaster.event_end_date < DateTime.Now)
                      {
                        source.Add(tblSulFestMaster);
                        continue;
                      }
                      continue;
                    default:
                      continue;
                  }
                }
              }
              else
              {
                tbl_sul_fest_event_registration eventRegistration = m2ostnextserviceDbContext.Database.SqlQuery<tbl_sul_fest_event_registration>("select * from tbl_sul_fest_event_registration where UID={0} and id_event={1}", (object) UID, (object) tblSulFestMaster.id_event).FirstOrDefault<tbl_sul_fest_event_registration>();
                tblSulFestMaster.register_status = eventRegistration == null ? 0 : (!(eventRegistration.status == "A") ? 2 : 1);
                switch (type)
                {
                  case 1:
                    if (tblSulFestMaster.event_start_date <= DateTime.Now)
                    {
                      if (tblSulFestMaster.event_end_date >= DateTime.Now)
                      {
                        source.Add(tblSulFestMaster);
                        continue;
                      }
                      continue;
                    }
                    continue;
                  case 2:
                    if (tblSulFestMaster.event_start_date > DateTime.Now)
                    {
                      source.Add(tblSulFestMaster);
                      continue;
                    }
                    continue;
                  case 3:
                    if (tblSulFestMaster.event_end_date < DateTime.Now)
                    {
                      source.Add(tblSulFestMaster);
                      continue;
                    }
                    continue;
                  default:
                    continue;
                }
              }
            }
            catch (Exception ex)
            {
              throw ex;
            }
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return namespace2.CreateResponse<List<tbl_sul_fest_master>>(this.Request, HttpStatusCode.OK, source.OrderBy<tbl_sul_fest_master, DateTime>((Func<tbl_sul_fest_master, DateTime>) (o => o.event_start_date)).ToList<tbl_sul_fest_master>());
    }
  }
}
