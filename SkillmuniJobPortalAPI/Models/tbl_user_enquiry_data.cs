﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_user_enquiry_data
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_user_enquiry_data
  {
    public int id_enquiry { get; set; }

    public int id_user { get; set; }

    public int enquiry_type { get; set; }

    public string name { get; set; }

    public string mail { get; set; }

    public string phone { get; set; }

    public int enquiry_reason { get; set; }

    public string message { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
