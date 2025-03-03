// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_cv_project
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class tbl_cv_project
  {
    public int id_cv_project { get; set; }

    public int id_cv { get; set; }

    public int id_user { get; set; }

    public string college { get; set; }

    public string project_title { get; set; }

    public string start_date { get; set; }

    public string end_date { get; set; }

    public string summary { get; set; }
  }
}
