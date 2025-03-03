// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_organization
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_organization
  {
    public tbl_organization()
    {
      this.tbl_assessment = (ICollection<m2ostnextservice.tbl_assessment>) new HashSet<m2ostnextservice.tbl_assessment>();
      this.tbl_assessment_general = (ICollection<m2ostnextservice.tbl_assessment_general>) new HashSet<m2ostnextservice.tbl_assessment_general>();
      this.tbl_assessment_sheet = (ICollection<m2ostnextservice.tbl_assessment_sheet>) new HashSet<m2ostnextservice.tbl_assessment_sheet>();
      this.tbl_banner = (ICollection<m2ostnextservice.tbl_banner>) new HashSet<m2ostnextservice.tbl_banner>();
      this.tbl_category = (ICollection<m2ostnextservice.tbl_category>) new HashSet<m2ostnextservice.tbl_category>();
      this.tbl_category_tiles = (ICollection<m2ostnextservice.tbl_category_tiles>) new HashSet<m2ostnextservice.tbl_category_tiles>();
      this.tbl_content_organization_mapping = (ICollection<m2ostnextservice.tbl_content_organization_mapping>) new HashSet<m2ostnextservice.tbl_content_organization_mapping>();
      this.tbl_content_right_association = (ICollection<m2ostnextservice.tbl_content_right_association>) new HashSet<m2ostnextservice.tbl_content_right_association>();
      this.tbl_csst_role = (ICollection<m2ostnextservice.tbl_csst_role>) new HashSet<m2ostnextservice.tbl_csst_role>();
      this.tbl_organisation_banner = (ICollection<m2ostnextservice.tbl_organisation_banner>) new HashSet<m2ostnextservice.tbl_organisation_banner>();
      this.tbl_role = (ICollection<m2ostnextservice.tbl_role>) new HashSet<m2ostnextservice.tbl_role>();
    }

    public int ID_ORGANIZATION { get; set; }

    public int ID_INDUSTRY { get; set; }

    public int ID_BUSINESS_TYPE { get; set; }

    public string ORGANIZATION_NAME { get; set; }

    public string DESCRIPTION { get; set; }

    public string LOGO { get; set; }

    public string CONTACT_NAME { get; set; }

    public string CONTACTNUMBER { get; set; }

    public string CONTACTEMAIL { get; set; }

    public string DEFAULT_EMAIL { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment> tbl_assessment { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment_general> tbl_assessment_general { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment_sheet> tbl_assessment_sheet { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_banner> tbl_banner { get; set; }

    public virtual tbl_business_type tbl_business_type { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_category> tbl_category { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_category_tiles> tbl_category_tiles { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_organization_mapping> tbl_content_organization_mapping { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_content_right_association> tbl_content_right_association { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_csst_role> tbl_csst_role { get; set; }

    public virtual tbl_industry tbl_industry { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_organisation_banner> tbl_organisation_banner { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_role> tbl_role { get; set; }
  }
}
