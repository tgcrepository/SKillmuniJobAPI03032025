// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_ce_evaluation_jobindustry
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_ce_evaluation_jobindustry
  {
    public int id_ce_evaluation_jobindustry { get; set; }

    public int id_organization { get; set; }

    public string ce_job_industry { get; set; }

    public string ce_industry_code { get; set; }

    public string description { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int id_header { get; set; }

    public string tile_image { get; set; }

    public int tile_position { get; set; }

    public string buttontext { get; set; }
  }
}
