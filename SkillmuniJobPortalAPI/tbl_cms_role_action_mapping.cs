﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_cms_role_action_mapping
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_cms_role_action_mapping
  {
    public int id_cms_role_action_mapping { get; set; }

    public int? id_cms_role_action { get; set; }

    public int? id_cms_role { get; set; }

    public int? id_organization { get; set; }

    public string status { get; set; }

    public DateTime? updated_date_time { get; set; }
  }
}
