// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.QuestionResponse
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.Generic;

namespace m2ostnextservice.Models
{
  public class QuestionResponse
  {
    public int id_brief_question { get; set; }

    public string brief_question { get; set; }

    public List<tbl_brief_answer> answer { get; set; }

    public List<tbl_user_quiz_log> attempt_log { get; set; }

    public int max_score { get; set; }

    public int is_question_active { get; set; }

    public int no_of_attempts { get; set; }

    public int earned_marks { get; set; }
  }
}
