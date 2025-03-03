// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ZoneCounter
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class ZoneCounter
  {
    public string assessment_title;
    public int nscore;
    public int sscore;
    public int escore;
    public int wscore;

    public ZoneCounter()
    {
      this.assessment_title = "";
      this.nscore = 0;
      this.sscore = 0;
      this.wscore = 0;
      this.escore = 0;
    }
  }
}
