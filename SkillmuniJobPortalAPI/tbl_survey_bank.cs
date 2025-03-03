// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_survey_bank
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_survey_bank
  {
    public tbl_survey_bank()
    {
      this.tbl_survey_bank_link = (ICollection<m2ostnextservice.tbl_survey_bank_link>) new HashSet<m2ostnextservice.tbl_survey_bank_link>();
      this.tbl_survey_data = (ICollection<m2ostnextservice.tbl_survey_data>) new HashSet<m2ostnextservice.tbl_survey_data>();
    }

    public int ID_SURVEY_BANK { get; set; }

    public string SURVEY_QUESTION { get; set; }

    public string SURVEY_CHOICES { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey_bank_link> tbl_survey_bank_link { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_survey_data> tbl_survey_data { get; set; }
  }
}
