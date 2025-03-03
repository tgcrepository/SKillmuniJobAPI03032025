// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.TextSample
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;

namespace m2ostnextservice.Areas.HelpPage
{
  public class TextSample
  {
    public TextSample(string text) => this.Text = text != null ? text : throw new ArgumentNullException(nameof (text));

    public string Text { get; private set; }

    public override bool Equals(object obj) => obj is TextSample textSample && this.Text == textSample.Text;

    public override int GetHashCode() => this.Text.GetHashCode();

    public override string ToString() => this.Text;
  }
}
