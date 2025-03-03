// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_content
  {
    public tbl_content()
    {
      this.tbl_content_banner = (ICollection<m2ostnextservice.tbl_content_banner>) new HashSet<m2ostnextservice.tbl_content_banner>();
      this.tbl_content_header_footer = (ICollection<m2ostnextservice.tbl_content_header_footer>) new HashSet<m2ostnextservice.tbl_content_header_footer>();
      this.tbl_content_right_association = (ICollection<m2ostnextservice.tbl_content_right_association>) new HashSet<m2ostnextservice.tbl_content_right_association>();
      this.tbl_content_organization_mapping = (ICollection<m2ostnextservice.tbl_content_organization_mapping>) new HashSet<m2ostnextservice.tbl_content_organization_mapping>();
      this.tbl_subscriptions = (ICollection<m2ostnextservice.tbl_subscriptions>) new HashSet<m2ostnextservice.tbl_subscriptions>();
    }

    public int ID_CONTENT { get; set; }

    public int ID_THEME { get; set; }

    public int ID_USER { get; set; }

    public int ID_CONTENT_LEVEL { get; set; }

    public string CONTENT_TITLE { get; set; }

    public string CONTENT_HEADER { get; set; }

    public string CONTENT_QUESTION { get; set; }

    public int CONTENT_COUNTER { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public int LINK_COUNT { get; set; }

    public int IS_PRIMARY { get; set; }

    public DateTime? EXPIRY_DATE { get; set; }

    public string COMMENT { get; set; }

    public string CONTENT_IDENTIFIER { get; set; }

    public int CONTENT_OWNER { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_banner> tbl_content_banner { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_header_footer> tbl_content_header_footer { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_right_association> tbl_content_right_association { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_organization_mapping> tbl_content_organization_mapping { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_subscriptions> tbl_subscriptions { get; set; }
  }
}
