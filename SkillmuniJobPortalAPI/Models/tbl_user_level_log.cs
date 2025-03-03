// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_user_level_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_user_level_log
  {
    public int id_level_log { get; set; }

    public int id_user { get; set; }

    public int level { get; set; }

    public int attempt_no { get; set; }

    public int score { get; set; }

    public int bonus { get; set; }

    public int total_score { get; set; }

    public DateTime updated_date_time { get; set; }

    public int is_qualified { get; set; }

    public string status { get; set; }

    public string userid { get; set; }

    public List<tbl_user_assessment_log> assessment { get; set; }
  }
}
