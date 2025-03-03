// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_org_game_content_badge_mapping
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_org_game_content_badge_mapping
  {
    public int id_mapping { get; set; }

    public int id_game { get; set; }

    public int id_content { get; set; }

    public int id_level { get; set; }

    public string status { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
