﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_user_game_score_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_user_game_score_log
  {
    public int id_game_score_log { get; set; }

    public int id_user { get; set; }

    public int id_game { get; set; }

    public int id_brief { get; set; }

    public int id_org { get; set; }

    public double score { get; set; }

    public string status { get; set; }

    public int id_academic_tile { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_metric { get; set; }

    public string metric_value { get; set; }
  }
}
