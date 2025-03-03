// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.ContentCounter
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using MySql.Data.MySqlClient;
using System;

namespace m2ostnextservice.Models
{
  public class ContentCounter
  {
    public int id_organization;
    public int id_content;
    public string CONTENT_QUESTION;
    public int counter;

    public ContentCounter(MySqlDataReader reader)
    {
      this.id_organization = Convert.ToInt32(reader[nameof (id_organization)]);
      this.id_content = Convert.ToInt32(reader[nameof (id_content)]);
      this.counter = Convert.ToInt32(reader[nameof (counter)]);
      this.CONTENT_QUESTION = Convert.ToString(reader[nameof (CONTENT_QUESTION)]);
    }
  }
}
