// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LoginResponseAuth
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class LoginResponseAuth
  {
    public string ResponseCode { get; set; }

    public int ResponseAction { get; set; }

    public string ResponseMessage { get; set; }

    public int UserID { get; set; }

    public string UserName { get; set; }

    public string LogoPath { get; set; }

    public string BannerPath { get; set; }

    public string ROLEID { get; set; }

    public string ORGID { get; set; }

    public string ORGEMAIL { get; set; }

    public string REURL { get; set; }

    public List<Menu> menu_response { get; set; }

    public string fullname { get; set; }

    public int log_flag { get; set; }

    public int profile_data { get; set; }

    public string profile_image { get; set; }

    public int total_score { get; set; }

    public int last_successive_level { get; set; }

    public string ref_id { get; set; }

    public string UserEmail { get; set; }

    public string college { get; set; }

    public string city { get; set; }

    public string state { get; set; }

    public string college_state { get; set; }

    public string college_city { get; set; }

    public int id_college { get; set; }

    public int is_first_time_login { get; set; }
  }
}
