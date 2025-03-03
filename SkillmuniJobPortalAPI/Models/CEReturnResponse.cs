// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CEReturnResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CEReturnResponse
  {
    public List<CEUserInput> ceReturn { get; set; }

    public string returnStat { get; set; }

    public int rightCount { get; set; }

    public int totalCount { get; set; }

    public int attemptno { get; set; }

    public double percentage { get; set; }

    public List<ComplexityResult> complexity { get; set; }

    public List<AnswerKeyBlock> answerKeyBlock { get; set; }

    public string CETime { get; set; }
  }
}
