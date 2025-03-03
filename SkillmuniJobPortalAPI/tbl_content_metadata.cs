// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_content_metadata
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_content_metadata
  {
    public int ID_CONTENT_METADATA { get; set; }

    public string CONTENT_METADATA { get; set; }

    public int CONTENT_METADATA_COUNTER { get; set; }

    public int ID_CONTENT_ANSWER { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDTIME { get; set; }

    public virtual tbl_content_answer tbl_content_answer { get; set; }
  }
}
