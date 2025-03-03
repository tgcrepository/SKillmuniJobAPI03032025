// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_category_tile
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_category_tile
  {
    public int id_brief_category_tile { get; set; }

    public int? id_organization { get; set; }

    public string category_tile { get; set; }

    public string tile_code { get; set; }

    public string tile_description { get; set; }

    public string tile_image { get; set; }

    public int? category_tile_type { get; set; }

    public int? tile_position { get; set; }

    public int? assessment_complation { get; set; }

    public int? attempt_limit { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public string buttontext { get; set; }
  }
}
