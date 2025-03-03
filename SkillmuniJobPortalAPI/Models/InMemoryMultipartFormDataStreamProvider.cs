// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Models.InMemoryMultipartFormDataStreamProvider
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace m2ostnextservice.Models
{
  public class InMemoryMultipartFormDataStreamProvider : MultipartStreamProvider
  {
    private NameValueCollection _formData = new NameValueCollection();
    private List<HttpContent> _fileContents = new List<HttpContent>();
    private Collection<bool> _isFormData = new Collection<bool>();

    public NameValueCollection FormData => this._formData;

    public List<HttpContent> Files => this._fileContents;

    public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
    {
      this._isFormData.Add(string.IsNullOrEmpty((headers.ContentDisposition ?? throw new InvalidOperationException(string.Format("Did not find required '{0}' header field in MIME multipart body part..", (object) "Content-Disposition"))).FileName));
      return (Stream) new MemoryStream();
    }

    public override async Task ExecutePostProcessingAsync()
    {
      InMemoryMultipartFormDataStreamProvider dataStreamProvider = this;
      for (int index = 0; index < dataStreamProvider.Contents.Count; ++index)
      {
        if (dataStreamProvider._isFormData[index])
        {
          HttpContent content = dataStreamProvider.Contents[index];
          string formFieldName = InMemoryMultipartFormDataStreamProvider.UnquoteToken(content.Headers.ContentDisposition.Name) ?? string.Empty;
          string str = await content.ReadAsStringAsync();
          dataStreamProvider.FormData.Add(formFieldName, str);
          formFieldName = (string) null;
        }
        else
          dataStreamProvider._fileContents.Add(dataStreamProvider.Contents[index]);
      }
    }

    private static string UnquoteToken(string token) => string.IsNullOrWhiteSpace(token) || !token.StartsWith("\"", StringComparison.Ordinal) || !token.EndsWith("\"", StringComparison.Ordinal) || token.Length <= 1 ? token : token.Substring(1, token.Length - 2);
  }
}
