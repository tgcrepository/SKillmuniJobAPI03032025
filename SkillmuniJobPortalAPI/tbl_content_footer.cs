// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_footer
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_content_footer
  {
    public tbl_content_footer() => this.tbl_content_header_footer = (ICollection<m2ostnextservice.tbl_content_header_footer>) new HashSet<m2ostnextservice.tbl_content_header_footer>();

    public int id_content_footer { get; set; }

    public int id_organization { get; set; }

    public string content_footer_name { get; set; }

    public string content_footer_image { get; set; }

    public string content_footer_action_url { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_header_footer> tbl_content_header_footer { get; set; }
  }
}
