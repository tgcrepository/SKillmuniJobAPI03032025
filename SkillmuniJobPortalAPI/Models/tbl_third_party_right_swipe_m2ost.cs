// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_third_party_right_swipe_m2ost
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Models
{
  public class tbl_third_party_right_swipe_m2ost
  {
    public int id_swipe { get; set; }

    public int id_brief { get; set; }

    public int id_category { get; set; }

    public int id_org { get; set; }

    public string status { get; set; }

    public int type { get; set; }

    public string URL { get; set; }

    public DateTime updated_date_time { get; set; }

    public string CATEGORYNAME { get; set; }

    public int id_category_heading { get; set; }

    public string Heading_title { get; set; }

    public string CategoryImage { get; set; }
  }
}
