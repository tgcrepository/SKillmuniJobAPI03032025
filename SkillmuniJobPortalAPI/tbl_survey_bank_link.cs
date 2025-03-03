// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_survey_bank_link
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_survey_bank_link
  {
    public int ID_SURVEY_BANK_LINK { get; set; }

    public int ID_SURVEY { get; set; }

    public int ID_SURVEY_BANK { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual tbl_survey tbl_survey { get; set; }

    public virtual tbl_survey_bank tbl_survey_bank { get; set; }
  }
}
