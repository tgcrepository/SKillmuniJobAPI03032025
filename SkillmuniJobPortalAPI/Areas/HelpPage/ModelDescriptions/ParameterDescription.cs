// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ModelDescriptions.ParameterDescription
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.ObjectModel;

namespace m2ostnextservice.Areas.HelpPage.ModelDescriptions
{
  public class ParameterDescription
  {
    public ParameterDescription() => this.Annotations = new Collection<ParameterAnnotation>();

    public Collection<ParameterAnnotation> Annotations { get; private set; }

    public string Documentation { get; set; }

    public string Name { get; set; }

    public ModelDescription TypeDescription { get; set; }
  }
}
