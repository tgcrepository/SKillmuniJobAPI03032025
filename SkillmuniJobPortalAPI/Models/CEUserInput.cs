// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.CEUserInput
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class CEUserInput
  {
    public string Question { get; set; }

    public string Answer { get; set; }

    public string WANS { get; set; }

    public int srno { get; set; }

    public int jpscore { get; set; }

    public int is_right { get; set; }

    public int question_complexity { get; set; }

    public string question_complexity_label { get; set; }

    public List<CEAnswerBody> answerBody { get; set; }

    public tbl_brief_question questionBody { get; set; }
  }
}
