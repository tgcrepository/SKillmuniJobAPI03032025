// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CVBuilderCreation
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CVBuilderCreation
  {
    public int UID { get; set; }

    public int OID { get; set; }

    public int data_flag { get; set; }

    public int id_cv { get; set; }

    public tbl_cv_personel_info personel { get; set; }

    public List<tbl_cv_education> education { get; set; }

    public tbl_cv_additional_info additional_info { get; set; }

    public List<tbl_cv_project> project_list { get; set; }
  }
}
