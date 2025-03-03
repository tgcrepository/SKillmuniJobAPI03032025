// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ModelDescriptions.ModelNameAttribute
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Areas.HelpPage.ModelDescriptions
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
  public sealed class ModelNameAttribute : Attribute
  {
    public ModelNameAttribute(string name) => this.Name = name;

    public string Name { get; private set; }
  }
}
