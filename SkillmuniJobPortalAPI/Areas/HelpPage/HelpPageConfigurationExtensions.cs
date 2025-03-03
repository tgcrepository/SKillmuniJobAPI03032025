// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.HelpPageConfigurationExtensions
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Areas.HelpPage.ModelDescriptions;
using m2ostnextservice.Areas.HelpPage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace m2ostnextservice.Areas.HelpPage
{
  public static class HelpPageConfigurationExtensions
  {
    private const string ApiModelPrefix = "MS_HelpPageApiModel_";

    public static void SetDocumentationProvider(
      this HttpConfiguration config,
      IDocumentationProvider documentationProvider)
    {
      config.Services.Replace(typeof (IDocumentationProvider), (object) documentationProvider);
    }

    public static void SetSampleObjects(
      this HttpConfiguration config,
      IDictionary<Type, object> sampleObjects)
    {
      config.GetHelpPageSampleGenerator().SampleObjects = sampleObjects;
    }

    public static void SetSampleRequest(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType,
      string controllerName,
      string actionName)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Request, controllerName, actionName, (IEnumerable<string>) new string[1]
      {
        "*"
      }), sample);
    }

    public static void SetSampleRequest(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType,
      string controllerName,
      string actionName,
      params string[] parameterNames)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Request, controllerName, actionName, (IEnumerable<string>) parameterNames), sample);
    }

    public static void SetSampleResponse(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType,
      string controllerName,
      string actionName)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Response, controllerName, actionName, (IEnumerable<string>) new string[1]
      {
        "*"
      }), sample);
    }

    public static void SetSampleResponse(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType,
      string controllerName,
      string actionName,
      params string[] parameterNames)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, SampleDirection.Response, controllerName, actionName, (IEnumerable<string>) parameterNames), sample);
    }

    public static void SetSampleForMediaType(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType), sample);
    }

    public static void SetSampleForType(
      this HttpConfiguration config,
      object sample,
      MediaTypeHeaderValue mediaType,
      Type type)
    {
      config.GetHelpPageSampleGenerator().ActionSamples.Add(new HelpPageSampleKey(mediaType, type), sample);
    }

    public static void SetActualRequestType(
      this HttpConfiguration config,
      Type type,
      string controllerName,
      string actionName)
    {
      config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Request, controllerName, actionName, (IEnumerable<string>) new string[1]
      {
        "*"
      }), type);
    }

    public static void SetActualRequestType(
      this HttpConfiguration config,
      Type type,
      string controllerName,
      string actionName,
      params string[] parameterNames)
    {
      config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Request, controllerName, actionName, (IEnumerable<string>) parameterNames), type);
    }

    public static void SetActualResponseType(
      this HttpConfiguration config,
      Type type,
      string controllerName,
      string actionName)
    {
      config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Response, controllerName, actionName, (IEnumerable<string>) new string[1]
      {
        "*"
      }), type);
    }

    public static void SetActualResponseType(
      this HttpConfiguration config,
      Type type,
      string controllerName,
      string actionName,
      params string[] parameterNames)
    {
      config.GetHelpPageSampleGenerator().ActualHttpMessageTypes.Add(new HelpPageSampleKey(SampleDirection.Response, controllerName, actionName, (IEnumerable<string>) parameterNames), type);
    }

    public static HelpPageSampleGenerator GetHelpPageSampleGenerator(this HttpConfiguration config) => (HelpPageSampleGenerator) config.Properties.GetOrAdd((object) typeof (HelpPageSampleGenerator), (Func<object, object>) (k => (object) new HelpPageSampleGenerator()));

    public static void SetHelpPageSampleGenerator(
      this HttpConfiguration config,
      HelpPageSampleGenerator sampleGenerator)
    {
      config.Properties.AddOrUpdate((object) typeof (HelpPageSampleGenerator), (Func<object, object>) (k => (object) sampleGenerator), (Func<object, object, object>) ((k, o) => (object) sampleGenerator));
    }

    public static ModelDescriptionGenerator GetModelDescriptionGenerator(
      this HttpConfiguration config)
    {
      return (ModelDescriptionGenerator) config.Properties.GetOrAdd((object) typeof (ModelDescriptionGenerator), (Func<object, object>) (k => (object) HelpPageConfigurationExtensions.InitializeModelDescriptionGenerator(config)));
    }

    public static HelpPageApiModel GetHelpPageApiModel(
      this HttpConfiguration config,
      string apiDescriptionId)
    {
      string key = "MS_HelpPageApiModel_" + apiDescriptionId;
      object apiModel;
      if (!config.Properties.TryGetValue((object) key, out apiModel))
      {
        ApiDescription apiDescription = config.Services.GetApiExplorer().ApiDescriptions.FirstOrDefault<ApiDescription>((Func<ApiDescription, bool>) (api => string.Equals(api.GetFriendlyId(), apiDescriptionId, StringComparison.OrdinalIgnoreCase)));
        if (apiDescription != null)
        {
          apiModel = (object) HelpPageConfigurationExtensions.GenerateApiModel(apiDescription, config);
          config.Properties.TryAdd((object) key, apiModel);
        }
      }
      return (HelpPageApiModel) apiModel;
    }

    private static HelpPageApiModel GenerateApiModel(
      ApiDescription apiDescription,
      HttpConfiguration config)
    {
      HelpPageApiModel apiModel = new HelpPageApiModel()
      {
        ApiDescription = apiDescription
      };
      ModelDescriptionGenerator descriptionGenerator = config.GetModelDescriptionGenerator();
      HelpPageSampleGenerator pageSampleGenerator = config.GetHelpPageSampleGenerator();
      HelpPageConfigurationExtensions.GenerateUriParameters(apiModel, descriptionGenerator);
      HelpPageConfigurationExtensions.GenerateRequestModelDescription(apiModel, descriptionGenerator, pageSampleGenerator);
      HelpPageConfigurationExtensions.GenerateResourceDescription(apiModel, descriptionGenerator);
      HelpPageConfigurationExtensions.GenerateSamples(apiModel, pageSampleGenerator);
      return apiModel;
    }

    private static void GenerateUriParameters(
      HelpPageApiModel apiModel,
      ModelDescriptionGenerator modelGenerator)
    {
      foreach (ApiParameterDescription parameterDescription1 in apiModel.ApiDescription.ParameterDescriptions)
      {
        if (parameterDescription1.Source == ApiParameterSource.FromUri)
        {
          HttpParameterDescriptor parameterDescriptor = parameterDescription1.ParameterDescriptor;
          Type type = (Type) null;
          ModelDescription typeDescription = (ModelDescription) null;
          ComplexTypeModelDescription modelDescription1 = (ComplexTypeModelDescription) null;
          if (parameterDescriptor != null)
          {
            type = parameterDescriptor.ParameterType;
            typeDescription = modelGenerator.GetOrCreateModelDescription(type);
            modelDescription1 = typeDescription as ComplexTypeModelDescription;
          }
          if (modelDescription1 != null && !HelpPageConfigurationExtensions.IsBindableWithTypeConverter(type))
          {
            foreach (ParameterDescription property in modelDescription1.Properties)
              apiModel.UriParameters.Add(property);
          }
          else if (parameterDescriptor != null)
          {
            ParameterDescription parameterDescription2 = HelpPageConfigurationExtensions.AddParameterDescription(apiModel, parameterDescription1, typeDescription);
            if (!parameterDescriptor.IsOptional)
              parameterDescription2.Annotations.Add(new ParameterAnnotation()
              {
                Documentation = "Required"
              });
            object defaultValue = parameterDescriptor.DefaultValue;
            if (defaultValue != null)
              parameterDescription2.Annotations.Add(new ParameterAnnotation()
              {
                Documentation = "Default value is " + Convert.ToString(defaultValue, (IFormatProvider) CultureInfo.InvariantCulture)
              });
          }
          else
          {
            ModelDescription modelDescription2 = modelGenerator.GetOrCreateModelDescription(typeof (string));
            HelpPageConfigurationExtensions.AddParameterDescription(apiModel, parameterDescription1, modelDescription2);
          }
        }
      }
    }

    private static bool IsBindableWithTypeConverter(Type parameterType) => !(parameterType == (Type) null) && TypeDescriptor.GetConverter(parameterType).CanConvertFrom(typeof (string));

    private static ParameterDescription AddParameterDescription(
      HelpPageApiModel apiModel,
      ApiParameterDescription apiParameter,
      ModelDescription typeDescription)
    {
      ParameterDescription parameterDescription = new ParameterDescription()
      {
        Name = apiParameter.Name,
        Documentation = apiParameter.Documentation,
        TypeDescription = typeDescription
      };
      apiModel.UriParameters.Add(parameterDescription);
      return parameterDescription;
    }

    private static void GenerateRequestModelDescription(
      HelpPageApiModel apiModel,
      ModelDescriptionGenerator modelGenerator,
      HelpPageSampleGenerator sampleGenerator)
    {
      ApiDescription apiDescription = apiModel.ApiDescription;
      foreach (ApiParameterDescription parameterDescription in apiDescription.ParameterDescriptions)
      {
        if (parameterDescription.Source == ApiParameterSource.FromBody)
        {
          Type parameterType = parameterDescription.ParameterDescriptor.ParameterType;
          apiModel.RequestModelDescription = modelGenerator.GetOrCreateModelDescription(parameterType);
          apiModel.RequestDocumentation = parameterDescription.Documentation;
        }
        else if (parameterDescription.ParameterDescriptor != null && parameterDescription.ParameterDescriptor.ParameterType == typeof (HttpRequestMessage))
        {
          Type modelType = sampleGenerator.ResolveHttpRequestMessageType(apiDescription);
          if (modelType != (Type) null)
            apiModel.RequestModelDescription = modelGenerator.GetOrCreateModelDescription(modelType);
        }
      }
    }

    private static void GenerateResourceDescription(
      HelpPageApiModel apiModel,
      ModelDescriptionGenerator modelGenerator)
    {
      ResponseDescription responseDescription = apiModel.ApiDescription.ResponseDescription;
      Type type = responseDescription.ResponseType;
      if ((object) type == null)
        type = responseDescription.DeclaredType;
      Type modelType = type;
      if (!(modelType != (Type) null) || !(modelType != typeof (void)))
        return;
      apiModel.ResourceDescription = modelGenerator.GetOrCreateModelDescription(modelType);
    }

    private static void GenerateSamples(
      HelpPageApiModel apiModel,
      HelpPageSampleGenerator sampleGenerator)
    {
      try
      {
        foreach (KeyValuePair<MediaTypeHeaderValue, object> sampleRequest in (IEnumerable<KeyValuePair<MediaTypeHeaderValue, object>>) sampleGenerator.GetSampleRequests(apiModel.ApiDescription))
        {
          apiModel.SampleRequests.Add(sampleRequest.Key, sampleRequest.Value);
          HelpPageConfigurationExtensions.LogInvalidSampleAsError(apiModel, sampleRequest.Value);
        }
        foreach (KeyValuePair<MediaTypeHeaderValue, object> sampleResponse in (IEnumerable<KeyValuePair<MediaTypeHeaderValue, object>>) sampleGenerator.GetSampleResponses(apiModel.ApiDescription))
        {
          apiModel.SampleResponses.Add(sampleResponse.Key, sampleResponse.Value);
          HelpPageConfigurationExtensions.LogInvalidSampleAsError(apiModel, sampleResponse.Value);
        }
      }
      catch (Exception ex)
      {
        apiModel.ErrorMessages.Add(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "An exception has occurred while generating the sample. Exception message: {0}", (object) HelpPageSampleGenerator.UnwrapException(ex).Message));
      }
    }

    private static bool TryGetResourceParameter(
      ApiDescription apiDescription,
      HttpConfiguration config,
      out ApiParameterDescription parameterDescription,
      out Type resourceType)
    {
      parameterDescription = apiDescription.ParameterDescriptions.FirstOrDefault<ApiParameterDescription>((Func<ApiParameterDescription, bool>) (p =>
      {
        if (p.Source == ApiParameterSource.FromBody)
          return true;
        return p.ParameterDescriptor != null && p.ParameterDescriptor.ParameterType == typeof (HttpRequestMessage);
      }));
      if (parameterDescription == null)
      {
        resourceType = (Type) null;
        return false;
      }
      resourceType = parameterDescription.ParameterDescriptor.ParameterType;
      if (resourceType == typeof (HttpRequestMessage))
      {
        HelpPageSampleGenerator pageSampleGenerator = config.GetHelpPageSampleGenerator();
        resourceType = pageSampleGenerator.ResolveHttpRequestMessageType(apiDescription);
      }
      if (!(resourceType == (Type) null))
        return true;
      parameterDescription = (ApiParameterDescription) null;
      return false;
    }

    private static ModelDescriptionGenerator InitializeModelDescriptionGenerator(
      HttpConfiguration config)
    {
      ModelDescriptionGenerator descriptionGenerator = new ModelDescriptionGenerator(config);
      foreach (ApiDescription apiDescription in config.Services.GetApiExplorer().ApiDescriptions)
      {
        Type resourceType;
        if (HelpPageConfigurationExtensions.TryGetResourceParameter(apiDescription, config, out ApiParameterDescription _, out resourceType))
          descriptionGenerator.GetOrCreateModelDescription(resourceType);
      }
      return descriptionGenerator;
    }

    private static void LogInvalidSampleAsError(HelpPageApiModel apiModel, object sample)
    {
      if (!(sample is InvalidSample invalidSample))
        return;
      apiModel.ErrorMessages.Add(invalidSample.ErrorMessage);
    }
  }
}
