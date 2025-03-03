// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.PasswordGeneration
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Text;

namespace m2ostnextservice.Models
{
  public class PasswordGeneration
  {
    private int RandomNumber(int min, int max) => new Random().Next(min, max);

    private string RandomString(int size, bool lowerCase)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Random random = new Random();
      for (int index = 0; index < size; ++index)
      {
        char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0)));
        stringBuilder.Append(ch);
      }
      return lowerCase ? stringBuilder.ToString().ToLower() : stringBuilder.ToString();
    }

    public string GetPassword()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(this.RandomString(4, true));
      stringBuilder.Append(this.RandomNumber(1000, 9999));
      stringBuilder.Append(this.RandomString(2, false));
      return stringBuilder.ToString();
    }
  }
}
