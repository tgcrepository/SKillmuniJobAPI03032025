// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_higher_education_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_sul_higher_education_master
  {
    public int id_higher_education { get; set; }

    public string message_to_display { get; set; }

    public string redirect_url { get; set; }

    public string event_name { get; set; }

    public DateTime higher_education_start_time { get; set; }

    public DateTime higher_education_end_time { get; set; }

    public int time_interval { get; set; }

    public string location { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }

    public List<higher_education_time_slots> slots { get; set; }

    public int is_registered { get; set; }

    public string slot_registered { get; set; }
  }
}
