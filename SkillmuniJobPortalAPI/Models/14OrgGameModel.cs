﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LevelUserLogResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class LevelUserLogResponse
  {
    public int UID { get; set; }

    public int OID { get; set; }

    public int id_game { get; set; }

    public int id_level { get; set; }

    public int is_level_completed { get; set; }

    public List<tbl_org_game_content> content { get; set; }

    public tbl_org_game_badge_master level_badge_log { get; set; }
  }
}
