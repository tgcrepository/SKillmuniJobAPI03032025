// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_sul_higher_education_user_registration
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_sul_higher_education_user_registration
  {
    public int id_register { get; set; }

    public int id_higher_education { get; set; }

    public int id_user { get; set; }

    public string ratings { get; set; }

    public string feedback { get; set; }

    public string slot { get; set; }

    public string status { get; set; }

    public DateTime update_date_time { get; set; }
  }
}
