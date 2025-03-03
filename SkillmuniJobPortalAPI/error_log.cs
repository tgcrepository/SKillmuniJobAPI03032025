// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.error_log
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class error_log
  {
    public int Error_ID { get; set; }

    public string Error_Message { get; set; }

    public string Error_Inner { get; set; }

    public string STATUS { get; set; }

    public DateTime UPDATEDDATETIME { get; set; }
  }
}
