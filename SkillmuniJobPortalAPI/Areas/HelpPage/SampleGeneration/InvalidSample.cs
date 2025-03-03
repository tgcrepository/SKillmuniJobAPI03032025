// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.InvalidSample
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Areas.HelpPage
{
  public class InvalidSample
  {
    public InvalidSample(string errorMessage) => this.ErrorMessage = errorMessage != null ? errorMessage : throw new ArgumentNullException(nameof (errorMessage));

    public string ErrorMessage { get; private set; }

    public override bool Equals(object obj) => obj is InvalidSample invalidSample && this.ErrorMessage == invalidSample.ErrorMessage;

    public override int GetHashCode() => this.ErrorMessage.GetHashCode();

    public override string ToString() => this.ErrorMessage;
  }
}
