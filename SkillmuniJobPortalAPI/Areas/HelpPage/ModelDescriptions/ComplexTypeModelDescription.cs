// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ModelDescriptions.ComplexTypeModelDescription
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Collections.ObjectModel;

namespace m2ostnextservice.Areas.HelpPage.ModelDescriptions
{
  public class ComplexTypeModelDescription : ModelDescription
  {
    public ComplexTypeModelDescription() => this.Properties = new Collection<ParameterDescription>();

    public Collection<ParameterDescription> Properties { get; private set; }
  }
}
