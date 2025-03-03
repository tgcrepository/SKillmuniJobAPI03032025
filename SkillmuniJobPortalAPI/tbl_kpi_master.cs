// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_kpi_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_kpi_master
  {
    public int id_kpi_master { get; set; }

    public int? id_organization { get; set; }

    public string kpi_name { get; set; }

    public string kpi_description { get; set; }

    public int? kpi_type { get; set; }

    public string KPIID { get; set; }

    public DateTime? start_date { get; set; }

    public DateTime? expiry_date { get; set; }

    public int? id_creator { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
