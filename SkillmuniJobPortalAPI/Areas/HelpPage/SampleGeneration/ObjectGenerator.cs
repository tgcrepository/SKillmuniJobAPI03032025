// Decompiled with JetBrains decompiler
// Type: m2ostnextservice.Areas.HelpPage.ObjectGenerator
// Assembly: m2ostnextservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 87E15969-D15D-4CF2-8DED-07401C08FD2E
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni_CMS_API-20250130T185510Z-001\Skillmuni_CMS_API\bin\m2ostnextservice.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace m2ostnextservice.Areas.HelpPage
{
  public class ObjectGenerator
  {
    internal const int DefaultCollectionSize = 2;
    private readonly ObjectGenerator.SimpleTypeObjectGenerator SimpleObjectGenerator = new ObjectGenerator.SimpleTypeObjectGenerator();

    public object GenerateObject(Type type) => this.GenerateObject(type, new Dictionary<Type, object>());

    private object GenerateObject(Type type, Dictionary<Type, object> createdObjectReferences)
    {
      try
      {
        if (ObjectGenerator.SimpleTypeObjectGenerator.CanGenerateObject(type))
          return this.SimpleObjectGenerator.GenerateObject(type);
        if (type.IsArray)
          return ObjectGenerator.GenerateArray(type, 2, createdObjectReferences);
        if (type.IsGenericType)
          return ObjectGenerator.GenerateGenericType(type, 2, createdObjectReferences);
        if (type == typeof (IDictionary))
          return ObjectGenerator.GenerateDictionary(typeof (Hashtable), 2, createdObjectReferences);
        if (typeof (IDictionary).IsAssignableFrom(type))
          return ObjectGenerator.GenerateDictionary(type, 2, createdObjectReferences);
        if (type == typeof (IList) || type == typeof (IEnumerable) || type == typeof (ICollection))
          return ObjectGenerator.GenerateCollection(typeof (ArrayList), 2, createdObjectReferences);
        if (typeof (IList).IsAssignableFrom(type))
          return ObjectGenerator.GenerateCollection(type, 2, createdObjectReferences);
        if (type == typeof (IQueryable))
          return ObjectGenerator.GenerateQueryable(type, 2, createdObjectReferences);
        if (type.IsEnum)
          return ObjectGenerator.GenerateEnum(type);
        if (!type.IsPublic)
        {
          if (!type.IsNestedPublic)
            goto label_22;
        }
        return ObjectGenerator.GenerateComplexObject(type, createdObjectReferences);
      }
      catch
      {
        return (object) null;
      }
label_22:
      return (object) null;
    }

    private static object GenerateGenericType(
      Type type,
      int collectionSize,
      Dictionary<Type, object> createdObjectReferences)
    {
      Type genericTypeDefinition = type.GetGenericTypeDefinition();
      if (genericTypeDefinition == typeof (Nullable<>))
        return ObjectGenerator.GenerateNullable(type, createdObjectReferences);
      if (genericTypeDefinition == typeof (KeyValuePair<,>))
        return ObjectGenerator.GenerateKeyValuePair(type, createdObjectReferences);
      if (ObjectGenerator.IsTuple(genericTypeDefinition))
        return ObjectGenerator.GenerateTuple(type, createdObjectReferences);
      Type[] genericArguments = type.GetGenericArguments();
      if (genericArguments.Length == 1)
      {
        if (genericTypeDefinition == typeof (IList<>) || genericTypeDefinition == typeof (IEnumerable<>) || genericTypeDefinition == typeof (ICollection<>))
          return ObjectGenerator.GenerateCollection(typeof (List<>).MakeGenericType(genericArguments), collectionSize, createdObjectReferences);
        if (genericTypeDefinition == typeof (IQueryable<>))
          return ObjectGenerator.GenerateQueryable(type, collectionSize, createdObjectReferences);
        if (typeof (ICollection<>).MakeGenericType(genericArguments[0]).IsAssignableFrom(type))
          return ObjectGenerator.GenerateCollection(type, collectionSize, createdObjectReferences);
      }
      if (genericArguments.Length == 2)
      {
        if (genericTypeDefinition == typeof (IDictionary<,>))
          return ObjectGenerator.GenerateDictionary(typeof (Dictionary<,>).MakeGenericType(genericArguments), collectionSize, createdObjectReferences);
        if (typeof (IDictionary<,>).MakeGenericType(genericArguments[0], genericArguments[1]).IsAssignableFrom(type))
          return ObjectGenerator.GenerateDictionary(type, collectionSize, createdObjectReferences);
      }
      return type.IsPublic || type.IsNestedPublic ? ObjectGenerator.GenerateComplexObject(type, createdObjectReferences) : (object) null;
    }

    private static object GenerateTuple(Type type, Dictionary<Type, object> createdObjectReferences)
    {
      Type[] genericArguments = type.GetGenericArguments();
      object[] objArray = new object[genericArguments.Length];
      bool flag = true;
      ObjectGenerator objectGenerator = new ObjectGenerator();
      for (int index = 0; index < genericArguments.Length; ++index)
      {
        objArray[index] = objectGenerator.GenerateObject(genericArguments[index], createdObjectReferences);
        flag &= objArray[index] == null;
      }
      return flag ? (object) null : Activator.CreateInstance(type, objArray);
    }

    private static bool IsTuple(Type genericTypeDefinition) => genericTypeDefinition == typeof (Tuple<>) || genericTypeDefinition == typeof (Tuple<,>) || genericTypeDefinition == typeof (Tuple<,,>) || genericTypeDefinition == typeof (Tuple<,,,>) || genericTypeDefinition == typeof (Tuple<,,,,>) || genericTypeDefinition == typeof (Tuple<,,,,,>) || genericTypeDefinition == typeof (Tuple<,,,,,,>) || genericTypeDefinition == typeof (Tuple<,,,,,,,>);

    private static object GenerateKeyValuePair(
      Type keyValuePairType,
      Dictionary<Type, object> createdObjectReferences)
    {
      Type[] genericArguments = keyValuePairType.GetGenericArguments();
      Type type1 = genericArguments[0];
      Type type2 = genericArguments[1];
      ObjectGenerator objectGenerator = new ObjectGenerator();
      object obj1 = objectGenerator.GenerateObject(type1, createdObjectReferences);
      object obj2 = objectGenerator.GenerateObject(type2, createdObjectReferences);
      if (obj1 == null && obj2 == null)
        return (object) null;
      return Activator.CreateInstance(keyValuePairType, obj1, obj2);
    }

    private static object GenerateArray(
      Type arrayType,
      int size,
      Dictionary<Type, object> createdObjectReferences)
    {
      Type elementType = arrayType.GetElementType();
      Array instance = Array.CreateInstance(elementType, size);
      bool flag = true;
      ObjectGenerator objectGenerator = new ObjectGenerator();
      for (int index = 0; index < size; ++index)
      {
        object obj = objectGenerator.GenerateObject(elementType, createdObjectReferences);
        instance.SetValue(obj, index);
        flag &= obj == null;
      }
      return flag ? (object) null : (object) instance;
    }

    private static object GenerateDictionary(
      Type dictionaryType,
      int size,
      Dictionary<Type, object> createdObjectReferences)
    {
      Type type1 = typeof (object);
      Type type2 = typeof (object);
      if (dictionaryType.IsGenericType)
      {
        Type[] genericArguments = dictionaryType.GetGenericArguments();
        type1 = genericArguments[0];
        type2 = genericArguments[1];
      }
      object instance = Activator.CreateInstance(dictionaryType);
      MethodInfo method1 = dictionaryType.GetMethod("Add");
      if ((object) method1 == null)
        method1 = dictionaryType.GetMethod("TryAdd");
      MethodInfo methodInfo1 = method1;
      MethodInfo method2 = dictionaryType.GetMethod("Contains");
      if ((object) method2 == null)
        method2 = dictionaryType.GetMethod("ContainsKey");
      MethodInfo methodInfo2 = method2;
      ObjectGenerator objectGenerator = new ObjectGenerator();
      for (int index = 0; index < size; ++index)
      {
        object obj1 = objectGenerator.GenerateObject(type1, createdObjectReferences);
        if (obj1 == null)
          return (object) null;
        if (!(bool) methodInfo2.Invoke(instance, new object[1]
        {
          obj1
        }))
        {
          object obj2 = objectGenerator.GenerateObject(type2, createdObjectReferences);
          methodInfo1.Invoke(instance, new object[2]
          {
            obj1,
            obj2
          });
        }
      }
      return instance;
    }

    private static object GenerateEnum(Type enumType)
    {
      Array values = Enum.GetValues(enumType);
      return values.Length > 0 ? values.GetValue(0) : (object) null;
    }

    private static object GenerateQueryable(
      Type queryableType,
      int size,
      Dictionary<Type, object> createdObjectReferences)
    {
      bool isGenericType = queryableType.IsGenericType;
      object source = !isGenericType ? ObjectGenerator.GenerateArray(typeof (object[]), size, createdObjectReferences) : ObjectGenerator.GenerateCollection(typeof (List<>).MakeGenericType(queryableType.GetGenericArguments()), size, createdObjectReferences);
      if (source == null)
        return (object) null;
      if (!isGenericType)
        return (object) ((IEnumerable) source).AsQueryable();
      return typeof (Queryable).GetMethod("AsQueryable", new Type[1]
      {
        typeof (IEnumerable<>).MakeGenericType(queryableType.GetGenericArguments())
      }).Invoke((object) null, new object[1]{ source });
    }

    private static object GenerateCollection(
      Type collectionType,
      int size,
      Dictionary<Type, object> createdObjectReferences)
    {
      Type type = collectionType.IsGenericType ? collectionType.GetGenericArguments()[0] : typeof (object);
      object instance = Activator.CreateInstance(collectionType);
      MethodInfo method = collectionType.GetMethod("Add");
      bool flag = true;
      ObjectGenerator objectGenerator = new ObjectGenerator();
      for (int index = 0; index < size; ++index)
      {
        object obj = objectGenerator.GenerateObject(type, createdObjectReferences);
        method.Invoke(instance, new object[1]{ obj });
        flag &= obj == null;
      }
      return flag ? (object) null : instance;
    }

    private static object GenerateNullable(
      Type nullableType,
      Dictionary<Type, object> createdObjectReferences)
    {
      return new ObjectGenerator().GenerateObject(nullableType.GetGenericArguments()[0], createdObjectReferences);
    }

    private static object GenerateComplexObject(
      Type type,
      Dictionary<Type, object> createdObjectReferences)
    {
      object complexObject1 = (object) null;
      if (createdObjectReferences.TryGetValue(type, out complexObject1))
        return complexObject1;
      object complexObject2;
      if (type.IsValueType)
      {
        complexObject2 = Activator.CreateInstance(type);
      }
      else
      {
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == (ConstructorInfo) null)
          return (object) null;
        complexObject2 = constructor.Invoke(new object[0]);
      }
      createdObjectReferences.Add(type, complexObject2);
      ObjectGenerator.SetPublicProperties(type, complexObject2, createdObjectReferences);
      ObjectGenerator.SetPublicFields(type, complexObject2, createdObjectReferences);
      return complexObject2;
    }

    private static void SetPublicProperties(
      Type type,
      object obj,
      Dictionary<Type, object> createdObjectReferences)
    {
      PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      ObjectGenerator objectGenerator = new ObjectGenerator();
      foreach (PropertyInfo propertyInfo in properties)
      {
        if (propertyInfo.CanWrite)
        {
          object obj1 = objectGenerator.GenerateObject(propertyInfo.PropertyType, createdObjectReferences);
          propertyInfo.SetValue(obj, obj1, (object[]) null);
        }
      }
    }

    private static void SetPublicFields(
      Type type,
      object obj,
      Dictionary<Type, object> createdObjectReferences)
    {
      FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
      ObjectGenerator objectGenerator = new ObjectGenerator();
      foreach (FieldInfo fieldInfo in fields)
      {
        object obj1 = objectGenerator.GenerateObject(fieldInfo.FieldType, createdObjectReferences);
        fieldInfo.SetValue(obj, obj1);
      }
    }

    private class SimpleTypeObjectGenerator
    {
      private long _index;
      private static readonly Dictionary<Type, Func<long, object>> DefaultGenerators = ObjectGenerator.SimpleTypeObjectGenerator.InitializeGenerators();

      private static Dictionary<Type, Func<long, object>> InitializeGenerators() => new Dictionary<Type, Func<long, object>>()
      {
        {
          typeof (bool),
          (Func<long, object>) (index => (object) true)
        },
        {
          typeof (byte),
          (Func<long, object>) (index => (object) (byte) 64)
        },
        {
          typeof (char),
          (Func<long, object>) (index => (object) 'A')
        },
        {
          typeof (DateTime),
          (Func<long, object>) (index => (object) DateTime.Now)
        },
        {
          typeof (DateTimeOffset),
          (Func<long, object>) (index => (object) new DateTimeOffset(DateTime.Now))
        },
        {
          typeof (DBNull),
          (Func<long, object>) (index => (object) DBNull.Value)
        },
        {
          typeof (Decimal),
          (Func<long, object>) (index => (object) (Decimal) index)
        },
        {
          typeof (double),
          (Func<long, object>) (index => (object) ((double) index + 0.1))
        },
        {
          typeof (Guid),
          (Func<long, object>) (index => (object) Guid.NewGuid())
        },
        {
          typeof (short),
          (Func<long, object>) (index => (object) (short) (index % (long) short.MaxValue))
        },
        {
          typeof (int),
          (Func<long, object>) (index => (object) (int) (index % (long) int.MaxValue))
        },
        {
          typeof (long),
          (Func<long, object>) (index => (object) index)
        },
        {
          typeof (object),
          (Func<long, object>) (index => new object())
        },
        {
          typeof (sbyte),
          (Func<long, object>) (index => (object) (sbyte) 64)
        },
        {
          typeof (float),
          (Func<long, object>) (index => (object) ((float) index + 0.1f))
        },
        {
          typeof (string),
          (Func<long, object>) (index => (object) string.Format((IFormatProvider) CultureInfo.CurrentCulture, "sample string {0}", (object) index))
        },
        {
          typeof (TimeSpan),
          (Func<long, object>) (index => (object) TimeSpan.FromTicks(1234567L))
        },
        {
          typeof (ushort),
          (Func<long, object>) (index => (object) (ushort) ((ulong) index % (ulong) ushort.MaxValue))
        },
        {
          typeof (uint),
          (Func<long, object>) (index => (object) (uint) ((ulong) index % (ulong) uint.MaxValue))
        },
        {
          typeof (ulong),
          (Func<long, object>) (index => (object) (ulong) index)
        },
        {
          typeof (Uri),
          (Func<long, object>) (index => (object) new Uri(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "http://webapihelppage{0}.com", (object) index)))
        }
      };

      public static bool CanGenerateObject(Type type) => ObjectGenerator.SimpleTypeObjectGenerator.DefaultGenerators.ContainsKey(type);

      public object GenerateObject(Type type) => ObjectGenerator.SimpleTypeObjectGenerator.DefaultGenerators[type](++this._index);
    }
  }
}
