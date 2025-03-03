// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ModelDescriptions.ModelNameHelper
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace m2ostnextservice.Areas.HelpPage.ModelDescriptions
{
  internal static class ModelNameHelper
  {
    public static string GetModelName(Type type)
    {
      ModelNameAttribute customAttribute = type.GetCustomAttribute<ModelNameAttribute>();
      if (customAttribute != null && !string.IsNullOrEmpty(customAttribute.Name))
        return customAttribute.Name;
      string modelName = type.Name;
      if (type.IsGenericType)
      {
        Type genericTypeDefinition = type.GetGenericTypeDefinition();
        Type[] genericArguments = type.GetGenericArguments();
        string name = genericTypeDefinition.Name;
        modelName = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}Of{1}", (object) name.Substring(0, name.IndexOf('`')), (object) string.Join("And", ((IEnumerable<Type>) genericArguments).Select<Type, string>((Func<Type, string>) (t => ModelNameHelper.GetModelName(t))).ToArray<string>()));
      }
      return modelName;
    }
  }
}
