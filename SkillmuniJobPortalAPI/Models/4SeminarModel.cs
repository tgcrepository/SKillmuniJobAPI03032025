// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_seminar_timeslot
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_sul_seminar_timeslot
  {
    public int id_slot { get; set; }

    public int time_slot_start_time_hour { get; set; }

    public int time_slot_start_time_minute { get; set; }

    public int time_slot_end_time_hour { get; set; }

    public int time_slot_end_time_minute { get; set; }

    public string session_start_time { get; set; }

    public string session_end_time { get; set; }

    public int id_seminar { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
