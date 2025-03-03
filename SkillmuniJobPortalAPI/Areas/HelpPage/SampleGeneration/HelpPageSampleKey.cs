// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.HelpPageSampleKey
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace m2ostnextservice.Areas.HelpPage
{
  public class HelpPageSampleKey
  {
    public HelpPageSampleKey(MediaTypeHeaderValue mediaType)
    {
      if (mediaType == null)
        throw new ArgumentNullException(nameof (mediaType));
      this.ActionName = string.Empty;
      this.ControllerName = string.Empty;
      this.MediaType = mediaType;
      this.ParameterNames = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    public HelpPageSampleKey(MediaTypeHeaderValue mediaType, Type type)
      : this(mediaType)
    {
      this.ParameterType = !(type == (Type) null) ? type : throw new ArgumentNullException(nameof (type));
    }

    public HelpPageSampleKey(
      m2ostnextservice.Areas.HelpPage.SampleDirection sampleDirection,
      string controllerName,
      string actionName,
      IEnumerable<string> parameterNames)
    {
      if (!Enum.IsDefined(typeof (m2ostnextservice.Areas.HelpPage.SampleDirection), (object) sampleDirection))
        throw new InvalidEnumArgumentException(nameof (sampleDirection), (int) sampleDirection, typeof (m2ostnextservice.Areas.HelpPage.SampleDirection));
      if (controllerName == null)
        throw new ArgumentNullException(nameof (controllerName));
      if (actionName == null)
        throw new ArgumentNullException(nameof (actionName));
      if (parameterNames == null)
        throw new ArgumentNullException(nameof (parameterNames));
      this.ControllerName = controllerName;
      this.ActionName = actionName;
      this.ParameterNames = new HashSet<string>(parameterNames, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.SampleDirection = new m2ostnextservice.Areas.HelpPage.SampleDirection?(sampleDirection);
    }

    public HelpPageSampleKey(
      MediaTypeHeaderValue mediaType,
      m2ostnextservice.Areas.HelpPage.SampleDirection sampleDirection,
      string controllerName,
      string actionName,
      IEnumerable<string> parameterNames)
      : this(sampleDirection, controllerName, actionName, parameterNames)
    {
      this.MediaType = mediaType != null ? mediaType : throw new ArgumentNullException(nameof (mediaType));
    }

    public string ControllerName { get; private set; }

    public string ActionName { get; private set; }

    public MediaTypeHeaderValue MediaType { get; private set; }

    public HashSet<string> ParameterNames { get; private set; }

    public Type ParameterType { get; private set; }

    public m2ostnextservice.Areas.HelpPage.SampleDirection? SampleDirection { get; private set; }

    public override bool Equals(object obj)
    {
      if (!(obj is HelpPageSampleKey helpPageSampleKey) || !string.Equals(this.ControllerName, helpPageSampleKey.ControllerName, StringComparison.OrdinalIgnoreCase) || !string.Equals(this.ActionName, helpPageSampleKey.ActionName, StringComparison.OrdinalIgnoreCase) || this.MediaType != helpPageSampleKey.MediaType && (this.MediaType == null || !this.MediaType.Equals((object) helpPageSampleKey.MediaType)) || !(this.ParameterType == helpPageSampleKey.ParameterType))
        return false;
      m2ostnextservice.Areas.HelpPage.SampleDirection? sampleDirection1 = this.SampleDirection;
      m2ostnextservice.Areas.HelpPage.SampleDirection? sampleDirection2 = helpPageSampleKey.SampleDirection;
      return sampleDirection1.GetValueOrDefault() == sampleDirection2.GetValueOrDefault() & sampleDirection1.HasValue == sampleDirection2.HasValue && this.ParameterNames.SetEquals((IEnumerable<string>) helpPageSampleKey.ParameterNames);
    }

    public override int GetHashCode()
    {
      int hashCode1 = this.ControllerName.ToUpperInvariant().GetHashCode() ^ this.ActionName.ToUpperInvariant().GetHashCode();
      if (this.MediaType != null)
        hashCode1 ^= this.MediaType.GetHashCode();
      m2ostnextservice.Areas.HelpPage.SampleDirection? sampleDirection = this.SampleDirection;
      if (sampleDirection.HasValue)
      {
        int num = hashCode1;
        sampleDirection = this.SampleDirection;
        int hashCode2 = sampleDirection.GetHashCode();
        hashCode1 = num ^ hashCode2;
      }
      if (this.ParameterType != (Type) null)
        hashCode1 ^= this.ParameterType.GetHashCode();
      foreach (string parameterName in this.ParameterNames)
        hashCode1 ^= parameterName.ToUpperInvariant().GetHashCode();
      return hashCode1;
    }
  }
}
