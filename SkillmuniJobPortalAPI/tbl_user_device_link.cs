﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_user_device_link
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_user_device_link
  {
    public int ID_USER_DEVICE_LINK { get; set; }

    public int ID_USER { get; set; }

    public int ID_DEVICE_TYPE { get; set; }

    public string DEVICE_ID { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATED_DATE_TIME { get; set; }
  }
}
