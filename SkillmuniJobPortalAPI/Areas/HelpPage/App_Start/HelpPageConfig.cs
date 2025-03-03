// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.HelpPageConfig
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System.Net.Http.Headers;
using System.Web.Http;

namespace m2ostnextservice.Areas.HelpPage
{
  public static class HelpPageConfig
  {
    public static void Register(HttpConfiguration config) => config.SetSampleForMediaType((object) new TextSample("Binary JSON content. See http://bsonspec.org for details."), new MediaTypeHeaderValue("application/bson"));
  }
}
