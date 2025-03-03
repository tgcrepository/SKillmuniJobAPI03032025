// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_scheduled_event
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_scheduled_event
  {
    public int id_scheduled_event { get; set; }

    public int? id_organization { get; set; }

    public int? id_event_creator { get; set; }

    public string event_title { get; set; }

    public string event_description { get; set; }

    public DateTime? registration_start_date { get; set; }

    public DateTime? registration_end_date { get; set; }

    public DateTime? event_start_datetime { get; set; }

    public string event_duration { get; set; }

    public int? event_type { get; set; }

    public int? event_group_type { get; set; }

    public string program_name { get; set; }

    public string program_description { get; set; }

    public string program_objective { get; set; }

    public string facilitator_name { get; set; }

    public string facilitator_organization { get; set; }

    public string program_image { get; set; }

    public int? no_of_participants { get; set; }

    public string program_venue { get; set; }

    public string program_location { get; set; }

    public int? attachment_type { get; set; }

    public int? id_program { get; set; }

    public int? id_assessment { get; set; }

    public int? id_category_tile { get; set; }

    public int? id_category_heading { get; set; }

    public int? id_category { get; set; }

    public string is_approval { get; set; }

    public string is_response { get; set; }

    public string is_unsubscribe { get; set; }

    public DateTime? program_start_date { get; set; }

    public int? program_duration_type { get; set; }

    public int? program_duration { get; set; }

    public string program_duration_unit { get; set; }

    public DateTime? program_end_date { get; set; }

    public string event_additional_info { get; set; }

    public string event_online_url { get; set; }

    public string participant_level { get; set; }

    public string event_comment { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
