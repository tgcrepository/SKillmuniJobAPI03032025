// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_category_tiles
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_category_tiles
  {
    public tbl_category_tiles()
    {
      this.tbl_category_associantion = (ICollection<m2ostnextservice.tbl_category_associantion>) new HashSet<m2ostnextservice.tbl_category_associantion>();
      this.tbl_category_heading = (ICollection<m2ostnextservice.tbl_category_heading>) new HashSet<m2ostnextservice.tbl_category_heading>();
    }

    public int id_category_tiles { get; set; }

    public string tile_heading { get; set; }

    public int? category_theme { get; set; }

    public int? id_organization { get; set; }

    public string tile_image { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public int? category_order { get; set; }

    public string image_url { get; set; }

    public int? id_default { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_category_associantion> tbl_category_associantion { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_category_heading> tbl_category_heading { get; set; }

    public virtual tbl_organization tbl_organization { get; set; }
  }
}
