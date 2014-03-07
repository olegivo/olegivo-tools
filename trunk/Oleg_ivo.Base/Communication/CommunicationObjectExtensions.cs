using System;
using System.ServiceModel;

namespace Oleg_ivo.Base.Communication
{
    /// <summary>
    /// Расширения работы с <see cref="ICommunicationObject"/>
    /// </summary>
    public static class CommunicationObjectExtensions
    {
        /// <summary>
        /// Безопасный вызов и закрытие <see cref="client"/>
        /// </summary>
        /// <typeparam name="TResult">Тип результата вызова метода клиента</typeparam>
        /// <typeparam name="TService">Тип клиента (должен реализовать <see cref="ICommunicationObject"/>)</typeparam>
        /// <param name="client">Инстанс клиента</param>
        /// <param name="method">Метод, вызываемый для клиента</param>
        /// <returns></returns>
        public static TResult MakeSafeServiceCall<TResult, TService>(this TService client, Func<TService, TResult> method) where TService : ICommunicationObject
        {
            TResult result;

            try
            {
                result = method(client);
            }
            finally
            {
                client.SafeClose();
            }

            return result;
        }

        /// <summary>
        /// Безопасное закрытие <see cref="client"/>
        /// </summary>
        /// <typeparam name="TService">Тип клиента (должен реализовать <see cref="ICommunicationObject"/>)</typeparam>
        /// <param name="client">Инстанс клиента</param>
        public static void SafeClose<TService>(this TService client) where TService : ICommunicationObject
        {
            try
            {
                client.Close();
            }
            catch (CommunicationException)
            {
                client.Abort(); // Don't care about these exceptions. The call has completed anyway.
            }
            catch (TimeoutException)
            {
                client.Abort(); // Don't care about these exceptions. The call has completed anyway.
            }
            catch (Exception)
            {
                client.Abort();
                throw;
            }
        }
    }
}
