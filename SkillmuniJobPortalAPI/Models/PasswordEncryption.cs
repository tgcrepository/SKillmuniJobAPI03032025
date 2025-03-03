// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.PasswordEncryption
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace m2ostnextservice.Models
{
  public static class PasswordEncryption
  {
    public static string ToMD5Hash(this byte[] bytes)
    {
      StringBuilder hash = new StringBuilder();
      ((IEnumerable<byte>) MD5.Create().ComputeHash(bytes)).ToList<byte>().ForEach((Action<byte>) (b => hash.AppendFormat("{0:x2}", (object) b)));
      return hash.ToString();
    }

    public static string ToMD5Hash(this string inputString) => Encoding.UTF8.GetBytes(inputString).ToMD5Hash();
  }
}
