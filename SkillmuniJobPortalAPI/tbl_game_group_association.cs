// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_game_group_association
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_game_group_association
  {
    public int id_game_group_association { get; set; }

    public int? id_game { get; set; }

    public int? association_type { get; set; }

    public int? id_game_group { get; set; }

    public int? id_user { get; set; }

    public int? id_organization { get; set; }

    public DateTime? creation_date { get; set; }

    public DateTime? removed_date { get; set; }

    public DateTime? start_date { get; set; }

    public DateTime? expiry_date { get; set; }

    public int? assigned_by { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
