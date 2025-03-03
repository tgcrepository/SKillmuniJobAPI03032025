// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.tbl_news_ticker
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class tbl_news_ticker
  {
    public int Id_ticker { get; set; }

    public int? Id_org { get; set; }

    public int? Id_creator { get; set; }

    public string status { get; set; }

    public DateTime? update_date { get; set; }

    public DateTime? expiry_date { get; set; }

    public string ticker_news { get; set; }

    public string background_color { get; set; }

    public string font_color { get; set; }
  }
}
