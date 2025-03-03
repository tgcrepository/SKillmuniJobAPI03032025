// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.Models.HelpPageApiModel
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Areas.HelpPage.ModelDescriptions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Web.Http.Description;

namespace m2ostnextservice.Areas.HelpPage.Models
{
  public class HelpPageApiModel
  {
    public HelpPageApiModel()
    {
      this.UriParameters = new Collection<ParameterDescription>();
      this.SampleRequests = (IDictionary<MediaTypeHeaderValue, object>) new Dictionary<MediaTypeHeaderValue, object>();
      this.SampleResponses = (IDictionary<MediaTypeHeaderValue, object>) new Dictionary<MediaTypeHeaderValue, object>();
      this.ErrorMessages = new Collection<string>();
    }

    public ApiDescription ApiDescription { get; set; }

    public Collection<ParameterDescription> UriParameters { get; private set; }

    public string RequestDocumentation { get; set; }

    public ModelDescription RequestModelDescription { get; set; }

    public IList<ParameterDescription> RequestBodyParameters => HelpPageApiModel.GetParameterDescriptions(this.RequestModelDescription);

    public ModelDescription ResourceDescription { get; set; }

    public IList<ParameterDescription> ResourceProperties => HelpPageApiModel.GetParameterDescriptions(this.ResourceDescription);

    public IDictionary<MediaTypeHeaderValue, object> SampleRequests { get; private set; }

    public IDictionary<MediaTypeHeaderValue, object> SampleResponses { get; private set; }

    public Collection<string> ErrorMessages { get; private set; }

    private static IList<ParameterDescription> GetParameterDescriptions(
      ModelDescription modelDescription)
    {
      switch (modelDescription)
      {
        case ComplexTypeModelDescription modelDescription1:
          return (IList<ParameterDescription>) modelDescription1.Properties;
        case CollectionModelDescription modelDescription2:
          if (modelDescription2.ElementDescription is ComplexTypeModelDescription elementDescription)
            return (IList<ParameterDescription>) elementDescription.Properties;
          break;
      }
      return (IList<ParameterDescription>) null;
    }
  }
}
