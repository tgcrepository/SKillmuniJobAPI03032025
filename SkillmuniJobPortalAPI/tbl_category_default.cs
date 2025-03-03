// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_category_default
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_category_default
  {
    public int ID_CATEGORY { get; set; }

    public int? ID_ORGANIZATION { get; set; }

    public string CATEGORYNAME { get; set; }

    public string DESCRIPTION { get; set; }

    public string IMAGE_PATH { get; set; }

    public string STATUS { get; set; }

    public DateTime? UPDATED_DATE_TIME { get; set; }

    public string SUB_HEADING { get; set; }

    public int? ORDERID { get; set; }

    public int? IS_PRIMARY { get; set; }

    public int? ID_PARENT { get; set; }

    public int? SEARCH_MAX_COUNT { get; set; }

    public int? COUNT_REQUIRED { get; set; }

    public int? CATEGORY_TYPE { get; set; }

    public string IMAGE_URL { get; set; }

    public int? id_default { get; set; }
  }
}
