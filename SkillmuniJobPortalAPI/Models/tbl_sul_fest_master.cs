// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_fest_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_sul_fest_master
  {
    public int id_event { get; set; }

    public string event_title { get; set; }

    public string event_objective { get; set; }

    public string event_logo { get; set; }

    public int is_registration_needed { get; set; }

    public DateTime registration_start_date { get; set; }

    public DateTime registration_end_date { get; set; }

    public DateTime event_start_date { get; set; }

    public DateTime event_end_date { get; set; }

    public string event_duration { get; set; }

    public string location_text { get; set; }

    public string state { get; set; }

    public string city { get; set; }

    public string address { get; set; }

    public int is_event_closed { get; set; }

    public int user_count { get; set; }

    public int is_college_restricted { get; set; }

    public int id_college { get; set; }

    public int is_paid_event { get; set; }

    public string amount { get; set; }

    public int is_org_specified { get; set; }

    public int id_org { get; set; }

    public int is_sponsor_available { get; set; }

    public int id_sponsor { get; set; }

    public string sponsor_logo { get; set; }

    public string event_status { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public List<tbl_event_type_mapping> event_type { get; set; }

    public List<tbl_sub_event_type_mapping> sub_event_type { get; set; }

    public string sponsor { get; set; }

    public int register_status { get; set; }

    public string college_name { get; set; }

    public string state_name { get; set; }

    public string city_name { get; set; }

    public int registration_date_status { get; set; }

    public int registration_count_exceed_status { get; set; }

    public int registration_user_status { get; set; }

    public string contact_name { get; set; }

    public string contact_number { get; set; }

    public List<tbl_sul_seminar_master> seminar { get; set; }

    public List<tbl_sul_higher_education_master> highereducation { get; set; }
  }
}
