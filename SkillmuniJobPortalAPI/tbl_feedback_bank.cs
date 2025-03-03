// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_feedback_bank
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice
{
  public class tbl_feedback_bank
  {
    public tbl_feedback_bank()
    {
      this.tbl_feedback_bank_link = (ICollection<m2ostnextservice.tbl_feedback_bank_link>) new HashSet<m2ostnextservice.tbl_feedback_bank_link>();
      this.tbl_feedback_data = (ICollection<m2ostnextservice.tbl_feedback_data>) new HashSet<m2ostnextservice.tbl_feedback_data>();
    }

    public int ID_FEEDBACK_BANK { get; set; }

    public string FEEDBACK_NAME { get; set; }

    public int? id_organization { get; set; }

    public string FEEDBACK_QUESTION { get; set; }

    public string FEEDBACK_CHOICES { get; set; }

    public string FEEDBACK_IMAGE { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_feedback_bank_link> tbl_feedback_bank_link { get; set; }

    public virtual ICollection<m2ostnextservice.tbl_feedback_data> tbl_feedback_data { get; set; }
  }
}
