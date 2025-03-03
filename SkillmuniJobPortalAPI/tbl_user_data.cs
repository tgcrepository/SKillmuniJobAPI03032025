// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_user_data
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_user_data
  {
    public int ID_USER_DATA { get; set; }

    public int ID_USER { get; set; }

    public int ID_DEVICE_TYPE { get; set; }

    public string DEVICE_ID { get; set; }

    public int ID_ACTION { get; set; }

    public DateTime UPDATEDDATETIME { get; set; }

    public virtual tbl_action tbl_action { get; set; }

    public virtual tbl_device_type tbl_device_type { get; set; }

    public virtual tbl_user tbl_user { get; set; }
  }
}
