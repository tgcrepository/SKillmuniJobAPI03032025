﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_assessment_general
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_assessment_general
  {
    public int id_assessment_general { get; set; }

    public int? id_organization { get; set; }

    public int id_assesment_sheet { get; set; }

    public int? id_user { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }

    public virtual tbl_organization tbl_organization { get; set; }

    public virtual tbl_assessment_sheet tbl_assessment_sheet { get; set; }

    public virtual tbl_user tbl_user { get; set; }
  }
}
