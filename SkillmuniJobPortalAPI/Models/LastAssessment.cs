// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.LastAssessment
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class LastAssessment
  {
    public int id_assessment;
    public int total_users;
    public string assess_created;
    public string assess_start;
    public string assess_ended;
    public string assessment_title;

    public LastAssessment(MySqlDataReader reader)
    {
      this.assess_created = Convert.ToString(reader[nameof (assess_created)]);
      this.assess_start = Convert.ToString(reader[nameof (assess_start)]);
      this.assess_ended = Convert.ToString(reader[nameof (assess_ended)]);
      this.assessment_title = Convert.ToString(reader[nameof (assessment_title)]);
      this.id_assessment = Convert.ToInt32(reader[nameof (id_assessment)]);
      this.total_users = Convert.ToInt32(reader[nameof (total_users)]);
    }
  }
}
