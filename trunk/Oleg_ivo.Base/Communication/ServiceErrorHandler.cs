using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Oleg_ivo.Base.Communication
{
    public class ServiceErrorHandler : IErrorHandler
    {
        private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

        public bool HandleError(Exception error) { return false; }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {

            if (error is FaultException)
                return;

            if (error is AggregateException)
                error = error.GetBaseException();

            FaultException faultEx = null;

            if (error is TimeoutException)
                faultEx = ToGenericFault(error);

            if (faultEx == null)
            {
                log.ErrorException("Error in service is not mapped to expected fault", error);
                return;
            }
            log.DebugException(string.Format("Error mapped to {0}", faultEx.GetType().Name), error);

            var msgFault = faultEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, faultEx.Action);
        }


        private static FaultException ToGenericFault(Exception error)
        {
            var code = error.GetType().Name;
            const string suffix = "exception";
            if (code.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) && code.Length > suffix.Length) code = code.Remove(code.Length - suffix.Length);
            return new FaultException(error.Message, new FaultCode(code));
        }
    }
}