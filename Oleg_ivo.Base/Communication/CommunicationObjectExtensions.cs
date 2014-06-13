using System;
using System.ServiceModel;
using System.Threading.Tasks;
using NLog;

namespace Oleg_ivo.Base.Communication
{
    /// <summary>
    /// Расширения работы с <see cref="ICommunicationObject"/>
    /// </summary>
    public static class CommunicationObjectExtensions
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

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
        /// <param name="timeout">Таймаут закрытия (по умолчанию без таймаута)</param>
        public static void SafeClose<TService>(this TService client, TimeSpan? timeout = null) where TService : ICommunicationObject
        {
            try
            {
                log.Trace("Попытка штатного закрытия объекта ICommunicationObject ({0})", client);
                if (timeout.HasValue)
                {
                    try
                    {
                        log.Trace("Таймаут ожидания: {0}", timeout.Value);
                        Task.Factory.StartNew(client.Close).Wait(timeout.Value);
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.GetBaseException();
                    }
                }
                else
                    client.Close();

            }
            catch (CommunicationException ex)
            {
                log.Warn("При попытке закрыть ICommunicationObject ({0}) выброшено исключение {1}. Будет вызван метод Abort", client, ex.Message);
                client.Abort(); // Don't care about these exceptions. The call has completed anyway.
            }
            catch (TimeoutException ex)
            {
                log.Warn("При попытке закрыть ICommunicationObject ({0}) выброшено исключение {1}. Будет вызван метод Abort", client, ex.Message);
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
