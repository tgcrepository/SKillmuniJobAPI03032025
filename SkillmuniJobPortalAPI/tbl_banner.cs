// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_banner
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_banner
  {
    public tbl_banner() => this.tbl_content_banner = (ICollection<m2ostnextservice.tbl_content_banner>) new HashSet<m2ostnextservice.tbl_content_banner>();

    public int id_banner { get; set; }

    public int id_organization { get; set; }

    public string banner_name { get; set; }

    public string banner_image { get; set; }

    public string banner_action_url { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_banner> tbl_content_banner { get; set; }

    public virtual tbl_organization tbl_organization { get; set; }
  }
}
