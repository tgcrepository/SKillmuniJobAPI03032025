// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_badge_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_badge_master
  {
    public int id_badge { get; set; }

    public int id_theme { get; set; }

    public string badge_name { get; set; }

    public string badge_logo { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }

    public int WonFlag { get; set; }

    public int eligiblescore { get; set; }

    public string currency_name { get; set; }

    public int currency_value { get; set; }

    public int badge_count { get; set; }

    public int money_value { get; set; }
  }
}
