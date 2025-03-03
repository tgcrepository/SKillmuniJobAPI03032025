// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_csst_role
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_csst_role
  {
    public int id_csst_role { get; set; }

    public string csst_role { get; set; }

    public int? id_organization { get; set; }

    public string status { get; set; }

    public DateTime? updated_dated_time { get; set; }

    public virtual tbl_organization tbl_organization { get; set; }
  }
}
