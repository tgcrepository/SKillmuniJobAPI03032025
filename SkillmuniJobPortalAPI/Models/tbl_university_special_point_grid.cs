// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_university_special_point_grid
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_university_special_point_grid
  {
    public int id_special_point_grid { get; set; }

    public int id_special_points { get; set; }

    public double start_range { get; set; }

    public int end_range { get; set; }

    public int special_value { get; set; }

    public string special_text { get; set; }

    public int special_metric { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_metric { get; set; }

    public int id_game { get; set; }
  }
}
