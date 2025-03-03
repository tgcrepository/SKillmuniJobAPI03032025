// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_org_game_user_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_org_game_user_log
  {
    public int id_log { get; set; }

    public int id_user { get; set; }

    public int id_game_content { get; set; }

    public int score { get; set; }

    public int id_score_unit { get; set; }

    public int score_type { get; set; }

    public string score_unit { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_level { get; set; }

    public int id_org_game { get; set; }

    public int attempt_no { get; set; }

    public string timetaken_to_complete { get; set; }

    public int is_completed { get; set; }

    public int UID { get; set; }

    public int OID { get; set; }
  }
}
