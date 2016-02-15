using System;
using System.Reflection;

namespace Oleg_ivo.Base.Utils
{
    public interface IApplicationVersionProvider
    {
        Version ApplicationVersion { get; }
    }

    public static class ApplicationVersionHelper
    {
        public static Version GetVersion(Type type)
        {
            return new Version(type.Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version);
        }
    }
}
