// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_category_heading
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_category_heading
  {
    public int id_category_heading { get; set; }

    public string Heading_title { get; set; }

    public int? id_category_tiles { get; set; }

    public int? heading_order { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public virtual tbl_category_tiles tbl_category_tiles { get; set; }
  }
}
