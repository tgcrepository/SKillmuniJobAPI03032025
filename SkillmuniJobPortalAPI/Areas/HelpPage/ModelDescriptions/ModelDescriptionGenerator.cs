// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ModelDescriptions.ModelDescriptionGenerator
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Serialization;

namespace m2ostnextservice.Areas.HelpPage.ModelDescriptions
{
  public class ModelDescriptionGenerator
  {
    private readonly IDictionary<Type, Func<object, string>> AnnotationTextGenerator = (IDictionary<Type, Func<object, string>>) new Dictionary<Type, Func<object, string>>()
    {
      {
        typeof (RequiredAttribute),
        (Func<object, string>) (a => "Required")
      },
      {
        typeof (RangeAttribute),
        (Func<object, string>) (a =>
        {
          RangeAttribute rangeAttribute = (RangeAttribute) a;
          return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Range: inclusive between {0} and {1}", rangeAttribute.Minimum, rangeAttribute.Maximum);
        })
      },
      {
        typeof (MaxLengthAttribute),
        (Func<object, string>) (a => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Max length: {0}", (object) ((MaxLengthAttribute) a).Length))
      },
      {
        typeof (MinLengthAttribute),
        (Func<object, string>) (a => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Min length: {0}", (object) ((MinLengthAttribute) a).Length))
      },
      {
        typeof (StringLengthAttribute),
        (Func<object, string>) (a =>
        {
          StringLengthAttribute stringLengthAttribute = (StringLengthAttribute) a;
          return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "String length: inclusive between {0} and {1}", (object) stringLengthAttribute.MinimumLength, (object) stringLengthAttribute.MaximumLength);
        })
      },
      {
        typeof (DataTypeAttribute),
        (Func<object, string>) (a =>
        {
          DataTypeAttribute dataTypeAttribute = (DataTypeAttribute) a;
          return string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Data type: {0}", (object) (dataTypeAttribute.CustomDataType ?? dataTypeAttribute.DataType.ToString()));
        })
      },
      {
        typeof (RegularExpressionAttribute),
        (Func<object, string>) (a => string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Matching regular expression pattern: {0}", (object) ((RegularExpressionAttribute) a).Pattern))
      }
    };
    private readonly IDictionary<Type, string> DefaultTypeDocumentation = (IDictionary<Type, string>) new Dictionary<Type, string>()
    {
      {
        typeof (short),
        "integer"
      },
      {
        typeof (int),
        "integer"
      },
      {
        typeof (long),
        "integer"
      },
      {
        typeof (ushort),
        "unsigned integer"
      },
      {
        typeof (uint),
        "unsigned integer"
      },
      {
        typeof (ulong),
        "unsigned integer"
      },
      {
        typeof (byte),
        "byte"
      },
      {
        typeof (char),
        "character"
      },
      {
        typeof (sbyte),
        "signed byte"
      },
      {
        typeof (Uri),
        "URI"
      },
      {
        typeof (float),
        "decimal number"
      },
      {
        typeof (double),
        "decimal number"
      },
      {
        typeof (Decimal),
        "decimal number"
      },
      {
        typeof (string),
        "string"
      },
      {
        typeof (Guid),
        "globally unique identifier"
      },
      {
        typeof (TimeSpan),
        "time interval"
      },
      {
        typeof (DateTime),
        "date"
      },
      {
        typeof (DateTimeOffset),
        "date"
      },
      {
        typeof (bool),
        "boolean"
      }
    };
    private Lazy<IModelDocumentationProvider> _documentationProvider;

    public ModelDescriptionGenerator(HttpConfiguration config)
    {
      this._documentationProvider = config != null ? new Lazy<IModelDocumentationProvider>((Func<IModelDocumentationProvider>) (() => config.Services.GetDocumentationProvider() as IModelDocumentationProvider)) : throw new ArgumentNullException(nameof (config));
      this.GeneratedModels = new Dictionary<string, ModelDescription>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    public Dictionary<string, ModelDescription> GeneratedModels { get; private set; }

    private IModelDocumentationProvider DocumentationProvider => this._documentationProvider.Value;

    public ModelDescription GetOrCreateModelDescription(Type modelType)
    {
      Type type = !(modelType == (Type) null) ? Nullable.GetUnderlyingType(modelType) : throw new ArgumentNullException(nameof (modelType));
      if (type != (Type) null)
        modelType = type;
      string modelName = ModelNameHelper.GetModelName(modelType);
      ModelDescription modelDescription;
      if (this.GeneratedModels.TryGetValue(modelName, out modelDescription))
      {
        if (modelType != modelDescription.ModelType)
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "A model description could not be created. Duplicate model name '{0}' was found for types '{1}' and '{2}'. Use the [ModelName] attribute to change the model name for at least one of the types so that it has a unique name.", (object) modelName, (object) modelDescription.ModelType.FullName, (object) modelType.FullName));
        return modelDescription;
      }
      if (this.DefaultTypeDocumentation.ContainsKey(modelType))
        return this.GenerateSimpleTypeModelDescription(modelType);
      if (modelType.IsEnum)
        return (ModelDescription) this.GenerateEnumTypeModelDescription(modelType);
      if (modelType.IsGenericType)
      {
        Type[] genericArguments = modelType.GetGenericArguments();
        if (genericArguments.Length == 1 && typeof (IEnumerable<>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
          return (ModelDescription) this.GenerateCollectionModelDescription(modelType, genericArguments[0]);
        if (genericArguments.Length == 2)
        {
          if (typeof (IDictionary<,>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
            return (ModelDescription) this.GenerateDictionaryModelDescription(modelType, genericArguments[0], genericArguments[1]);
          if (typeof (KeyValuePair<,>).MakeGenericType(genericArguments).IsAssignableFrom(modelType))
            return (ModelDescription) this.GenerateKeyValuePairModelDescription(modelType, genericArguments[0], genericArguments[1]);
        }
      }
      if (modelType.IsArray)
      {
        Type elementType = modelType.GetElementType();
        return (ModelDescription) this.GenerateCollectionModelDescription(modelType, elementType);
      }
      if (modelType == typeof (NameValueCollection))
        return (ModelDescription) this.GenerateDictionaryModelDescription(modelType, typeof (string), typeof (string));
      if (typeof (IDictionary).IsAssignableFrom(modelType))
        return (ModelDescription) this.GenerateDictionaryModelDescription(modelType, typeof (object), typeof (object));
      return typeof (IEnumerable).IsAssignableFrom(modelType) ? (ModelDescription) this.GenerateCollectionModelDescription(modelType, typeof (object)) : this.GenerateComplexTypeModelDescription(modelType);
    }

    private static string GetMemberName(MemberInfo member, bool hasDataContractAttribute)
    {
      JsonPropertyAttribute customAttribute1 = member.GetCustomAttribute<JsonPropertyAttribute>();
      if (customAttribute1 != null && !string.IsNullOrEmpty(customAttribute1.PropertyName))
        return customAttribute1.PropertyName;
      if (hasDataContractAttribute)
      {
        DataMemberAttribute customAttribute2 = member.GetCustomAttribute<DataMemberAttribute>();
        if (customAttribute2 != null && !string.IsNullOrEmpty(customAttribute2.Name))
          return customAttribute2.Name;
      }
      return member.Name;
    }

    private static bool ShouldDisplayMember(MemberInfo member, bool hasDataContractAttribute)
    {
      JsonIgnoreAttribute customAttribute1 = member.GetCustomAttribute<JsonIgnoreAttribute>();
      XmlIgnoreAttribute customAttribute2 = member.GetCustomAttribute<XmlIgnoreAttribute>();
      IgnoreDataMemberAttribute customAttribute3 = member.GetCustomAttribute<IgnoreDataMemberAttribute>();
      NonSerializedAttribute customAttribute4 = member.GetCustomAttribute<NonSerializedAttribute>();
      ApiExplorerSettingsAttribute customAttribute5 = member.GetCustomAttribute<ApiExplorerSettingsAttribute>();
      bool flag = member.DeclaringType.IsEnum ? member.GetCustomAttribute<EnumMemberAttribute>() != null : member.GetCustomAttribute<DataMemberAttribute>() != null;
      return customAttribute1 == null && customAttribute2 == null && customAttribute3 == null && customAttribute4 == null && (customAttribute5 == null || !customAttribute5.IgnoreApi) && !hasDataContractAttribute | flag;
    }

    private string CreateDefaultDocumentation(Type type)
    {
      string documentation;
      if (this.DefaultTypeDocumentation.TryGetValue(type, out documentation) || this.DocumentationProvider == null)
        return documentation;
      documentation = this.DocumentationProvider.GetDocumentation(type);
      return documentation;
    }

    private void GenerateAnnotations(MemberInfo property, ParameterDescription propertyModel)
    {
      List<ParameterAnnotation> parameterAnnotationList = new List<ParameterAnnotation>();
      foreach (Attribute customAttribute in property.GetCustomAttributes())
      {
        Func<object, string> func;
        if (this.AnnotationTextGenerator.TryGetValue(customAttribute.GetType(), out func))
          parameterAnnotationList.Add(new ParameterAnnotation()
          {
            AnnotationAttribute = customAttribute,
            Documentation = func((object) customAttribute)
          });
      }
      parameterAnnotationList.Sort((Comparison<ParameterAnnotation>) ((x, y) =>
      {
        if (x.AnnotationAttribute is RequiredAttribute)
          return -1;
        return y.AnnotationAttribute is RequiredAttribute ? 1 : string.Compare(x.Documentation, y.Documentation, StringComparison.OrdinalIgnoreCase);
      }));
      foreach (ParameterAnnotation parameterAnnotation in parameterAnnotationList)
        propertyModel.Annotations.Add(parameterAnnotation);
    }

    private CollectionModelDescription GenerateCollectionModelDescription(
      Type modelType,
      Type elementType)
    {
      ModelDescription modelDescription1 = this.GetOrCreateModelDescription(elementType);
      if (modelDescription1 == null)
        return (CollectionModelDescription) null;
      CollectionModelDescription modelDescription2 = new CollectionModelDescription();
      modelDescription2.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription2.ModelType = modelType;
      modelDescription2.ElementDescription = modelDescription1;
      return modelDescription2;
    }

    private ModelDescription GenerateComplexTypeModelDescription(Type modelType)
    {
      ComplexTypeModelDescription modelDescription1 = new ComplexTypeModelDescription();
      modelDescription1.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription1.ModelType = modelType;
      modelDescription1.Documentation = this.CreateDefaultDocumentation(modelType);
      ComplexTypeModelDescription modelDescription2 = modelDescription1;
      this.GeneratedModels.Add(modelDescription2.Name, (ModelDescription) modelDescription2);
      bool hasDataContractAttribute = modelType.GetCustomAttribute<DataContractAttribute>() != null;
      foreach (PropertyInfo property in modelType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        if (ModelDescriptionGenerator.ShouldDisplayMember((MemberInfo) property, hasDataContractAttribute))
        {
          ParameterDescription propertyModel = new ParameterDescription()
          {
            Name = ModelDescriptionGenerator.GetMemberName((MemberInfo) property, hasDataContractAttribute)
          };
          if (this.DocumentationProvider != null)
            propertyModel.Documentation = this.DocumentationProvider.GetDocumentation((MemberInfo) property);
          this.GenerateAnnotations((MemberInfo) property, propertyModel);
          modelDescription2.Properties.Add(propertyModel);
          propertyModel.TypeDescription = this.GetOrCreateModelDescription(property.PropertyType);
        }
      }
      foreach (FieldInfo field in modelType.GetFields(BindingFlags.Instance | BindingFlags.Public))
      {
        if (ModelDescriptionGenerator.ShouldDisplayMember((MemberInfo) field, hasDataContractAttribute))
        {
          ParameterDescription parameterDescription = new ParameterDescription()
          {
            Name = ModelDescriptionGenerator.GetMemberName((MemberInfo) field, hasDataContractAttribute)
          };
          if (this.DocumentationProvider != null)
            parameterDescription.Documentation = this.DocumentationProvider.GetDocumentation((MemberInfo) field);
          modelDescription2.Properties.Add(parameterDescription);
          parameterDescription.TypeDescription = this.GetOrCreateModelDescription(field.FieldType);
        }
      }
      return (ModelDescription) modelDescription2;
    }

    private DictionaryModelDescription GenerateDictionaryModelDescription(
      Type modelType,
      Type keyType,
      Type valueType)
    {
      ModelDescription modelDescription1 = this.GetOrCreateModelDescription(keyType);
      ModelDescription modelDescription2 = this.GetOrCreateModelDescription(valueType);
      DictionaryModelDescription modelDescription3 = new DictionaryModelDescription();
      modelDescription3.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription3.ModelType = modelType;
      modelDescription3.KeyModelDescription = modelDescription1;
      modelDescription3.ValueModelDescription = modelDescription2;
      return modelDescription3;
    }

    private EnumTypeModelDescription GenerateEnumTypeModelDescription(Type modelType)
    {
      EnumTypeModelDescription modelDescription1 = new EnumTypeModelDescription();
      modelDescription1.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription1.ModelType = modelType;
      modelDescription1.Documentation = this.CreateDefaultDocumentation(modelType);
      EnumTypeModelDescription modelDescription2 = modelDescription1;
      bool hasDataContractAttribute = modelType.GetCustomAttribute<DataContractAttribute>() != null;
      foreach (FieldInfo field in modelType.GetFields(BindingFlags.Static | BindingFlags.Public))
      {
        if (ModelDescriptionGenerator.ShouldDisplayMember((MemberInfo) field, hasDataContractAttribute))
        {
          EnumValueDescription valueDescription = new EnumValueDescription()
          {
            Name = field.Name,
            Value = field.GetRawConstantValue().ToString()
          };
          if (this.DocumentationProvider != null)
            valueDescription.Documentation = this.DocumentationProvider.GetDocumentation((MemberInfo) field);
          modelDescription2.Values.Add(valueDescription);
        }
      }
      this.GeneratedModels.Add(modelDescription2.Name, (ModelDescription) modelDescription2);
      return modelDescription2;
    }

    private KeyValuePairModelDescription GenerateKeyValuePairModelDescription(
      Type modelType,
      Type keyType,
      Type valueType)
    {
      ModelDescription modelDescription1 = this.GetOrCreateModelDescription(keyType);
      ModelDescription modelDescription2 = this.GetOrCreateModelDescription(valueType);
      KeyValuePairModelDescription modelDescription3 = new KeyValuePairModelDescription();
      modelDescription3.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription3.ModelType = modelType;
      modelDescription3.KeyModelDescription = modelDescription1;
      modelDescription3.ValueModelDescription = modelDescription2;
      return modelDescription3;
    }

    private ModelDescription GenerateSimpleTypeModelDescription(Type modelType)
    {
      SimpleTypeModelDescription modelDescription1 = new SimpleTypeModelDescription();
      modelDescription1.Name = ModelNameHelper.GetModelName(modelType);
      modelDescription1.ModelType = modelType;
      modelDescription1.Documentation = this.CreateDefaultDocumentation(modelType);
      SimpleTypeModelDescription modelDescription2 = modelDescription1;
      this.GeneratedModels.Add(modelDescription2.Name, (ModelDescription) modelDescription2);
      return (ModelDescription) modelDescription2;
    }
  }
}
