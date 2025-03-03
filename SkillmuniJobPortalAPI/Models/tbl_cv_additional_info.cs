// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_cv_additional_info
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class tbl_cv_additional_info
  {
    public int id_cv_additional { get; set; }

    public int id_cv { get; set; }

    public int id_user { get; set; }

    public string skills { get; set; }

    public string languages { get; set; }

    public string intrests { get; set; }

    public string linkedin { get; set; }

    public string facebook { get; set; }

    public string twitter { get; set; }

    public string blog { get; set; }

    public string others { get; set; }

    public string refrences { get; set; }

    public string awards { get; set; }
  }
}
