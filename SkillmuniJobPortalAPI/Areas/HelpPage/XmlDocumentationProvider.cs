// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.XmlDocumentationProvider
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using m2ostnextservice.Areas.HelpPage.ModelDescriptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.XPath;

namespace m2ostnextservice.Areas.HelpPage
{
  public class XmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
  {
    private XPathNavigator _documentNavigator;
    private const string TypeExpression = "/doc/members/member[@name='T:{0}']";
    private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
    private const string PropertyExpression = "/doc/members/member[@name='P:{0}']";
    private const string FieldExpression = "/doc/members/member[@name='F:{0}']";
    private const string ParameterExpression = "param[@name='{0}']";

    public XmlDocumentationProvider(string documentPath) => this._documentNavigator = documentPath != null ? new XPathDocument(documentPath).CreateNavigator() : throw new ArgumentNullException(nameof (documentPath));

    public string GetDocumentation(HttpControllerDescriptor controllerDescriptor) => XmlDocumentationProvider.GetTagValue(this.GetTypeNode(controllerDescriptor.ControllerType), "summary");

    public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor) => XmlDocumentationProvider.GetTagValue(this.GetMethodNode(actionDescriptor), "summary");

    public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
    {
      if (parameterDescriptor is ReflectedHttpParameterDescriptor parameterDescriptor1)
      {
        XPathNavigator methodNode = this.GetMethodNode(parameterDescriptor1.ActionDescriptor);
        if (methodNode != null)
        {
          string name = parameterDescriptor1.ParameterInfo.Name;
          XPathNavigator xpathNavigator = methodNode.SelectSingleNode(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "param[@name='{0}']", (object) name));
          if (xpathNavigator != null)
            return xpathNavigator.Value.Trim();
        }
      }
      return (string) null;
    }

    public string GetResponseDocumentation(HttpActionDescriptor actionDescriptor) => XmlDocumentationProvider.GetTagValue(this.GetMethodNode(actionDescriptor), "returns");

    public string GetDocumentation(MemberInfo member)
    {
      string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.{1}", (object) XmlDocumentationProvider.GetTypeName(member.DeclaringType), (object) member.Name);
      return XmlDocumentationProvider.GetTagValue(this._documentNavigator.SelectSingleNode(string.Format((IFormatProvider) CultureInfo.InvariantCulture, member.MemberType == MemberTypes.Field ? "/doc/members/member[@name='F:{0}']" : "/doc/members/member[@name='P:{0}']", (object) str)), "summary");
    }

    public string GetDocumentation(Type type) => XmlDocumentationProvider.GetTagValue(this.GetTypeNode(type), "summary");

    private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor) => actionDescriptor is ReflectedHttpActionDescriptor actionDescriptor1 ? this._documentNavigator.SelectSingleNode(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "/doc/members/member[@name='M:{0}']", (object) XmlDocumentationProvider.GetMemberName(actionDescriptor1.MethodInfo))) : (XPathNavigator) null;

    private static string GetMemberName(MethodInfo method)
    {
      string memberName = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}.{1}", (object) XmlDocumentationProvider.GetTypeName(method.DeclaringType), (object) method.Name);
      ParameterInfo[] parameters = method.GetParameters();
      if (parameters.Length != 0)
      {
        string[] array = ((IEnumerable<ParameterInfo>) parameters).Select<ParameterInfo, string>((Func<ParameterInfo, string>) (param => XmlDocumentationProvider.GetTypeName(param.ParameterType))).ToArray<string>();
        memberName += string.Format((IFormatProvider) CultureInfo.InvariantCulture, "({0})", (object) string.Join(",", array));
      }
      return memberName;
    }

    private static string GetTagValue(XPathNavigator parentNode, string tagName)
    {
      if (parentNode != null)
      {
        XPathNavigator xpathNavigator = parentNode.SelectSingleNode(tagName);
        if (xpathNavigator != null)
          return xpathNavigator.Value.Trim();
      }
      return (string) null;
    }

    private XPathNavigator GetTypeNode(Type type) => this._documentNavigator.SelectSingleNode(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "/doc/members/member[@name='T:{0}']", (object) XmlDocumentationProvider.GetTypeName(type)));

    private static string GetTypeName(Type type)
    {
      string typeName = type.FullName;
      if (type.IsGenericType)
      {
        Type genericTypeDefinition = type.GetGenericTypeDefinition();
        Type[] genericArguments = type.GetGenericArguments();
        string fullName = genericTypeDefinition.FullName;
        typeName = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}{{{1}}}", (object) fullName.Substring(0, fullName.IndexOf('`')), (object) string.Join(",", ((IEnumerable<Type>) genericArguments).Select<Type, string>((Func<Type, string>) (t => XmlDocumentationProvider.GetTypeName(t))).ToArray<string>()));
      }
      if (type.IsNested)
        typeName = typeName.Replace("+", ".");
      return typeName;
    }

    string IModelDocumentationProvider.GetDocumentation(MemberInfo member) => throw new NotImplementedException();

    string IModelDocumentationProvider.GetDocumentation(Type type) => throw new NotImplementedException();
  }
}
