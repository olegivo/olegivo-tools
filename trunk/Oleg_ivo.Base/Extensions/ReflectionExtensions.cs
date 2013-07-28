using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Oleg_ivo.Base.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this AttributeCollection collection) where TAttribute : Attribute
        {
            return (TAttribute)collection[typeof(TAttribute)];
        }

        public static bool Contains<TAttribute>(this AttributeCollection collection) where TAttribute : Attribute
        {
            return collection[typeof(TAttribute)] != null;
        }


        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            return attributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        public static IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return attributeProvider.GetCustomAttributes<TAttribute>(true);
        }

        public static TAttribute GetCustomAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return attributeProvider.GetCustomAttributes<TAttribute>(true).FirstOrDefault();
        }

        public static string GetShortTypeName(this Type type)
        {
            if (!type.IsGenericType) return type.Name;
            var name = type.Name.Remove(type.Name.IndexOf('`'));
            return string.Format("{0}<{1}>", name, type.GetGenericArguments().JoinToString(", ", GetShortTypeName));
        }
    }
}
