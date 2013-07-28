using System;

namespace Oleg_ivo.PrismExtensions.Extensions
{

    public static class BaseExtensions
    {


        #region Расширения IServiceProvider

        /// <summary>
        ///  Метод расширения, позволяющий вызвать метод GetService типизированно и получить максимум от вывода типов.
        /// </summary>
        /// <typeparam name="TService">Тип службы, которую требуется получить</typeparam>
        /// <param name="provider">Контекст, содержащий службы</param>
        /// <returns>Экземпляр службы или null, если контекст не содержит такой службы</returns>
        /// <exception cref="ArgumentNullException">Если в качестве provider передан null</exception>
        public static TService GetService<TService>(this IServiceProvider provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            return (TService) provider.GetService(typeof(TService));
        }


        /// <summary>
        ///  Метод расширения, позволяющий вызвать метод GetService типизированно и получить максимум от вывода типов.
        /// </summary>
        /// <typeparam name="TService">Тип службы, которую требуется получить</typeparam>
        /// <param name="provider">Контекст, содержащий службы</param>
        /// <returns>Всегда возвращает экземпляр службы или выбрасывает исключение, если контейнер не содержит службы</returns>
        /// <exception cref="ArgumentNullException">Если в качестве provider передан null</exception>
        /// <exception cref="InvalidOperationException">Если переданный провайдет не содержит реализации службы</exception>
        public static TService GetRequiredService<TService>(this IServiceProvider provider)
        {
            if (provider == null) throw new ArgumentNullException("provider");
            var service = (TService) provider.GetService(typeof(TService));
            if (service != null) return service;

            throw new InvalidOperationException(
                String.Format("Переданный контекст {0} не содержит службы {1}",
                    provider, typeof(TService)));
        }

        #endregion
    }
}
