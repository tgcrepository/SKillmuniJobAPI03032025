// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.AuthResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class AuthResponse
  {
    public int IDUSER { get; set; }

    public string USERID { get; set; }

    public int OID { get; set; }

    public string FIRST_NAME { get; set; }

    public string LAST_NAME { get; set; }

    public string PROFILE_IMAGE { get; set; }

    public string AuthStatus { get; set; }

    public string AuthMessage { get; set; }

    public int id_org_game_unit { get; set; }

    public string UserFunction { get; set; }

    public int is_first_time_login { get; set; }

    public string unit { get; set; }

    public int avatar_type { get; set; }
  }
}
