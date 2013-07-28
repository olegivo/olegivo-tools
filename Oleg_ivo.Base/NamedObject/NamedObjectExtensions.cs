using System;
using Oleg_ivo.Base.Autofac;

namespace Oleg_ivo.Base.NamedObject
{
    public static class NamedObjectExtensions
    {
        public static string GetFullName(this INamedObject namedObject, NameForm nameForm)
        {
            Enforce.ArgumentNotNull(namedObject, "namedObject");

            var currentObject = namedObject;

            Func<INamedObject, string> getName;

            switch (nameForm)
            {
                case NameForm.InstanceOrTypeName:
                    getName = GetInstanceOrTypeName;
                    break;
                case NameForm.TypeNameAndInstanceName:
                    getName = GetTypeNameAndInstanceName;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("nameForm");
            }

            var fullName = getName(currentObject);

            while (currentObject.Parent != null)
            {
                fullName = string.Format("{0}.{1}", getName(currentObject.Parent), fullName);
                currentObject = currentObject.Parent;
            }

            return fullName;
        }

        public static string GetTypeNameAndInstanceName(this INamedObject namedObject)
        {
            Enforce.ArgumentNotNull(namedObject, "namedObject");

            var instanceName = string.IsNullOrEmpty(namedObject.Name) ? "" : string.Format(" ({0})", namedObject.Name);

            return string.Format("{0}{1}", namedObject.GetType().Name, instanceName);
        }

        public static string GetInstanceOrTypeName(this INamedObject namedObject)
        {
            Enforce.ArgumentNotNull(namedObject, "namedObject");

            return string.IsNullOrEmpty(namedObject.Name) ? namedObject.GetType().Name : namedObject.Name;
        }

        internal static void SetParent(this INamedObject instance, object parent)
        {
            if (instance != null && parent is INamedObject)
                instance.Parent = parent as INamedObject;
        }

        //public static void SetParents(this INamedObject namedObject)
        //{
        //    Enforce.ArgumentNotNull(namedObject, "namedObject");

        //    var children = namedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //                                        .Where(t => t.Name != "Parent")
        //                                        .Select(t => t.GetValue(namedObject, null)).OfType<INamedObject>();

        //    foreach (var child in children)
        //    {
        //        child.Parent = namedObject;
        //        SetParents(child);
        //    }
        //}
    }
}
