// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_non_disclousure_clause_content
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_non_disclousure_clause_content
  {
    public int id_clause_content { get; set; }

    public int? id_org { get; set; }

    public string content_title { get; set; }

    public string content { get; set; }

    public DateTime? updated_date_time { get; set; }

    public int? id_creator { get; set; }

    public string status { get; set; }
  }
}
