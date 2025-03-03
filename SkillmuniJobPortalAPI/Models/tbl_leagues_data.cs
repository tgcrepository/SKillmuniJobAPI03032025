// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_leagues_data
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_leagues_data
  {
    public int id_league_data { get; set; }

    public int id_league { get; set; }

    public int id_theme { get; set; }

    public int id_game { get; set; }

    public double minscore { get; set; }

    public int evaluation_type { get; set; }

    public int expression_type { get; set; }

    public int movement_number { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int level { get; set; }

    public int id_metric { get; set; }
  }
}
