// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.sc_game_element_weightage
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class sc_game_element_weightage
  {
    public int id_sc_game_element_weightage { get; set; }

    public int? id_organization { get; set; }

    public int? id_game { get; set; }

    public int? element_type { get; set; }

    public int? id_user { get; set; }

    public int? id_category { get; set; }

    public int? id_assessment { get; set; }

    public int? id_kpi_master { get; set; }

    public double? element_score { get; set; }

    public double? element_weightage { get; set; }

    public string status { get; set; }

    public DateTime? updated_darte_time { get; set; }
  }
}
