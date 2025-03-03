// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_color_config
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_color_config
  {
    public int id_color_config { get; set; }

    public int? id_organisation { get; set; }

    public int? config_type { get; set; }

    public string grid1_bk_color { get; set; }

    public string grid1_text_color { get; set; }

    public string grid2_bk_color { get; set; }

    public string grid2_text_color { get; set; }

    public string status { get; set; }

    public DateTime? created_date_time { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
