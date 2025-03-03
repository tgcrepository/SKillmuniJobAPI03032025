// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_user_log_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_user_log_master
  {
    public int id_log { get; set; }

    public int id_user { get; set; }

    public string is_registered { get; set; }

    public int academic_tiles { get; set; }

    public int study_abroad { get; set; }

    public int job { get; set; }

    public int entrepreneurship { get; set; }

    public DateTime updated_date_time { get; set; }
  }
}
