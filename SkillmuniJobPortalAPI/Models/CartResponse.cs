﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CartResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CartResponse
  {
    public int id_subscription_cart { get; set; }

    public List<SearchResponce> content { get; set; }

    public string id_user { get; set; }
  }
}
