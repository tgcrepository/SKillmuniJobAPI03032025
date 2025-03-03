// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_seminar_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_sul_seminar_master
  {
    public int id_seminar { get; set; }

    public string title { get; set; }

    public string objective { get; set; }

    public string stream { get; set; }

    public DateTime seminar_start_time { get; set; }

    public DateTime seminar_end_time { get; set; }

    public string seminar_duration { get; set; }

    public string speaker_name { get; set; }

    public string speaker_organisation { get; set; }

    public string location { get; set; }

    public int? user_count { get; set; }

    public string seminar_status { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }

    public int? fest_type { get; set; }

    public int? time_interval { get; set; }

    public List<tbl_sul_seminar_timeslot_new> slots { get; set; }

    public int? is_registered { get; set; }

    public List<tbl_sul_seminar_user_registration> slot_registered { get; set; }

    public List<tbl_sul_seminar_user_registration> semslots { get; set; }
  }
}
