﻿// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LoginResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class LoginResponse
  {
    public string ResponseCode { get; set; }

    public int ResponseAction { get; set; }

    public string ResponseMessage { get; set; }

    public int UserID { get; set; }

    public string LogoPath { get; set; }

    public string BannerPath { get; set; }

    public string ORGEMAIL { get; set; }
  }
}
