// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_mutthimeinsitare_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_mutthimeinsitare_master
  {
    public int id_mutthi_sheet { get; set; }

    public int? id_organization { get; set; }

    public int? id_user { get; set; }

    public string roll_no { get; set; }

    public string year { get; set; }

    public DateTime? doj { get; set; }

    public DateTime? created_on { get; set; }

    public DateTime? last_modified { get; set; }

    public int? term { get; set; }
  }
}
