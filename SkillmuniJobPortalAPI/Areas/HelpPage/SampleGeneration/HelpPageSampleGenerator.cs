// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.HelpPageSampleGenerator
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http.Description;
using System.Xml.Linq;

namespace m2ostnextservice.Areas.HelpPage
{
  public class HelpPageSampleGenerator
  {
    public HelpPageSampleGenerator()
    {
      this.ActualHttpMessageTypes = (IDictionary<HelpPageSampleKey, Type>) new Dictionary<HelpPageSampleKey, Type>();
      this.ActionSamples = (IDictionary<HelpPageSampleKey, object>) new Dictionary<HelpPageSampleKey, object>();
      this.SampleObjects = (IDictionary<Type, object>) new Dictionary<Type, object>();
      this.SampleObjectFactories = (IList<Func<HelpPageSampleGenerator, Type, object>>) new List<Func<HelpPageSampleGenerator, Type, object>>()
      {
        new Func<HelpPageSampleGenerator, Type, object>(HelpPageSampleGenerator.DefaultSampleObjectFactory)
      };
    }

    public IDictionary<HelpPageSampleKey, Type> ActualHttpMessageTypes { get; internal set; }

    public IDictionary<HelpPageSampleKey, object> ActionSamples { get; internal set; }

    public IDictionary<Type, object> SampleObjects { get; internal set; }

    public IList<Func<HelpPageSampleGenerator, Type, object>> SampleObjectFactories { get; private set; }

    public IDictionary<MediaTypeHeaderValue, object> GetSampleRequests(ApiDescription api) => this.GetSample(api, SampleDirection.Request);

    public IDictionary<MediaTypeHeaderValue, object> GetSampleResponses(ApiDescription api) => this.GetSample(api, SampleDirection.Response);

    public virtual IDictionary<MediaTypeHeaderValue, object> GetSample(
      ApiDescription api,
      SampleDirection sampleDirection)
    {
      if (api == null)
        throw new ArgumentNullException(nameof (api));
      string controllerName = api.ActionDescriptor.ControllerDescriptor.ControllerName;
      string actionName = api.ActionDescriptor.ActionName;
      IEnumerable<string> parameterNames = api.ParameterDescriptions.Select<ApiParameterDescription, string>((Func<ApiParameterDescription, string>) (p => p.Name));
      Collection<MediaTypeFormatter> formatters;
      Type type = this.ResolveType(api, controllerName, actionName, parameterNames, sampleDirection, out formatters);
      Dictionary<MediaTypeHeaderValue, object> sample1 = new Dictionary<MediaTypeHeaderValue, object>();
      foreach (KeyValuePair<HelpPageSampleKey, object> allActionSample in this.GetAllActionSamples(controllerName, actionName, parameterNames, sampleDirection))
        sample1.Add(allActionSample.Key.MediaType, HelpPageSampleGenerator.WrapSampleIfString(allActionSample.Value));
      if (type != (Type) null && !typeof (HttpResponseMessage).IsAssignableFrom(type))
      {
        object sampleObject = this.GetSampleObject(type);
        foreach (MediaTypeFormatter formatter in formatters)
        {
          foreach (MediaTypeHeaderValue supportedMediaType in formatter.SupportedMediaTypes)
          {
            if (!sample1.ContainsKey(supportedMediaType))
            {
              object sample2 = this.GetActionSample(controllerName, actionName, parameterNames, type, formatter, supportedMediaType, sampleDirection);
              if (sample2 == null && sampleObject != null)
                sample2 = this.WriteSampleObjectUsingFormatter(formatter, sampleObject, type, supportedMediaType);
              sample1.Add(supportedMediaType, HelpPageSampleGenerator.WrapSampleIfString(sample2));
            }
          }
        }
      }
      return (IDictionary<MediaTypeHeaderValue, object>) sample1;
    }

    public virtual object GetActionSample(
      string controllerName,
      string actionName,
      IEnumerable<string> parameterNames,
      Type type,
      MediaTypeFormatter formatter,
      MediaTypeHeaderValue mediaType,
      SampleDirection sampleDirection)
    {
      object actionSample;
      if (!this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, parameterNames), out actionSample))
      {
        if (!this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, sampleDirection, controllerName, actionName, (IEnumerable<string>) new string[1]
        {
          "*"
        }), out actionSample) && !this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType, type), out actionSample) && !this.ActionSamples.TryGetValue(new HelpPageSampleKey(mediaType), out actionSample))
          return (object) null;
      }
      return actionSample;
    }

    public virtual object GetSampleObject(Type type)
    {
      object sampleObject;
      if (!this.SampleObjects.TryGetValue(type, out sampleObject))
      {
        foreach (Func<HelpPageSampleGenerator, Type, object> sampleObjectFactory in (IEnumerable<Func<HelpPageSampleGenerator, Type, object>>) this.SampleObjectFactories)
        {
          if (sampleObjectFactory != null)
          {
            try
            {
              sampleObject = sampleObjectFactory(this, type);
              if (sampleObject != null)
                break;
            }
            catch
            {
            }
          }
        }
      }
      return sampleObject;
    }

    public virtual Type ResolveHttpRequestMessageType(ApiDescription api)
    {
      string controllerName = api.ActionDescriptor.ControllerDescriptor.ControllerName;
      string actionName = api.ActionDescriptor.ActionName;
      IEnumerable<string> parameterNames = api.ParameterDescriptions.Select<ApiParameterDescription, string>((Func<ApiParameterDescription, string>) (p => p.Name));
      return this.ResolveType(api, controllerName, actionName, parameterNames, SampleDirection.Request, out Collection<MediaTypeFormatter> _);
    }

    public virtual Type ResolveType(
      ApiDescription api,
      string controllerName,
      string actionName,
      IEnumerable<string> parameterNames,
      SampleDirection sampleDirection,
      out Collection<MediaTypeFormatter> formatters)
    {
      if (!System.Enum.IsDefined(typeof (SampleDirection), (object) sampleDirection))
        throw new InvalidEnumArgumentException(nameof (sampleDirection), (int) sampleDirection, typeof (SampleDirection));
      if (api == null)
        throw new ArgumentNullException(nameof (api));
      Type type1;
      if (!this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, parameterNames), out type1))
      {
        if (!this.ActualHttpMessageTypes.TryGetValue(new HelpPageSampleKey(sampleDirection, controllerName, actionName, (IEnumerable<string>) new string[1]
        {
          "*"
        }), out type1))
        {
          if (sampleDirection != SampleDirection.Request)
          {
            if (sampleDirection == SampleDirection.Response)
              ;
            Type type2 = api.ResponseDescription.ResponseType;
            if ((object) type2 == null)
              type2 = api.ResponseDescription.DeclaredType;
            type1 = type2;
            formatters = api.SupportedResponseFormatters;
            goto label_21;
          }
          else
          {
            ApiParameterDescription parameterDescription = api.ParameterDescriptions.FirstOrDefault<ApiParameterDescription>((Func<ApiParameterDescription, bool>) (p => p.Source == ApiParameterSource.FromBody));
            type1 = parameterDescription == null ? (Type) null : parameterDescription.ParameterDescriptor.ParameterType;
            formatters = api.SupportedRequestBodyFormatters;
            goto label_21;
          }
        }
      }
      Collection<MediaTypeFormatter> collection = new Collection<MediaTypeFormatter>();
      foreach (MediaTypeFormatter formatter in (Collection<MediaTypeFormatter>) api.ActionDescriptor.Configuration.Formatters)
      {
        if (HelpPageSampleGenerator.IsFormatSupported(sampleDirection, formatter, type1))
          collection.Add(formatter);
      }
      formatters = collection;
label_21:
      return type1;
    }

    public virtual object WriteSampleObjectUsingFormatter(
      MediaTypeFormatter formatter,
      object value,
      Type type,
      MediaTypeHeaderValue mediaType)
    {
      if (formatter == null)
        throw new ArgumentNullException(nameof (formatter));
      if (mediaType == null)
        throw new ArgumentNullException(nameof (mediaType));
      object obj = (object) string.Empty;
      MemoryStream writeStream = (MemoryStream) null;
      HttpContent content = (HttpContent) null;
      try
      {
        if (formatter.CanWriteType(type))
        {
          writeStream = new MemoryStream();
          content = (HttpContent) new ObjectContent(type, value, formatter, mediaType);
          formatter.WriteToStreamAsync(type, value, (Stream) writeStream, content, (TransportContext) null).Wait();
          writeStream.Position = 0L;
          string str = new StreamReader((Stream) writeStream).ReadToEnd();
          if (mediaType.MediaType.ToUpperInvariant().Contains("XML"))
            str = HelpPageSampleGenerator.TryFormatXml(str);
          else if (mediaType.MediaType.ToUpperInvariant().Contains("JSON"))
            str = HelpPageSampleGenerator.TryFormatJson(str);
          obj = (object) new TextSample(str);
        }
        else
          obj = (object) new InvalidSample(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Failed to generate the sample for media type '{0}'. Cannot use formatter '{1}' to write type '{2}'.", (object) mediaType, (object) formatter.GetType().Name, (object) type.Name));
      }
      catch (Exception ex)
      {
        obj = (object) new InvalidSample(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "An exception has occurred while using the formatter '{0}' to generate sample for media type '{1}'. Exception message: {2}", (object) formatter.GetType().Name, (object) mediaType.MediaType, (object) HelpPageSampleGenerator.UnwrapException(ex).Message));
      }
      finally
      {
        writeStream?.Dispose();
        content?.Dispose();
      }
      return obj;
    }

    internal static Exception UnwrapException(Exception exception) => exception is AggregateException aggregateException ? aggregateException.Flatten().InnerException : exception;

    private static object DefaultSampleObjectFactory(
      HelpPageSampleGenerator sampleGenerator,
      Type type)
    {
      return new ObjectGenerator().GenerateObject(type);
    }

    private static string TryFormatJson(string str)
    {
      try
      {
        return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(str), Newtonsoft.Json.Formatting.Indented);
      }
      catch
      {
        return str;
      }
    }

    private static string TryFormatXml(string str)
    {
      try
      {
        return XDocument.Parse(str).ToString();
      }
      catch
      {
        return str;
      }
    }

    private static bool IsFormatSupported(
      SampleDirection sampleDirection,
      MediaTypeFormatter formatter,
      Type type)
    {
      if (sampleDirection == SampleDirection.Request)
        return formatter.CanReadType(type);
      return sampleDirection == SampleDirection.Response && formatter.CanWriteType(type);
    }

    private IEnumerable<KeyValuePair<HelpPageSampleKey, object>> GetAllActionSamples(
      string controllerName,
      string actionName,
      IEnumerable<string> parameterNames,
      SampleDirection sampleDirection)
    {
      HashSet<string> parameterNamesSet = new HashSet<string>(parameterNames, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      foreach (KeyValuePair<HelpPageSampleKey, object> actionSample in (IEnumerable<KeyValuePair<HelpPageSampleKey, object>>) this.ActionSamples)
      {
        HelpPageSampleKey key = actionSample.Key;
        if (string.Equals(controllerName, key.ControllerName, StringComparison.OrdinalIgnoreCase) && string.Equals(actionName, key.ActionName, StringComparison.OrdinalIgnoreCase))
        {
          if (key.ParameterNames.SetEquals((IEnumerable<string>) new string[1]
          {
            "*"
          }) || parameterNamesSet.SetEquals((IEnumerable<string>) key.ParameterNames))
          {
            int num = (int) sampleDirection;
            SampleDirection? sampleDirection1 = key.SampleDirection;
            int valueOrDefault = (int) sampleDirection1.GetValueOrDefault();
            if (num == valueOrDefault & sampleDirection1.HasValue)
              yield return actionSample;
          }
        }
      }
    }

    private static object WrapSampleIfString(object sample) => sample is string text ? (object) new TextSample(text) : sample;
  }
}
