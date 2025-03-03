// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_org_game_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_org_game_master
  {
    public int id_org_game { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public DateTime game_start_date_time { get; set; }

    public DateTime game_end_date_time { get; set; }

    public GameUserLog ScoreLog { get; set; }
  }
}
