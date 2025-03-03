// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_user
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_user
  {
    public tbl_user()
    {
      this.tbl_assessment_general = (ICollection<m2ostnextservice.tbl_assessment_general>) new HashSet<m2ostnextservice.tbl_assessment_general>();
      this.tbl_feedback_data = (ICollection<m2ostnextservice.tbl_feedback_data>) new HashSet<m2ostnextservice.tbl_feedback_data>();
      this.tbl_offline_expiry = (ICollection<m2ostnextservice.tbl_offline_expiry>) new HashSet<m2ostnextservice.tbl_offline_expiry>();
      this.tbl_subscriptions = (ICollection<m2ostnextservice.tbl_subscriptions>) new HashSet<m2ostnextservice.tbl_subscriptions>();
      this.tbl_survey_data = (ICollection<m2ostnextservice.tbl_survey_data>) new HashSet<m2ostnextservice.tbl_survey_data>();
      this.tbl_user_data = (ICollection<m2ostnextservice.tbl_user_data>) new HashSet<m2ostnextservice.tbl_user_data>();
    }

    public int ID_USER { get; set; }

    public int ID_CODE { get; set; }

    public int? ID_ORGANIZATION { get; set; }

    public int ID_ROLE { get; set; }

    public string USERID { get; set; }

    public string PASSWORD { get; set; }

    public string FBSOCIALID { get; set; }

    public string GPSOCIALID { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDTIME { get; set; }

    public DateTime? EXPIRY_DATE { get; set; }

    public string EMPLOYEEID { get; set; }

    public string user_department { get; set; }

    public string user_designation { get; set; }

    public string user_function { get; set; }

    public string user_grade { get; set; }

    public string user_status { get; set; }

    public int? reporting_manager { get; set; }

    public int? is_reporting { get; set; }

    public string ref_id { get; set; }

    public int is_first_time_login { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_assessment_general> tbl_assessment_general { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_feedback_data> tbl_feedback_data { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_offline_expiry> tbl_offline_expiry { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_subscriptions> tbl_subscriptions { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey_data> tbl_survey_data { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_user_data> tbl_user_data { get; set; }
  }
}
