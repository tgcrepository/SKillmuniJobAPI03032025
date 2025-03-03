// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.BriefUserInput
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

namespace m2ostnextservice.Models
{
  public class BriefUserInput
  {
    public string Question { get; set; }

    public string Answer { get; set; }

    public string WANS { get; set; }

    public int srno { get; set; }

    public int is_right { get; set; }

    public int question_complexity { get; set; }

    public string question_complexity_label { get; set; }

    public int? id_question { get; set; }

    public int id_answer { get; set; }

    public int id_wans { get; set; }

    public int questiontheme { get; set; }

    public int questionchoicetype { get; set; }

    public string questionimg { get; set; }

    public int answertheme { get; set; }

    public int answerchoicetype { get; set; }

    public string answerimg { get; set; }

    public int wanstheme { get; set; }

    public int wanschoicetype { get; set; }

    public string wansimg { get; set; }
  }
}
