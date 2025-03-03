// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_brief_tile_level_brief_restriction
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_brief_tile_level_brief_restriction
  {
    public int id_restriction { get; set; }

    public int id_brief_tile { get; set; }

    public int OID { get; set; }

    public int brief_count { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }

    public int time { get; set; }

    public int id_academy { get; set; }
  }
}
