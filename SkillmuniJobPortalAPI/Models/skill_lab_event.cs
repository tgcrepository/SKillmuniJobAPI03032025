// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.skill_lab_event
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class skill_lab_event
  {
    public int id_scheduled_event { get; set; }

    public int? id_organization { get; set; }

    public int? id_event_creator { get; set; }

    public string event_title { get; set; }

    public string event_description { get; set; }

    public DateTime? event_start_datetime { get; set; }

    public string facilitator_name { get; set; }

    public string program_image { get; set; }

    public string program_venue { get; set; }

    public string program_location { get; set; }

    public string event_additional_info { get; set; }

    public string event_comment { get; set; }

    public string participant_level { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public List<EventBatch> BatchList { get; set; }

    public string batch { get; set; }

    public int id_batch { get; set; }
  }
}
