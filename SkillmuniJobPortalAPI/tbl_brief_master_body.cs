﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_brief_master_body
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_brief_master_body
  {
    public int id_brief_master_body { get; set; }

    public int? id_brief_master { get; set; }

    public string brief_code { get; set; }

    public string brief_template { get; set; }

    public string brief_destination { get; set; }

    public string resource_number { get; set; }

    public int? srno { get; set; }

    public int? resource_type { get; set; }

    public string resouce_data { get; set; }

    public string resouce_code { get; set; }

    public int? media_type { get; set; }

    public string file_type { get; set; }

    public string file_extension { get; set; }

    public string resource_mime { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
