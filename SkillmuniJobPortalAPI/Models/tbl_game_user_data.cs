// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_game_user_data
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_game_user_data
  {
    public int id_user_data { get; set; }

    public int id_org { get; set; }

    public int id_game { get; set; }

    public int id_user { get; set; }

    public double kpi_value { get; set; }

    public string status { get; set; }

    public DateTime updated_datetime { get; set; }
  }
}
