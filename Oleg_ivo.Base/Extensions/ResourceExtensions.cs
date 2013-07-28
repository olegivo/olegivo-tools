using System;

namespace Oleg_ivo.Base.Extensions
{

    /// <summary>
    ///  Методы расширения по работе с ресурсами
    /// </summary>
    public static class ResourceExtensions 
    {

        /// <summary>
        ///  Получает ресурс из сборки указанного типа, квалифицируя имя ресурса простанством имен типа
        /// </summary>
        /// <param name="type">Тип, сброка и пространство имен которого используются для поиска ресурса</param>
        /// <param name="resourceName">Имя файла ресурса</param>
        /// <returns></returns>
        public static System.IO.Stream GetManifestResourceStream(this Type type, string resourceName)
        {
            if (type == null) throw new ArgumentNullException("type");
            return type.Assembly.GetManifestResourceStream(type, resourceName);
        }
    }
}