// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.tbl_job_category_header
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class tbl_job_category_header
  {
    public int id_header { get; set; }

    public string header { get; set; }

    public string status { get; set; }

    public object tbl_job_category_headercol { get; set; }

    public DateTime updated_date_time { get; set; }

    public List<tbl_job_category> category { get; set; }
  }
}
