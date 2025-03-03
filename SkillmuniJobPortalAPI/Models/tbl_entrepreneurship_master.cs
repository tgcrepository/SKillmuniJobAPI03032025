// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_entrepreneurship_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_entrepreneurship_master
  {
    public int id_entrepreneurship { get; set; }

    public string company_name { get; set; }

    public string founders { get; set; }

    public string foundation_date { get; set; }

    public string reason { get; set; }

    public int id_buisiness_stage { get; set; }

    public string revenue { get; set; }

    public string far_from_launch { get; set; }

    public string company_structure { get; set; }

    public string buisiness_stage_others { get; set; }

    public DateTime updated_date_time { get; set; }

    public string product_code { get; set; }

    public string website { get; set; }

    public int id_user { get; set; }
  }
}
