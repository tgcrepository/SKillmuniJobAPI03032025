// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_program_mapping
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_content_program_mapping
  {
    public int id_content_program_mapping { get; set; }

    public int? id_organization { get; set; }

    public int? id_category { get; set; }

    public int? id_assessment_sheet { get; set; }

    public int? id_user { get; set; }

    public int? id_role { get; set; }

    public int? map_type { get; set; }

    public int? option_type { get; set; }

    public DateTime? start_date { get; set; }

    public DateTime? expiry_date { get; set; }

    public int? id_category_tile { get; set; }

    public int? id_category_heading { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
