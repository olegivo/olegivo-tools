using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Oleg_ivo.Base.Communication
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ErrorBehaviorAttribute : Attribute, IServiceBehavior
    {
        readonly Type handlerType;

        public ErrorBehaviorAttribute(Type handlerType)
        {
            this.handlerType = handlerType;
        }

        #region IServiceBehavior Members

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }


        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            var errorHandler = (IErrorHandler)Activator.CreateInstance(handlerType);

            foreach (var disp in serviceHostBase.ChannelDispatchers.Cast<ChannelDispatcher>())
                disp.ErrorHandlers.Add(errorHandler);
        }


        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }



        #endregion

    }
}