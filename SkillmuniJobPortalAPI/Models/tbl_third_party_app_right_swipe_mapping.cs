// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_third_party_app_right_swipe_mapping
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_third_party_app_right_swipe_mapping
  {
    public int id_third_party { get; set; }

    public int id_mapping { get; set; }

    public int third_party_type { get; set; }

    public string title { get; set; }

    public string description { get; set; }

    public string logo { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
