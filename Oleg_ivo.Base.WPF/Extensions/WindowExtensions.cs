using System.Linq;
using System.Windows;

namespace Oleg_ivo.Base.WPF.Extensions
{
    public static class WindowExtensions
    {
        /// <summary>
        /// Установить текущее активное окно приложения в <paramref name="window"/>.Owner
        /// </summary>
        /// <param name="window"></param>
        public static void SetActiveWindowOwner(this Window window)
        {
            window.Owner = GetActiveWindow(Application.Current);
        }

        public static Window GetActiveWindow(this Application application)
        {
            return application.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
    }
}