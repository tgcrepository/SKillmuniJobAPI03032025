// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ApiDescriptionExtensions
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Text;
using System.Web;
using System.Web.Http.Description;

namespace m2ostnextservice.Areas.HelpPage
{
  public static class ApiDescriptionExtensions
  {
    public static string GetFriendlyId(this ApiDescription description)
    {
      string[] strArray = description.RelativePath.Split('?');
      string str1 = strArray[0];
      string str2 = (string) null;
      if (strArray.Length > 1)
        str2 = string.Join("_", HttpUtility.ParseQueryString(strArray[1]).AllKeys);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("{0}-{1}", (object) description.HttpMethod.Method, (object) str1.Replace("/", "-").Replace("{", string.Empty).Replace("}", string.Empty));
      if (str2 != null)
        stringBuilder.AppendFormat("_{0}", (object) str2.Replace('.', '-'));
      return stringBuilder.ToString();
    }
  }
}
