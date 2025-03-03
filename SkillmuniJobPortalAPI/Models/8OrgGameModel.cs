// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_org_game_content
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_org_game_content
  {
    public int id_game_content { get; set; }

    public int content_type { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public int id_brief_master { get; set; }

    public int id_level { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int content_sequence { get; set; }

    public List<tbl_org_game_user_log> user_log { get; set; }

    public tbl_org_game_badge_master badge_log { get; set; }
  }
}
