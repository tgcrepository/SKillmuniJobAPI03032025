// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_feedback_data
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_feedback_data
  {
    public int ID_FEEDBACK_DATA { get; set; }

    public int ID_FEEDBACK_BANK { get; set; }

    public int ID_USER { get; set; }

    public string FEEDBACK_CHOICE { get; set; }

    public string FEEDBACK_TEXT { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual tbl_feedback_bank tbl_feedback_bank { get; set; }

    public virtual tbl_user tbl_user { get; set; }
  }
}
