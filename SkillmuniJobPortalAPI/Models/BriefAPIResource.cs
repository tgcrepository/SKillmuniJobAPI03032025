// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefAPIResource
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class BriefAPIResource
  {
    public int id_organization { get; set; }

    public string brief_title { get; set; }

    public string brief_code { get; set; }

    public string brief_description { get; set; }

    public DateTime? datetimestamp { get; set; }

    public string scheduled_type { get; set; }

    public int override_dnd { get; set; }

    public int id_brief_master { get; set; }

    public int is_question_attached { get; set; }

    public int read_status { get; set; }

    public int action_status { get; set; }

    public int? id_brief_category { get; set; }

    public int id_brief_sub_category { get; set; }

    public int id_user { get; set; }

    public int question_count { get; set; }

    public int RESULTSTATUS { get; set; }

    public double RESULTSCORE { get; set; }

    public string brief_subcategory { get; set; }

    public string brief_category { get; set; }

    public int SRNO { get; set; }

    public string brief_template { get; set; }

    public List<BriefRow> briefResource { get; set; }

    public string RestrictionMessage { get; set; }

    public int RestrictionCode { get; set; }

    public int SlideCheckCount { get; set; }

    public DateTime? BrfDate { get; set; }

    public Cat_Mapping_Data ctmap { get; set; }

    public tbl_brief_m2ost_category_mapping cat_mapping { get; set; }

    public int brief_attachment_flag { get; set; }

    public string brief_attachement_url { get; set; }

    public int liked { get; set; }

    public int disliked { get; set; }
  }
}
