// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_subscriptions
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_subscriptions
  {
    public int ID_SUBSCRIPTION { get; set; }

    public int ID_USER { get; set; }

    public int ID_CONTENT { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDTIME { get; set; }

    public DateTime? EXPIRY_DATE { get; set; }

    public virtual tbl_content tbl_content { get; set; }

    public virtual tbl_user tbl_user { get; set; }
  }
}
