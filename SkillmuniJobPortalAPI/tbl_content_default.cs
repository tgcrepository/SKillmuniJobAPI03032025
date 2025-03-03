// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_default
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_content_default
  {
    public int ID_CONTENT { get; set; }

    public int? ID_THEME { get; set; }

    public int? ID_USER { get; set; }

    public int? ID_CONTENT_LEVEL { get; set; }

    public string CONTENT_TITLE { get; set; }

    public string CONTENT_HEADER { get; set; }

    public string CONTENT_QUESTION { get; set; }

    public int? CONTENT_COUNTER { get; set; }

    public string STATUS { get; set; }

    public DateTime? UPDATED_DATE_TIME { get; set; }

    public int LINK_COUNT { get; set; }

    public int IS_PRIMARY { get; set; }

    public DateTime? EXPIRY_DATE { get; set; }

    public string COMMENT { get; set; }

    public string CONTENT_IDENTIFIER { get; set; }

    public int CONTENT_OWNER { get; set; }
  }
}
