// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.AssessmentSlabGraph
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class AssessmentSlabGraph
  {
    public int id_assessment;
    public string assessment_title;
    public string CZONE;
    public string trainer_name;
    public int total_final;
    public int total_users;
    public int total_incomplete;
    public int slab1;
    public int slab2;
    public int slab3;
    public int slab4;
    public int slab5;

    public AssessmentSlabGraph(MySqlDataReader reader)
    {
      this.assessment_title = Convert.ToString(reader[nameof (assessment_title)]);
      this.CZONE = Convert.ToString(reader[nameof (CZONE)]);
      this.trainer_name = Convert.ToString(reader[nameof (trainer_name)]);
      this.id_assessment = Convert.ToInt32(reader[nameof (id_assessment)]);
      this.total_users = Convert.ToInt32(reader[nameof (total_users)]);
      this.slab1 = Convert.ToInt32(reader[nameof (slab1)]);
      this.slab2 = Convert.ToInt32(reader[nameof (slab2)]);
      this.slab3 = Convert.ToInt32(reader[nameof (slab3)]);
      this.slab4 = Convert.ToInt32(reader[nameof (slab4)]);
      this.slab5 = Convert.ToInt32(reader[nameof (slab5)]);
      this.total_final = 0;
      this.total_incomplete = 0;
    }
  }
}
