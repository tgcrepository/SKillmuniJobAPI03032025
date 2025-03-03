// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.masti_teacher_galleryfolder
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice
{
  public class masti_teacher_galleryfolder
  {
    public int id_gallery_folder { get; set; }

    public int? id_teacher { get; set; }

    public string folder_name { get; set; }

    public double? folder_size { get; set; }

    public DateTime? updated_time { get; set; }

    public int? id_org { get; set; }
  }
}
