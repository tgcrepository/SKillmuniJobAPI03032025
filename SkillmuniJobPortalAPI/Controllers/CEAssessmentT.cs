// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Controllers.CEAssessmentT
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Controllers
{
  public class CEAssessmentT
  {
    public string career_evaluation_title { get; set; }

    public string career_evaluation_code { get; set; }

    public int ce_assessment_type { get; set; }

    public string cea_type { get; set; }

    public int job_points_for_ra { get; set; }

    public List<int> CEAssessList { get; set; }
  }
}
