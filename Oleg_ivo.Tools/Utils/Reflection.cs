using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.Utils
{
    ///<summary>
    ///
    ///</summary>
    public abstract class Reflection
    {
        #region Assemblies
        /// <summary>
        /// Подгрузить некоторые сборки
        /// </summary>
        public static void LoadAssemblies()
        {
            string[] assemblyStrings = new string[]{/*"Transfers.dll"*/};
            foreach (string assemblyString in assemblyStrings)
                Assembly.LoadFrom(string.Format("{0}\\{1}", Application.StartupPath, assemblyString));
        }

        ///<summary>
        /// Получить все загруженные сборки
        ///</summary>
        ///<param name="onlyMine">Только мои</param>
        ///<param name="domain">Домен</param>
        ///<returns></returns>
        public static Assembly[] GetAssemblies(bool onlyMine, AppDomain domain)
        {
            List<Assembly> assembliesList = new List<Assembly>(GetAssembliesFromAppDomain(domain));
            //if (_customAssemblies.Count > 0)
            //{
            //    assembliesList.AddRange(_customAssemblies);
            //}
            if (onlyMine)
                foreach (Assembly assembly in assembliesList.ToArray())
                    if (assembly.FullName == null || !assembly.FullName.Contains("Oleg_ivo"))
                        assembliesList.Remove(assembly);
            
            return assembliesList.ToArray();
        }

        /// <summary>
        /// Получить сборки из домена
        /// </summary>
        /// <param name="domain">Если null, будет взят домен текущего приложения</param>
        /// <returns></returns>
        private static Assembly[] GetAssembliesFromAppDomain(AppDomain domain)
        {
            if (domain == null) domain = AppDomain.CurrentDomain;
            return domain.GetAssemblies();
        }

        #endregion

        #region Types
        ///<summary>
        /// Получить все типы из домена текущего приложения
        ///</summary>
        ///<returns></returns>
        public static List<Type> GetTypesFromDomain()
        {
            return GetTypesFromDomain(null);
        }

        ///<summary>
        /// Получить все типы из домена
        ///</summary>
        ///<param name="domain"></param>
        ///<returns></returns>
        public static List<Type> GetTypesFromDomain(AppDomain domain)
        {
            List<Type> types = new List<Type>();

            foreach (Assembly assembly in GetAssembliesFromAppDomain(domain))
                foreach (Type type in assembly.GetTypes())
                    types.Add(type);

            return types;
        }

        /// <summary>
        /// Получить тип
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static Type GetTypeFromAssemblies(string fullName)
        {
            foreach (Type type in GetTypesFromDomain())
                if (type.FullName == fullName)
                    return type;

            return null;
        }


        #endregion

        #region Members
        /// <summary>
        /// Получить все конструкторы для типа
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ConstructorInfo[] GetConstructorsForType(Type type)
        {
            ConstructorInfo[] constructorInfos = type.GetConstructors();
            return constructorInfos;
        }

        /// <summary>
        /// Получить единственный конструктор для типа (если он один)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ConstructorInfo GetTheOneConstructorForType(Type type)
        {
            ConstructorInfo[] constructorInfos = GetConstructorsForType(type);
            if (constructorInfos != null && constructorInfos.Length == 1)
                return constructorInfos[0];
            return null;
        }

        /// <summary>
        /// Найти метод по имени для указанного типа
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static MethodInfo FindMethodInfo(Type type, string methodName)
        {
            MethodInfo methodInfo = null;
            if (type != null)
            {
                MethodInfo[] methodInfos = type.GetMethods();

                foreach (MethodInfo info in methodInfos)
                {
                    if (string.Compare(info.Name, methodName, true) == 0)
                    {
                        methodInfo = info;
                        break;
                    }
                }
            }

            return methodInfo;
        }

        #endregion


        #region Attributes & Metadata Descriptions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="attributeType"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(ICustomAttributeProvider customAttributeProvider, Type attributeType)
        {
            return GetAttribute(customAttributeProvider, attributeType, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="attributeType"></param>
        /// <param name="throwOnlyOne">Генерить исключение, если атрибут обязан быть только один</param>
        /// <returns></returns>
        public static Attribute GetAttribute(ICustomAttributeProvider customAttributeProvider, Type attributeType, bool throwOnlyOne)
        {
            return GetAttribute(customAttributeProvider, attributeType, throwOnlyOne, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="attributeType"></param>
        /// <param name="throwOnlyOne">Генерить исключение, если атрибут обязан быть только один</param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(ICustomAttributeProvider customAttributeProvider, Type attributeType, bool throwOnlyOne, bool inherit)
        {
            Attribute[] attributes = null;
            if (customAttributeProvider != null)
                attributes = (Attribute[])customAttributeProvider.GetCustomAttributes(attributeType, inherit);
            if (attributes != null)
                if (attributes.Length == 1)
                {
                    return attributes[0];
                }
                else if (attributes.Length > 1 && throwOnlyOne)
                {
                    throw new Exception(string.Format("Несколько атрибутов '{0}'. Должен быть только один!", GetTypeDescription(attributeType)));
                }
            return null;
        }

        /// <summary>
        /// Возвращает описание из метаданных объекта
        /// </summary>
        /// <param name="customAttributeProvider"></param>
        /// <param name="throwOnlyOne">Управление исключениями при количестве атрибутов > 1</param>
        /// <returns></returns>
        private static string GetDescription(ICustomAttributeProvider customAttributeProvider, bool throwOnlyOne)
        {
            DescriptionAttribute descriptionAttribute = GetAttribute(customAttributeProvider, typeof(DescriptionAttribute), throwOnlyOne) as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;
            return "";
        }
        
        /// <summary>
        /// Описание типа (если оно пустое, возвращает имя типа)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTypeDescription(Type type)
        {
            string description = null;
            if (type != null)
            {
                description = GetDescription(type, false);
                if (description == "")
                    description = type.Name;
            }
            return description;
        }

        /// <summary>
        /// Возвращает описание из метаданных объекта
        /// </summary>
        /// <param name="customAttributeProvider">Объект</param>
        /// <returns></returns>
        public static string GetDescription(ICustomAttributeProvider customAttributeProvider)
        {
            return GetDescription(customAttributeProvider, true);
        }

        /// <summary>
        /// Возвращает атрибут для элемента перечисления
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="itemName"></param>
        /// <param name="customAttributeType"></param>
        /// <returns></returns>
        public static Attribute GetEnumAttributes(Type enumType, string itemName, Type customAttributeType)
        {
            if(enumType!=null && customAttributeType!=null)
            {
                FieldInfo fieldInfo = enumType.GetField(itemName);
                if (fieldInfo != null)
                    return GetAttribute(fieldInfo, customAttributeType, true);
            }
            return null;
        }

        /// <summary>
        /// Возвращает словарь значений перечисления (используется атрибут <see cref="DescriptionAttribute"/>)
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static Dictionary<Enum, string> GetEnumMemberDictionary(Type enumType)
        {
            var dictionary = new Dictionary<Enum, string>();
            if (enumType != null)
            {
                foreach (var name in Enum.GetNames(enumType))
                {
                    var fieldInfo = enumType.GetField(name);
                    if (fieldInfo!=null)
                    {
                        var description = GetDescription(fieldInfo);
                        var value = (Enum)Enum.Parse(enumType, name);
                        dictionary.Add(value, description);
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// Получить описание для элемента перечисления
        /// </summary>
        /// <param name="enumMember">Элемент перечисления</param>
        /// <returns></returns>
        public static string GetDescription(Enum enumMember)
        {
            var enumType = enumMember.GetType();
            var enumMemberDictionary = GetEnumMemberDictionary(enumType);
            var value = (Enum) Enum.Parse(enumType, enumMember.ToString());
            var description = enumMemberDictionary[value];

            return description;
        }

        #endregion

        ///<summary>
        ///
        ///</summary>
        ///<param name="memberInfo"></param>
        public delegate bool MemberInfoFilter(MemberInfo memberInfo);

        ///<summary>
        ///
        ///</summary>
        ///<param name="type"></param>
        ///<param name="memberInfoFilter"></param>
        ///<param name="memberType"></param>
        ///<returns></returns>
        public static MemberInfo[] GetMembers(Type type, MemberTypes memberType, MemberInfoFilter memberInfoFilter)
        {
            List<MemberInfo> memberInfos = new List<MemberInfo>();
            MemberInfo[] members = GetMembers(type, memberType);
            if(members!=null)
            {
                memberInfos.AddRange(members);
                if (memberInfoFilter != null)
                    foreach (MemberInfo memberInfo in memberInfos.ToArray())
                        if (!memberInfoFilter(memberInfo))
                            memberInfos.Remove(memberInfo);
            }

            return memberInfos.ToArray();
        }

        private static MemberInfo[] GetMembers(Type type, MemberTypes memberType)
        {
            MemberInfo[] infos = null;
            if (type!=null)
            {
                switch (memberType)
                {
                    case MemberTypes.Constructor:
                        infos = type.GetConstructors();
                        break;
                    case MemberTypes.Event:
                        infos = type.GetEvents();
                        break;
                    case MemberTypes.Field:
                        infos = type.GetFields();
                        break;
                    case MemberTypes.Method:
                        infos = type.GetMethods();
                        break;
                    case MemberTypes.Property:
                        infos = type.GetProperties();
                        break;
                    case MemberTypes.All:
                        infos = type.GetMembers();
                        break;
                }
            }

            return infos;
        }
    }
}