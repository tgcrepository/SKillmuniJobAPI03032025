// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_cv_personel_info
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class tbl_cv_personel_info
  {
    public int id_cv_personel_info { get; set; }

    public int id_cv { get; set; }

    public int id_user { get; set; }

    public string first_name { get; set; }

    public string last_name { get; set; }

    public string mobile { get; set; }

    public string email { get; set; }

    public string country { get; set; }

    public string city { get; set; }

    public string street { get; set; }

    public string day { get; set; }

    public string month { get; set; }

    public string about { get; set; }

    public string year { get; set; }
  }
}
