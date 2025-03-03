// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_game_process_path
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_game_process_path
  {
    public int id_game_process_path { get; set; }

    public int? id_game { get; set; }

    public int? id_organization { get; set; }

    public int? process_type { get; set; }

    public int? sequence_number { get; set; }

    public int? is_mandatory { get; set; }

    public double? weightage { get; set; }

    public int? element_type { get; set; }

    public int? id_category { get; set; }

    public int? id_assessment { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
