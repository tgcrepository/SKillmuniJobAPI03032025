﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_survey
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_survey
  {
    public tbl_survey()
    {
      this.tbl_survey_bank_link = (ICollection<m2ostnextservice.tbl_survey_bank_link>) new HashSet<m2ostnextservice.tbl_survey_bank_link>();
      this.tbl_survey_data = (ICollection<m2ostnextservice.tbl_survey_data>) new HashSet<m2ostnextservice.tbl_survey_data>();
    }

    public int ID_SURVEY { get; set; }

    public int? ID_CONTENT_ANSWER { get; set; }

    public int? ID_ANSWER_STEP { get; set; }

    public string SURVEY_NAME { get; set; }

    public string DESCRIPTION { get; set; }

    public string SURVEY_IMAGE { get; set; }

    public DateTime START_DATE { get; set; }

    public DateTime END_DATE { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual tbl_content_answer tbl_content_answer { get; set; }

    public virtual tbl_content_answer_steps tbl_content_answer_steps { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey_bank_link> tbl_survey_bank_link { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey_data> tbl_survey_data { get; set; }
  }
}
