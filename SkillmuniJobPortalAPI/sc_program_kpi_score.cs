﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.sc_program_kpi_score
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class sc_program_kpi_score
  {
    public int id_sc_program_kpi_score { get; set; }

    public string transactionid { get; set; }

    public int? id_category { get; set; }

    public int? id_user { get; set; }

    public int? id_kpi_master { get; set; }

    public int? id_organization { get; set; }

    public double? score { get; set; }

    public DateTime? log_datetime { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
