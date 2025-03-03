// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_user_zone_master
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice
{
  public class tbl_user_zone_master
  {
    public int id_user_zone_master { get; set; }

    public int? id_organization { get; set; }

    public string zone_title { get; set; }

    public string trainer_name { get; set; }

    public int? id_user_trainer { get; set; }

    public string employee_id { get; set; }

    public string status { get; set; }

    public string updated_date_time { get; set; }
  }
}
